using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public async void OnEnable()
    {
       
        LoadRecommend();
        //LoadUserAvatar();
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
                Debug.Log("LoadRecommend, reqPar: " + reqPar);
                Loading.ShowLoading("Loading Recommendation...");
                var response = await httpClient.SendAsync(request);
                Loading.CloseLoading();
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Loading.CloseLoading();
                Debug.Log(body);
                var stuff = (JObject)JsonConvert.DeserializeObject(body);
                var eventIds = (JArray)stuff["eventIds"];
                if (eventIds == null) { return; }
                var eventIdList = eventIds.ToObject<List<string>>();
                
                foreach (Transform child in RecommendsRoot.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

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

    public ButtonManagerIcon UserProfileButton;
    public async void LoadUserAvatar()
    {
        var reqPar = "q=" + UserProfile.UserID; // uid1";//+ userID;
        Debug.Log(reqPar);
        using (var httpClient = new HttpClient())
        {
           
            Debug.Log(reqPar);
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://omx6f7pb2f.execute-api.us-west-2.amazonaws.com/user_v1/detail?" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                try
                {
                    var stuff = (JObject)JsonConvert.DeserializeObject(body);

                    var _picture = stuff.GetValue("picture");
                    if (_picture != null)
                    {
                        Debug.Log("Avatar URL: "+ _picture);
                        StartCoroutine(LoadAvatar((string)_picture));
                    }
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }
            }
        }
    }

    IEnumerator LoadAvatar(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        UserProfileButton.buttonIcon = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        UserProfileButton.UpdateUI();
    }
}
