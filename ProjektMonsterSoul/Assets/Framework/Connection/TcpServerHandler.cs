using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework.Connection
{
    public class TcpServerHandler
    {
        private TcpListener _listener;
        private readonly List<TcpClient> _clients = new List<TcpClient>();
        private CancellationTokenSource _cancellation = new();
        private bool _isRunning = false;

        public event Action<TcpClient> OnClientConnected;
        public event Action<TcpClient, string> OnMessageReceived;

        ~TcpServerHandler()
        {
            Stop();
        }
        
        public async void Host(int port, Action<string> callback = null)
        {
            if (_isRunning) Stop();
            _isRunning = true;
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);
            _listener = new TcpListener(endpoint);
            _listener.Start();
            callback?.Invoke(GetLocalIPAddress());
            await AcceptClients();
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

        private async Task AcceptClients()
        {
            while (!_cancellation.Token.IsCancellationRequested)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                _clients.Add(client);
                OnClientConnected?.Invoke(client);
                HandleClient(client);
            }
        }

        private async void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            var sb = new StringBuilder();
            byte[] buffer = new byte[ConnectionConfig.BytesRead];

            while (client.Connected && !_cancellation.Token.IsCancellationRequested)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;
                
                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                string data = sb.ToString();
                int newlineIndex;
                while ((newlineIndex = data.IndexOf('\n')) >= 0)
                {
                    string message = data.Substring(0, newlineIndex).Trim();
                    if (message.Length > 0)
                    {
                        OnMessageReceived?.Invoke(client, message);
                    }
                    data = data.Substring(newlineIndex + 1);
                }
                sb.Clear();
                sb.Append(data);
            }

            _clients.Remove(client);
            client.Close();
        }

        public void Send(TcpClient client, string message)
        {
            if (client.Connected)
            {
                message += "\n";
                byte[] data = Encoding.UTF8.GetBytes(message);
                client.GetStream().Write(data, 0, data.Length);
            }
        }

        public void SendAll(string message)
        {
            foreach (var client in _clients)
            {
                Send(client, message);
            }
        }

        public void SendAll(TcpClient sender, string message)
        {
            foreach (var client in _clients)
            {
                if (client == sender) continue;
                Send(client, message);
            }
        }

        public void Stop()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _cancellation.Cancel();
            foreach (var client in _clients) client.Close();
            _clients.Clear();
            _listener.Stop();
            OnMessageReceived = null;
            OnClientConnected = null;
        }
    }
}
