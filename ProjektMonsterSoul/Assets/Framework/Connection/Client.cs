using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                await ReceiveMessages();
            }
            catch (Exception e)
            {
                _isRunning = false;
                onFailure?.Invoke();
            }
        }

        public bool IsConnected()
        {
            return _isRunning;
        }

        private async Task ReceiveMessages()
        {
            using var ms = new MemoryStream();
            byte[] buffer = new byte[256];

            while (_client.Connected && !_cancellation.Token.IsCancellationRequested)
            {
                int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                ms.Write(buffer, 0, bytesRead);
                if (!_stream.DataAvailable)
                {
                    string message = Encoding.UTF8.GetString(ms.ToArray());
                    ms.SetLength(0); // Clear the MemoryStream
                    OnMessageReceived?.Invoke(message);
                }
            }
        }

        public void Send(string message)
        {
            if (_client.Connected)
            {
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
