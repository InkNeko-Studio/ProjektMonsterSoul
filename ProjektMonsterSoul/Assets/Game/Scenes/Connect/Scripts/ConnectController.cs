using System;
using Framework.Connection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scenes.Connect.Scripts
{
    [Serializable]
    public class ConnectControllerData
    {
        public Action OnClose;
        public Action OnLobby;
    }
    
    public class ConnectController : MonoBehaviour
    {
        private static readonly string SceneName = "Connect";
        private static ConnectControllerData _data;
        
        [Header("General")]
        [SerializeField] private Button closeButton;
        
        [Header("Screens")]
        [SerializeField] private CanvasGroup selectScreen;
        [SerializeField] private CanvasGroup hostScreen;
        [SerializeField] private CanvasGroup connectScreen;
        [SerializeField] private CanvasGroup lobbyScreen;
        [SerializeField] private CanvasGroup loadingScreen;

        [Header("Select")]
        [SerializeField] private Button hostButton;
        [SerializeField] private Button connectButton;
        
        [Header("Host")]
        [SerializeField] private TMP_Text ipText;
        [SerializeField] private Button hostLobbyButton;
        
        [Header("Connect")]
        [SerializeField] private TMP_InputField ipInput;
        [SerializeField] private Button connectToIpButton;
        
        [Header("Lobby")]
        [SerializeField] private Button lobbyButton;

        public static void Show(ConnectControllerData data)
        {
            SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            _data = data;
        }
        
        public static void Hide()
        {
            SceneManager.UnloadSceneAsync(SceneName);
        }
        
        private void Start()
        {
            if (_data == null)
            {
                _data = new ConnectControllerData();
                _data.OnLobby += () => { Debug.Log("OnLobby"); };
                _data.OnClose += () => { Debug.Log("OnClose"); };
            }
            
            closeButton.onClick.AddListener(() =>
            {
                _data.OnClose?.Invoke();
                Hide();
            });
            
            HideAllScreens();
            selectScreen.alpha = 1f;
            selectScreen.interactable = true;
            selectScreen.blocksRaycasts = true;
            
            hostButton.onClick.AddListener(() =>
            {
                selectScreen.alpha = 0f;
                loadingScreen.alpha = 1f;
                loadingScreen.interactable = true;
                loadingScreen.blocksRaycasts = true;
                ConnectionManager.HostGame(() =>
                {
                    HideAllScreens();
                    hostScreen.alpha = 1f;
                    hostScreen.interactable = true;
                    hostScreen.blocksRaycasts = true;
                    ipText.text = ConnectionManager.Ip;
                });
            });
            
            connectButton.onClick.AddListener(() =>
            {
                HideAllScreens();
                connectScreen.alpha = 1f;
                connectScreen.interactable = true;
                connectScreen.blocksRaycasts = true;
            });
            
            hostLobbyButton.onClick.AddListener(() =>
            {
                _data.OnLobby?.Invoke();
                Hide();
            });
            
            connectToIpButton.onClick.AddListener(() =>
            {
                HideAllScreens();
                loadingScreen.alpha = 1f;
                loadingScreen.interactable = true;
                loadingScreen.blocksRaycasts = true;
                
                ConnectionManager.Connect(ipInput.text, () =>
                {
                    HideAllScreens();
                    lobbyScreen.alpha = 1f;
                    lobbyScreen.interactable = true;
                    lobbyScreen.blocksRaycasts = true;
                }, () =>
                {
                    HideAllScreens();
                    connectScreen.alpha = 1f;
                    connectScreen.interactable = true;
                    connectScreen.blocksRaycasts = true;
                });
            });
            
            lobbyButton.onClick.AddListener(() =>
            {
                _data.OnLobby?.Invoke();
                Hide();
            });
        }

        private void HideAllScreens()
        {
            selectScreen.alpha = 0f;
            selectScreen.interactable = false;
            selectScreen.blocksRaycasts = false;
            hostScreen.alpha = 0f;
            hostScreen.interactable = false;
            hostScreen.blocksRaycasts = false;
            connectScreen.alpha = 0f;
            connectScreen.interactable = false;
            connectScreen.blocksRaycasts = false;
            lobbyScreen.alpha = 0f;
            lobbyScreen.interactable = false;
            lobbyScreen.blocksRaycasts = false;
            loadingScreen.alpha = 0f;
            loadingScreen.interactable = false;
            loadingScreen.blocksRaycasts = false;
        }
    }
}
