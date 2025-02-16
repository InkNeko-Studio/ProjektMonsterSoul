using System;
using Game.Shared.Player.Scripts;
using UnityEngine;
using Newtonsoft.Json;

namespace Framework.Connection
{
    public static class ConnectionManager
    {
        public static Action<NetworkTag, string> OnMessage;
        public static string Ip;
        
        private static Server _server;
        private static Client _client;
        
        public static void HostGame(Action callback = null)
        {
            _client = null;
            
            _server = new Server();
            _server.Host(ConnectionConfig.Port, (ip) =>
            {
                Ip = ip;
                callback?.Invoke();
            });
            _server.OnMessageReceived += (client, message) =>
            {
                _server.SendAll(client, message);
                var messageBase = JsonConvert.DeserializeObject<MessageBaseData>(message, ConnectionConfig.JsonSettings);
#if UNITY_EDITOR
                Debug.Log($"Message Received\n{messageBase.networkTag}\n{message}");
#endif
                OnMessage?.Invoke(messageBase.networkTag, message);
            };
        }
        
        public static void Connect(string ip, Action onSuccess = null, Action onFailure = null)
        {
            _server = null;
            
            _client = new Client();
            _client.Connect(ip, ConnectionConfig.Port, onSuccess, onFailure);
            _client.OnMessageReceived += ( message) =>
            {
                var messageBase = JsonConvert.DeserializeObject<MessageBaseData>(message, ConnectionConfig.JsonSettings);
#if UNITY_EDITOR
                Debug.Log($"Message Received\n{messageBase.networkTag}\n{message}");
#endif
                OnMessage?.Invoke(messageBase.networkTag, message);
            };
        }
        
        public static void Send(string data)
        {
#if UNITY_EDITOR
            Debug.Log($"Message Sent\n{data}");
#endif
            if (_client != null)
            {
                _client.Send(data);
                return;
            }

            if (_server != null)
            {
                _server.SendAll(data);
                return;
            }
        }
    }
}