using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;

public class HomePage : MonoBehaviour
{
    [SerializeField]
    TMP_InputField SearchInput;
    [SerializeField]
    SearchResult searchResult;
    [SerializeField]
    public EventDetails eventDetails;

    public static HomePage Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;
    }

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
                var stuff = (JObject)JsonConvert.DeserializeObject(body);
                var eventIds = (JArray)stuff["eventIds"];
                var eventIdList = eventIds.ToObject<List<string>>();
                Debug.Log(eventIdList);
                searchResult.ShowResult(eventIdList);
                PopupManager.OpenPopup("API result", body);
            }
        }
    }
}
