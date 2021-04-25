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

public class MyEvents : MonoBehaviour
{
    public Transform RecommendsRoot;
    public GameObject EventCardTemplate;

    public async void OnEnable()
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/searchbyuid?q=" + UserProfile.UserID))
            {
                Loading.ShowLoading("Loading My Events...");
                var response = await httpClient.SendAsync(request);
                Debug.Log("MyEvents()");
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                Loading.CloseLoading();
                var eventIds = (JArray)JsonConvert.DeserializeObject(body);

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
}
