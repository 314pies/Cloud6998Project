using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using Michsky.UI.ModernUIPack;

public class HomePage : MonoBehaviour
{
    [SerializeField]
    TMP_InputField SearchInput;

    public async void OnSearchButtonClicked()
    {
        Debug.Log(SearchInput.text);

        using (var httpClient = new HttpClient())
        {
            
            var reqPar = "q=" + SearchInput.text;
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/search?" + reqPar))
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
