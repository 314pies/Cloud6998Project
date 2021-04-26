using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Net.Http;
using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.UI;

public class UsersRecommend : MonoBehaviour
{
    private async void OnEnable()
    {
        var reqPar = "q=" + UserProfile.UserID; // uid1";//+ userID;
        Debug.Log(reqPar);
        using (var httpClient = new HttpClient())
        {
            Loading.ShowLoading("Loading User Profile...");
            Debug.Log(reqPar);
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://omx6f7pb2f.execute-api.us-west-2.amazonaws.com/v_0_0/recommendation?" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                try
                {
                    var stuff = (JArray)JsonConvert.DeserializeObject(body);
                    
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }
            }
        }
    }
}
