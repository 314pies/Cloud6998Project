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

public class EventDetails : MonoBehaviour
{

    public string eventId = "2021-04-11_16:29:59.389352";


    public TMP_Text PeopleJoinText, TimeText;
    public void SetEventID(string eventId)
    {
        this.eventId = eventId;
    }

    // Start is called before the first frame update
    public async void OnEnable()
    {
        var reqPar = "q=" + eventId;

        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/detail?" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);                
                try
                {
                    var stuff = (JObject)JsonConvert.DeserializeObject(body);
                    string eventId = (string)stuff["eventIds"][0]["eventId"];
                    string userId = (string)stuff["eventIds"][0]["userId"];
                    string restaurantId = (string)stuff["eventIds"][0]["restaurantId"];
                    string time = (string)stuff["eventIds"][0]["time"];
                    string numPeople = (string)stuff["eventIds"][0]["numPeople"];
                    string userName = (string)stuff["eventIds"][0]["userName"];
                    string gender = (string)stuff["eventIds"][0]["gender"];

                    PeopleJoinText.text = numPeople + "people joined";
                    TimeText.text = time;
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }
            }
        }
    }

    public async void OnDropButtonClicked()
    {
        Debug.Log("OnDropButtonClicked");

        using (var httpClient = new HttpClient())
        {
            //var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId="+SelectedRestaurentID;
            var reqPar = "eid=2021-04-05_10:28:33.666930" + "&userId=uid1";
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/drop?" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);

                PopupManager.OpenPopup("API result", body);
            }
        }

    }

    public async void OnJoinButtonClicked()
    {
        Debug.Log("OnJoinButtonClicked");

        using (var httpClient = new HttpClient())
        {
            //var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId="+SelectedRestaurentID;
            var reqPar = "eid=2021-04-05_10:28:33.666930" + "&userId=uid1";
            using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/join?" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);

                PopupManager.OpenPopup("API result", body);
            }
        }
    }
}
