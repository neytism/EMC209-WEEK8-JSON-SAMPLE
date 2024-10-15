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
        
        string jsonUsers = "";
        if (PlayerPrefs.HasKey(USERS_KEY)) jsonUsers = PlayerPrefs.GetString(USERS_KEY);

        if (jsonUsers != "")
        {
            allUsers = JsonConvert.DeserializeObject<List<UserData>>(jsonUsers);

            foreach (var user in allUsers)
            {
                if (user.username.Equals(username))
                {
                    if (user.password.Equals(password))
                    {
                        Debug.Log("Logged in.");
                    }
                    else
                    {
                        Debug.Log("Password incorrect.");
                    }

                    return;
                }
            }
            
            Debug.Log("User does not exist.");

        }
    }
    
}