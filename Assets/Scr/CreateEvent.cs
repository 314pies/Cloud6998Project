using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http;

public class CreateEvent : MonoBehaviour
{
    [SerializeField]
    TMP_InputField WhenInput, HowManyInput, RestaurentAddressInput;


    public async void OnCreateButtonClicked()
    {
        Debug.Log(WhenInput.text);
        Debug.Log(HowManyInput.text);
        Debug.Log(RestaurentAddressInput.text);

        using (var httpClient = new HttpClient())
        {
            var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId=12345";

            using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                "https://uzeqgezy5e.execute-api.us-west-2.amazonaws.com/v1/test1?" + reqPar))
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
