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
    void Start()
    {
        
    }

    public async void OnDropButtonClicked()
    {
        Debug.Log("OnDropButtonClicked");

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

    public async void OnJoinButtonClicked()
    {
        Debug.Log("OnJoinButtonClicked");

        using (var httpClient = new HttpClient())
        {
            //var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId="+SelectedRestaurentID;
            var reqPar = "eid=2021-04-05_10:28:33.666930" + "&userId=uid1";
            using (var request = new HttpRequestMessage(new HttpMethod("POST"),
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
}
