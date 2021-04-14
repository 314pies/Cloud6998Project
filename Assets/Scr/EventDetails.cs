using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http;
using Michsky.UI.ModernUIPack;


public class EventDetails : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(eid)
    {
        eid = eid.Replace(" ", "_"); 
        var reqPar = "q=" + eid;  // q=2021-04-11_16:29:59.389352

        using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/detail?" + reqPar))
        {
            var response = await httpClient.SendAsync(request);
            Debug.Log(response);
            string body = await response.Content.ReadAsStringAsync();
            Debug.Log(body);

            var stuff = (JObject)JsonConvert.DeserializeObject(body);
            Debug.Log(stuff["eventIds"][0]["userId"]);

            string eventId = stuff["eventIds"][0]["eventId"];
            string userId = stuff["eventIds"][0]["userId"];
            string restaurantId = stuff["eventIds"][0]["restaurantId"];
            string time = stuff["eventIds"][0]["time"];
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            string numPeople = stuff["eventIds"][0]["numPeople"];
            string userName = stuff["eventIds"][0]["userName"];
            string gender = stuff["eventIds"][0]["gender"];
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
