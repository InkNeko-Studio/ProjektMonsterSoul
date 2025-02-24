using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Framework.Connection
{
    public class UdpServerHandler
    {
        private UdpClient _listener;
        private IPEndPoint _endPoint;
        private CancellationTokenSource _cancellation = new();
        private bool _isRunning = false;
        private List<IPEndPoint> _clients = new();

        public event Action<IPEndPoint> OnClientConnected;
        public event Action<IPEndPoint, string> OnMessageReceived;

        ~UdpServerHandler()
        {
            Stop();
        }

        public void Host(int port, Action<string> callback = null)
        {
            if (_isRunning) Stop();
            _isRunning = true;
            _endPoint = new IPEndPoint(IPAddress.Any, port);
            _listener = new UdpClient(_endPoint);
            callback?.Invoke(GetLocalIPAddress());
            StartReceiving();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            return null;
        }

        public bool IsConnected()
        {
            return _isRunning;
        }

        private async void StartReceiving()
        {
            while (_isRunning && !_cancellation.Token.IsCancellationRequested)
            {
                try
                {
                    UdpReceiveResult result = await _listener.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(result.Buffer).Trim();
                    if (!string.IsNullOrEmpty(message))
                    {
                        if (!_clients.Contains(result.RemoteEndPoint))
                        {
                            _clients.Add(result.RemoteEndPoint);
                            OnClientConnected?.Invoke(result.RemoteEndPoint);
                        }
                        OnMessageReceived?.Invoke(result.RemoteEndPoint, message);
                    }
                }
                catch (Exception)
                {
                    Stop();
                }
            }
        }

        public void Send(IPEndPoint client, string message)
        {
            if (_isRunning && client != null)
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                _listener.Send(data, data.Length, client);
            }
        }

        public void SendAll(string message)
        {
            if (_isRunning)
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                foreach (var client in _clients)
                {
                    _listener.Send(data, data.Length, client);
                }
            }
        }

        public void SendAll(IPEndPoint sender, string message)
        {
            foreach (var client in _clients)
            {
                if (Equals(client, sender)) continue;
                Send(client, message);
            }
        }

        public void Stop()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _cancellation.Cancel();
            _listener?.Close();
            _clients.Clear();
            OnMessageReceived = null;
            OnClientConnected = null;
        }
    }
}