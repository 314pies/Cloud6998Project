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

    //This will be called when this page is enabled
    private async void OnEnable()
    {
        //UpdateUI("John", "John@columbia.edu", "Male");

        var reqPar = "q=";//+ eventId;
        Debug.Log(reqPar);
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://omx6f7pb2f.execute-api.us-west-2.amazonaws.com/user_v1/detail?q=uid1"))
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

        int gender = -1;
        if (gender_str == "Male")
        {
            gender = 0;
        }
        else if (gender_str == "Female")
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
