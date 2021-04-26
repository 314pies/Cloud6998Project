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
public class RestaurentInfo : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        var reqPar = "BBQ"; // uid1";//+ userID;
        using (var httpClient = new HttpClient())
        {
            Debug.Log(reqPar);
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://ir5pgsnsfk.execute-api.us-west-2.amazonaws.com/v_0_0/search?zipcode=10027&q=" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);

                var stuff = (JArray)JsonConvert.DeserializeObject(body);


                foreach (var a in stuff)
                {
                    Debug.Log(a);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
