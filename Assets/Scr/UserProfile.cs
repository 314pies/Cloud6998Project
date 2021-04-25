using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Net.Http;
using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class UserProfile : MonoBehaviour
{

    public static string UserID = "936b39a1-c98f-413e-b7d7-7968f227dd9a";
    //This will be called when this page is enabled
    private async void OnEnable()
    {

        var reqPar = "q=uid1";//+ userID;
        Debug.Log(reqPar);
        using (var httpClient = new HttpClient())
        {
            Loading.ShowLoading("Loading User Profile...");
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://omx6f7pb2f.execute-api.us-west-2.amazonaws.com/user_v1/detail?" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                try
                {
                    var stuff = (JObject)JsonConvert.DeserializeObject(body);
                    string name = (string)stuff["name"];
                    string email = (string)stuff["email"];
                    int gender = (int)stuff["gender"];
                    string gender_str = "";
                    if (gender == 0) {
                        gender_str = "Male";
                    }
                    else if(gender == 1)
                    {
                        gender_str = "Female";
                    }
                    else
                    {
                        gender_str = "Unknown";
                    }

                    UpdateUI(name, email, gender_str);
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }
            }
            Loading.CloseLoading();
        }
    }

    [SerializeField]
    TMP_InputField NameInput, EmailInput,GenderInput;

    public async void OnUpdateButtonClicked() {
        Debug.Log(NameInput.text);
        Debug.Log(EmailInput.text);
        Debug.Log(GenderInput.text);
        //UpdateUI("John", "John@columbia.edu", "Male");

        string name = NameInput.text;
        string email = EmailInput.text;
        string gender_str = GenderInput.text;
        gender_str = gender_str.ToLower();

        int gender = -1;
        if (gender_str == "male")
        {
            gender = 0;
        }
        else if (gender_str == "female")
        {
            gender = 1;
        }
        else
        {
            gender = 2;
        }

        //var reqPar = "name=" + name + "&email=" + email + "&gender=" + gender;
        var reqPar = "email=" + email + "&gender=" + gender;
        Debug.Log(reqPar);
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"),
            "https://omx6f7pb2f.execute-api.us-west-2.amazonaws.com/user_v1/update?userId=uid1&"+ reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                try
                {
                    var stuff = (JObject)JsonConvert.DeserializeObject(body);
                    
                    
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }

                PopupManager.OpenPopup("User Profile", "Updated Complete!");
            }
        }

    }

    public void UpdateUI(string Name, string Email, string Gender)
    {
        NameInput.text = Name;
        EmailInput.text = Email;
        GenderInput.text = Gender;

    }
}
