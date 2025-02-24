using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Framework.Connection
{
    public class UdpClientHandler
    {
        private UdpClient _client;
        private IPEndPoint _remoteEndPoint;
        private CancellationTokenSource _cancellation = new();
        private bool _isRunning = false;

        public event Action<string> OnMessageReceived;

        ~UdpClientHandler()
        {
            Stop();
        }

        public void Connect(string host, int port, Action onSuccess = null, Action onFailure = null)
        {
            if (_isRunning) Stop();
            _isRunning = true;
            try
            {
                _client = new UdpClient();
                _remoteEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
                StartReceiving();
                onSuccess?.Invoke();
            }
            catch (Exception)
            {
                _isRunning = false;
                onFailure?.Invoke();
            }
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
                    UdpReceiveResult result = await _client.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(result.Buffer).Trim();
                    if (!string.IsNullOrEmpty(message))
                    {
                        OnMessageReceived?.Invoke(message);
                    }
                }
                catch (Exception)
                {
                    Stop();
                }
            }
        }

        public void Send(string message)
        {
            if (_isRunning && _remoteEndPoint != null)
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                _client.Send(data, data.Length, _remoteEndPoint);
            }
        }

        public void Stop()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _cancellation.Cancel();
            _client?.Close();
            OnMessageReceived = null;
        }
    }

}