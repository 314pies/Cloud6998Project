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
            Loading.ShowLoading("Searching...");
            var reqPar = "q=" + SearchInput.text;
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/search?" + reqPar))

            {
                var response = await httpClient.SendAsync(request);
                Loading.CloseLoading();
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                var stuff = (JObject)JsonConvert.DeserializeObject(body);
                var eventIds = (JArray)stuff["eventIds"];
                var eventIdList = eventIds.ToObject<List<string>>();
                Debug.Log(eventIdList);             
                if(eventIdList.Count != 0)
                {
                    searchResult.gameObject.SetActive(true);
                    searchResult.ShowResult(eventIdList);
                }
                else
                {
                    PopupManager.OpenPopup("", "No Result Found :(");
                }
            }
           
        }
    }

    private async void OnEnable()
    {
        LoadRecommend();
    }

    public GameObject EventCardTemplate;
    public Transform RecommendsRoot;
    public async void LoadRecommend()
    {
        using (var httpClient = new HttpClient())
        {
            var reqPar = "userId=" + UserProfile.UserID + "&zipcode=10003";
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/recommendation?" + reqPar))

            {
                var response = await httpClient.SendAsync(request);
                Loading.CloseLoading();
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                var stuff = (JObject)JsonConvert.DeserializeObject(body);
                var eventIds = (JArray)stuff["eventIds"];
                var eventIdList = eventIds.ToObject<List<string>>();

                foreach (var _eventId in eventIdList)
                {

                    var _cardInstance = (GameObject)Instantiate(EventCardTemplate);
                    _cardInstance.transform.SetParent(RecommendsRoot);
                    _cardInstance.transform.localScale = new Vector3(1, 1, 1);
                    var _eventCard = _cardInstance.GetComponent<EventCard>();
                    _eventCard.eventID = _eventId;
                    _eventCard.LoadEventDetails();
                }
            }

        }
    }
}
