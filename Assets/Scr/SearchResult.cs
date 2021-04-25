using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class SearchResult : MonoBehaviour
{

    public GameObject Template;
    public Transform CardsRoot;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result">List of event details id</param>
    public async void ShowResult(List<string> result)
    {
        foreach (var a in result) { Debug.Log(a); }
        foreach (var _eventId in result)
        {
            var _cardInstance = (GameObject)Instantiate(Template);
            _cardInstance.transform.SetParent(CardsRoot);
            var _eventCard = _cardInstance.GetComponent<EventCard>();
            LoadCard(_eventId, _eventCard);
        }
    }

    public async void LoadCard(string eventId, EventCard eventCard) {
        //Load
        var reqPar = "q=" + eventId;
        Debug.Log(reqPar);
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/detail?" + reqPar))
            {
                Loading.ShowLoading("Loading Event Details...");
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                try
                {
                    var stuff = (JObject)JsonConvert.DeserializeObject(body);
                    string _eventId = (string)stuff["eventIds"][0]["eventId"];
                    string userId = (string)stuff["eventIds"][0]["userId"];
                    string restaurantId = (string)stuff["eventIds"][0]["restaurantId"];
                    string time = (string)stuff["eventIds"][0]["time"];
                    string numPeople = (string)stuff["eventIds"][0]["numPeople"];
                    string userName = (string)stuff["eventIds"][0]["userName"];
                    string gender = (string)stuff["eventIds"][0]["gender"];

                    string timeText = time;
                    try
                    {
                        System.DateTime dateTime = System.DateTime.Parse(time);
                        timeText = string.Format("{0:D2}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                    }
                    catch { }


                    eventCard.RestaurentName.text = "Resta ID" + restaurantId;
                    eventCard.PeopleJoin.text = numPeople + " people joined";
                    eventCard.Time.text = timeText;
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }
                Loading.CloseLoading();
            }
        }
    }
}
