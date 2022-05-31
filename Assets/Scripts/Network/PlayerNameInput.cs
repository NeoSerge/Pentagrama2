using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiscoVR;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private NetworkGamePlayerDisco networkmanager = null;

    [Header("UI")]
    [SerializeField] TMP_InputField nameInputField = null;
    [SerializeField] Button continueButton = null;

    public static string DisplayName { get; private set; }
    private const string PlayerPrefsNameKey = "PlayerName";

    private void Start()
    {
        SetUpInputField();
    }
    private void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }
        string defaulName = PlayerPrefs.GetString(PlayerPrefsNameKey);

        nameInputField.text = defaulName;
        SetPlayerName(defaulName);
    }

    public void SetPlayerName(string name)
    {
        continueButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePlayerName()
    {
        DisplayName = nameInputField.text;
        PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);

    }


}
