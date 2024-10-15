using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputRepeatPassword;
    
    private const string USERS_KEY = "Users";

    private List<UserData> allUsers = new List<UserData>();

    public void CheckRegister()
    {
        allUsers = new List<UserData>();
        string username = inputUsername.text;
        string password = inputPassword.text;
        string repeatPassword = inputRepeatPassword.text;
        
        Debug.Log(username);
        
        string jsonUsers = "";
        if (PlayerPrefs.HasKey(USERS_KEY)) jsonUsers = PlayerPrefs.GetString(USERS_KEY);

        allUsers = JsonConvert.DeserializeObject<List<UserData>>(jsonUsers);

        if (jsonUsers != "")
        {
            foreach (var user in allUsers)
            {
                if (user.username.Equals(username))
                {
                    Debug.Log("Username Taken");
                    return;
                }
            }
        }

        if (!password.Equals(repeatPassword))
        {
            Debug.Log("Password did not match");
            return;
        }

        UserData newUser = new UserData() { username = username, password = password};
            
        allUsers.Add(newUser);
            
        Debug.Log("Registered account.");
            
        //save
            
        PlayerPrefs.SetString(USERS_KEY, JsonConvert.SerializeObject(allUsers));
    }
    
    
}

[Serializable]
public struct UserData
{
    public string username;
    public string password;
}