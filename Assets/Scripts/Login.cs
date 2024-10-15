using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TextMeshProUGUI textError;
    
    private const string USERS_KEY = "Users";

    private List<UserData> allUsers = new List<UserData>();

    private void Start()
    {
        string jsonUsers = "";
        if (PlayerPrefs.HasKey(USERS_KEY)) jsonUsers = PlayerPrefs.GetString(USERS_KEY);

        if (jsonUsers != "")
        {
            allUsers = JsonConvert.DeserializeObject<List<UserData>>(jsonUsers);

            foreach (var user in allUsers)
            {
                Debug.Log(user.username);
            }
        }
        else
        {
            Debug.Log("No saved users!");
        }
    }

    public void CheckLogin()
    {
        string username = inputUsername.text;
        string password = inputPassword.text;
        
        if (string.IsNullOrEmpty(username))
        {
            ShowError("Username Required");
            return;
        }
        
        if (string.IsNullOrEmpty(password))
        {
            ShowError("Password Required");
            return;
        }

        
        string jsonUsers = "";
        if (PlayerPrefs.HasKey(USERS_KEY)) jsonUsers = PlayerPrefs.GetString(USERS_KEY);

        if (jsonUsers != "")
        {
            allUsers = JsonConvert.DeserializeObject<List<UserData>>(jsonUsers);

            foreach (var user in allUsers)
            {
                if (user.username.Equals(username))
                {
                    if (!user.password.Equals(password))
                    {
                        ShowError("Invalid Username or password.");
                        Debug.Log("Password incorrect.");
                    }
                    else
                    {
                        ShowError(show:false);
                        inputUsername.text = "";
                        inputPassword.text = "";
                        Debug.Log("Logged in.");
                    }

                    return;
                }
            }
            
            ShowError("Invalid Username or password.");

        }
        else
        {
            ShowError("Invalid Username or password.");
        }
    }
    
    private void ShowError(string error = "", bool show = true)
    {
        textError.rectTransform.parent.gameObject.SetActive(show);
        textError.text = $"Error: {error}";
    }

    
}