using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventCard : MonoBehaviour
{
    public TMP_Text Time, PeopleJoin, RestaurentName;
    public string eventID;
    
    public string restaurentID;
    public Image restaurentImage;

    public void OnCardClicked()
    {
        Debug.Log("EventCard OnCardClicked();");
        HomePage.Singleton.eventDetails.SetEventID(eventID);
        HomePage.Singleton.eventDetails.gameObject.SetActive(true);
    }

    private async void OnEnable()
    {
        //LoadEventDetails();
    }
    public async void LoadEventDetails()
    {
        //Load
        var reqPar = "q=" + eventID;
        Debug.Log(reqPar);
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
                    string _eventId = (string)stuff["eventIds"][0]["eventId"];
                    string userId = (string)stuff["eventIds"][0]["userId"];
                    string restaurantId = (string)stuff["eventIds"][0]["restaurantId"];
                    string time = (string)stuff["eventIds"][0]["time"];
                    string numPeople = (string)stuff["eventIds"][0]["numPeople"];
                    string joinedPeoleNum = (string)stuff["eventIds"][0]["joinedPeoleNum"];
                    string userName = (string)stuff["eventIds"][0]["userName"];
                    string gender = (string)stuff["eventIds"][0]["gender"];

                    string timeText = time;
                    try
                    {
                        System.DateTime dateTime = System.DateTime.Parse(time);
                        timeText = string.Format("{0:D2}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                    }
                    catch { }


                    //RestaurentName.text = "Resta ID" + restaurantId;
                    restaurentID = restaurantId;
                    LoadRestaurentDetails(restaurantId);
                    PeopleJoin.text = joinedPeoleNum + "/" + numPeople + " people joined";
                    Time.text = timeText;
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }
            }
        }
    }

    public async void LoadRestaurentDetails(string restrauntId)
    {
        using (var httpClient = new HttpClient())
        {

            //var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId="+SelectedRestaurentID;
            //var reqPar = "eH0bypB-IqUH73IVgEEfPA";
            var reqPar = restrauntId;

            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://ir5pgsnsfk.execute-api.us-west-2.amazonaws.com/v_0_0/searchbyrid?q=" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);


                var stuff = (JArray)JsonConvert.DeserializeObject(body);
                var restarName = stuff[0]["name"];
                var image_url = stuff[0]["image_url"];
                Debug.Log(restarName);
                Debug.Log(image_url);
                RestaurentName.text = (string)restarName;
                StartCoroutine(LoadImage(restaurentImage, (string)image_url));
            }

        }
    }
    IEnumerator LoadImage(Image img, string url)
    {
        WWW www = new WWW(url);
        yield return www;
        img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}
