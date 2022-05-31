using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DiscoVR;

namespace DiscoVR
{

    public class JoinLobbyMenu : MonoBehaviour
    {
        [SerializeField] private NetworkManagerDisco networkManager = null;
        [Header("UI")]
        [SerializeField] private GameObject landingPagePanel;
        [SerializeField] private TMP_InputField ipAddressInputField;
        [SerializeField] private Button joinButton;

        
        private void OnEnable()
        {
            NetworkManagerDisco.OnClientConnected += HandleClientConnected;
            NetworkManagerDisco.OnClientDisconnected += HandleClientDisconnected;        
        }
        private void OnDisable()
        {
            NetworkManagerDisco.OnClientConnected -= HandleClientConnected;
            NetworkManagerDisco.OnClientDisconnected -= HandleClientDisconnected;
        }
        
        public void JoinLobby()
        {
            string ipAddress = ipAddressInputField.text;

            networkManager.networkAddress = "localhost";
            networkManager.StartClient();

            joinButton.interactable = false;
        }

        private void HandleClientConnected()
        {
            joinButton.interactable = true;

            gameObject.SetActive(false);
            landingPagePanel.SetActive(false);
        }

        private void HandleClientDisconnected()
        {
            joinButton.interactable = true;
        }
    }
}