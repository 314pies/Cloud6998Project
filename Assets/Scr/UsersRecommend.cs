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

    public GameObject[] UserButtons;
    private async void OnEnable()
    {
        var reqPar = "q=" + UserProfile.UserID; // uid1";//+ userID;
        Debug.Log(reqPar);
        using (var httpClient = new HttpClient())
        {
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
                    var _events = (JArray)JsonConvert.DeserializeObject(body);
                    //List<string> _userDetailsList = ((JArray)_events).ToObject<List<string>>();
                    var _index = 0;
                    foreach (var _userDetails in _events)
                    {
                        Debug.Log("Recommend User: " +_userDetails["id"]);
                        if (_index < UserButtons.Length)
                        {
                            UserButtons[_index].GetComponent<RecommendUserButton>().Load((string)_userDetails["id"]);
                        }
                        else
                        {
                            break;
                        }
                        _index++;
                    }
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }
            }
        }
    }
}
