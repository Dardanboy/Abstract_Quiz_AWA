﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AbstractQuizzStructure;

/**
 * ConnectionManager is the manager for the Connection page (used when user has to connect to the API and receive token API for example)
 */
public class LoginManager : PageLogic
{
    public Button connectButton;
    public Button registerButton;
    public InputField pseudoInput;
    public InputField passwordInput;

    void Start()
    {
        connectButton.onClick.AddListener(ConnectButtonClicked);
        registerButton.onClick.AddListener(RegisterButtonClicked);
    }

    void ConnectButtonClicked()
    {
        ApiToken apiTokenData = GameManager.Instance.GetApiManager().ConnectToApi(pseudoInput.text, passwordInput.text);

        Debug.Log(JsonUtility.ToJson(apiTokenData));

        if (apiTokenData == null)
        {
            Debug.LogError("[WARNING]: apiTokenData is null");
            return;
        }
        else
        {
            GameManager.Instance.GetApiManager().SetApiToken(apiTokenData.GetApiToken());
        }

        if (!GameManager.Instance.GetApiManager().IsApiTokenDefined() && 
            NetworkRequestManager.lastHttpWebRequestErrorMessage == null)
        {
            PopupManager.PopupAlert("Connection impossible", "Your login or password are not correct");
            passwordInput.text = "";
        }
        else if (GameManager.Instance.GetApiManager().GetApiToken() == null && NetworkRequestManager.lastHttpWebRequestErrorMessage != null)
        {
            PopupManager.PopupAlert("Connection impossible", "Error:" + NetworkRequestManager.lastHttpWebRequestErrorMessage);
        }
        else
        {
            GameManager.Instance.pagesManager.GoToPage("ScanQrCodePlease");
        }
    }

    void RegisterButtonClicked()
    {
        GameManager.Instance.pagesManager.GoToPage("Register");
    }
}
