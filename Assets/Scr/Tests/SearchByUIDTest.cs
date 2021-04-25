using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http;
using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;


public class SearchByUIDTest : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        //
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/searchbyuid?q=" + UserProfile.UserID))
            {              
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
