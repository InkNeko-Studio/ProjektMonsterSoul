using System;
using Game.Shared.NetworkMessages;
using Game.Shared.Player.Scripts;
using UnityEngine;
using Newtonsoft.Json;

namespace Framework.Connection
{
    public enum ConnectionProtocol
    {
        Tcp = 0,
        Udp = 1
    }
    
    public static class ConnectionManager
    {
        public static Action<NetworkTag, string> OnMessage;
        public static string Ip;
        
        private static TcpServerHandler _tcpServerHandler;
        private static TcpClientHandler _tcpClientHandler;
        
        private static UdpServerHandler _udpServerHandler;
        private static UdpClientHandler _udpClientHandler;
        
        public static void HostGame(Action callback = null)
        {
            bool other = false;
            InitializeTcpServerHandler(() =>
            {
                if (other) callback?.Invoke();
                else other = true;
            });
            InitializeUdpServerHandler(() =>
            {
                if (other) callback?.Invoke();
                else other = true;
            });
        }

        private static void InitializeTcpServerHandler(Action callback = null)
        {
            _tcpClientHandler = null;
            
            _tcpServerHandler = new TcpServerHandler();
            _tcpServerHandler.Host(ConnectionConfig.Port, (ip) =>
            {
                Ip = ip;
                callback?.Invoke();
            });
            _tcpServerHandler.OnMessageReceived += (client, message) =>
            {
                _tcpServerHandler.SendAll(client, message);
                var messageBase = JsonConvert.DeserializeObject<MessageBaseData>(message, ConnectionConfig.JsonSettings);
#if UNITY_EDITOR
                Debug.Log($"Message Received\n{messageBase.networkTag}\n{message}");
#endif
                OnMessage?.Invoke(messageBase.networkTag, message);
            };
        }

        private static void InitializeUdpServerHandler(Action callback = null)
        {
            _udpClientHandler = null;
            
            _udpServerHandler = new UdpServerHandler();
            _udpServerHandler.Host(ConnectionConfig.Port, (ip) =>
            {
                Ip = ip;
                callback?.Invoke();
            });
            _udpServerHandler.OnMessageReceived += (client, message) =>
            {
                _udpServerHandler.SendAll(client, message);
                var messageBase = JsonConvert.DeserializeObject<MessageBaseData>(message, ConnectionConfig.JsonSettings);
#if UNITY_EDITOR
                Debug.Log($"Message Received\n{messageBase.networkTag}\n{message}");
#endif
                OnMessage?.Invoke(messageBase.networkTag, message);
            };
        }
        
        public static void Connect(string ip, Action onSuccess = null, Action onFailure = null)
        {
            bool otherSuccess = false;
            bool otherFailure = false;
            InitializeTcpClientHandler(ip, () =>
            {
                if (otherSuccess) onSuccess?.Invoke();
                else otherSuccess = true;
            }, () =>
            {
                if (!otherFailure)
                {
                    onFailure?.Invoke();
                    otherFailure = true;
                }
            });
            InitializeUdpClientHandler(ip, () =>
            {
                if (otherSuccess) onSuccess?.Invoke();
                else otherSuccess = true;
            }, () =>
            {
                if (!otherFailure)
                {
                    onFailure?.Invoke();
                    otherFailure = true;
                }
            });
        }

        private static void InitializeTcpClientHandler(string ip, Action onSuccess = null, Action onFailure = null)
        {
            _tcpServerHandler = null;
            
            _tcpClientHandler = new TcpClientHandler();
            _tcpClientHandler.Connect(ip, ConnectionConfig.Port, onSuccess, onFailure);
            _tcpClientHandler.OnMessageReceived += ( message) =>
            {
                var messageBase = JsonConvert.DeserializeObject<MessageBaseData>(message, ConnectionConfig.JsonSettings);
#if UNITY_EDITOR
                Debug.Log($"Message Received\n{messageBase.networkTag} [TCP]\n{message}");
#endif
                OnMessage?.Invoke(messageBase.networkTag, message);
            };
        }

        private static void InitializeUdpClientHandler(string ip, Action onSuccess = null, Action onFailure = null)
        {
            _udpServerHandler = null;
            
            _udpClientHandler = new UdpClientHandler();
            _udpClientHandler.Connect(ip, ConnectionConfig.Port, onSuccess, onFailure);
            _udpClientHandler.OnMessageReceived += ( message) =>
            {
                var messageBase = JsonConvert.DeserializeObject<MessageBaseData>(message, ConnectionConfig.JsonSettings);
#if UNITY_EDITOR
                Debug.Log($"Message Received\n{messageBase.networkTag} [UDP]\n{message}");
#endif
                OnMessage?.Invoke(messageBase.networkTag, message);
            };
        }
        
        public static void Send(string data, ConnectionProtocol protocol = ConnectionProtocol.Tcp)
        {
#if UNITY_EDITOR
            Debug.Log($"Message Sent\n{data}");
#endif
            if (protocol == ConnectionProtocol.Tcp)
            {
                if (_tcpClientHandler != null) { _tcpClientHandler.Send(data); return; }
                if (_tcpServerHandler != null) { _tcpServerHandler.SendAll(data); return; }
            }
            else if (protocol == ConnectionProtocol.Udp)
            {
                if (_udpClientHandler != null) { _udpClientHandler.Send(data); return; }
                if (_udpServerHandler != null) { _udpServerHandler.SendAll(data); return; }
            }
        }
    }
}