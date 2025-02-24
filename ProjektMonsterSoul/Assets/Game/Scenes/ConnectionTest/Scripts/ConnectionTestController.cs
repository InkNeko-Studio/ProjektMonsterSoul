using System;
using Framework.Connection;
using Game.Shared.NetworkMessages;
using Game.Shared.Player.Scripts;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.ConnectionTest.Scripts
{
    [Serializable]
    public class TestMessage : MessageBaseData
    {
        public string text;
    }
    
    public class ConnectionTestController : MonoBehaviour
    {
        [Header("Switch")]
        [SerializeField] private TMP_Text modeText;
        [SerializeField] private Button switchButton;
        
        [Header("Host")]
        [SerializeField] private TMP_Text hostText;
        [SerializeField] private Button hostButton;
        
        [Header("Connect")]
        [SerializeField] private TMP_InputField connectionInput;
        [SerializeField] private Button connectionButton;
        
        [Header("Message")]
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private TMP_InputField textInput;
        [SerializeField] private Button sendButton;

        private ConnectionProtocol _connectionProtocol;
        
        private void Start()
        {
            sendButton.interactable = false;
            
            hostButton.onClick.AddListener(() =>
            {
                ConnectionManager.HostGame(() =>
                {
                    hostText.text = $"{ConnectionManager.Ip}";
                    sendButton.interactable = true;
                    connectionButton.interactable = false;
                });
            });
            
            connectionButton.onClick.AddListener(() =>
            {
                ConnectionManager.Connect(connectionInput.text, () =>
                {
                    hostButton.interactable = false;
                    sendButton.interactable = true;
                }, () =>
                {
                    LogToBoard($"Failed");
                });
            });

            ConnectionManager.OnMessage += (tag, message) =>
            {
                LogToBoard($"[{tag.ToString()}] {message}]");
            };
            
            sendButton.onClick.AddListener(() =>
            {
                TestMessage message = new TestMessage();
                message.text = textInput.text;
                ConnectionManager.Send(JsonConvert.SerializeObject(message), _connectionProtocol);
            });

            _connectionProtocol = ConnectionProtocol.Tcp;

            modeText.text = "TCP";
            switchButton.onClick.AddListener(() =>
            {
                if (_connectionProtocol == ConnectionProtocol.Tcp)
                {
                    _connectionProtocol = ConnectionProtocol.Udp;
                    modeText.text = "UDP";
                }
                else
                {
                    _connectionProtocol = ConnectionProtocol.Tcp;
                    modeText.text = "TCP";
                }
            });
        }

        private void LogToBoard(string text)
        {
            messageText.text += $"{text}\n";
        }
    }
}
