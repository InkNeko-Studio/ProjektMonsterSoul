using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework.Connection
{
    public class Client
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private CancellationTokenSource _cancellation = new();
        private bool _isRunning = false;

        public event Action<string> OnMessageReceived;

        ~Client()
        {
            Stop();
        }
        
        public async void Connect(string host, int port, Action onSuccess = null, Action onFailure = null)
        {
            if (_isRunning) Stop();
            _isRunning = true;
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(host, port);
                _stream = _client.GetStream();
                onSuccess?.Invoke();
            }
            catch (Exception e)
            {
                _isRunning = false;
                onFailure?.Invoke();
                return;
            }
            await ReceiveMessages();
        }

        public bool IsConnected()
        {
            return _isRunning;
        }

        private async Task ReceiveMessages()
        {
            var sb = new StringBuilder();
            byte[] buffer = new byte[ConnectionConfig.BytesRead];

            while (_client.Connected && !_cancellation.Token.IsCancellationRequested)
            {
                int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                
                string data = sb.ToString();
                int newlineIndex;
                while ((newlineIndex = data.IndexOf('\n')) >= 0)
                {
                    string message = data.Substring(0, newlineIndex).Trim();
                    if (message.Length > 0)
                    {
                        OnMessageReceived?.Invoke(message);
                    }
                    data = data.Substring(newlineIndex + 1);
                }
                sb.Clear();
                sb.Append(data);
            }
        }

        public void Send(string message)
        {
            if (_client.Connected)
            {
                message += "\n";
                byte[] data = Encoding.UTF8.GetBytes(message);
                _stream.Write(data, 0, data.Length);
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
