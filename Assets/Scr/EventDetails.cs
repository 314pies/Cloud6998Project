using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http;
using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class EventDetails : MonoBehaviour
{

    public string eventId = "2021-04-11_16:29:59.389352";


    public TMP_Text PeopleJoinText, TimeText, RestaurentName;
    public Image restaurentImage;

    public void SetEventID(string eventId)
    {
        this.eventId = eventId;
    }

    // Start is called before the first frame update
    public async void OnEnable()
    {
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
                    string eventId = (string)stuff["eventIds"][0]["eventId"];
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



                    PeopleJoinText.text = numPeople + " people";
                    TimeText.text = timeText;
                    LoadRestaurentDetails(restaurantId);
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }
                Loading.CloseLoading();
            }
        }

        CheckEventJoinStatus(UserProfile.UserID);
    }

    public async void OnDropButtonClicked()
    {
        Debug.Log("OnDropButtonClicked");

        using (var httpClient = new HttpClient())
        {
            //var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId="+SelectedRestaurentID;
            var reqPar = "eid=" + eventId + "&userId=" + UserProfile.UserID;
            Debug.Log("reqPar: " + reqPar);
            using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/drop?" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                var _result = (JObject)JsonConvert.DeserializeObject(body);

                if((string)_result["statusCode"] == "200")
                {
                    PopupManager.OpenPopup("Drop Success", "");
                }
                else
                {
                    PopupManager.OpenPopup("Sth Go Wrong", body);
                }             
            }
        }

    }

    public async void OnJoinButtonClicked()
    {
        Debug.Log("OnJoinButtonClicked");

        using (var httpClient = new HttpClient())
        {
            //var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId="+SelectedRestaurentID;
            var reqPar = "eid=" + eventId + "&userId=" + UserProfile.UserID;
            using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/join?" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                var _result = (JObject)JsonConvert.DeserializeObject(body);

                if ((string)_result["statusCode"] == "200")
                {
                    PopupManager.OpenPopup("Join Success", "");
                }
                else
                {
                    PopupManager.OpenPopup("Sth Go Wrong", body);
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


    public GameObject JoinButton, DropButton;
    public async void CheckEventJoinStatus(string userId)
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/searchbyuid?q=" + userId))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log("CheckEventJoinStatus()");
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);

                var eventIds = (JArray)JsonConvert.DeserializeObject(body);
                var eventIdList = eventIds.ToObject<List<string>>();
                bool _isJoin = false;
                foreach (var _eventId in eventIdList)
                {
                    if (_eventId == eventId)
                    {
                        _isJoin = true;
                    }
                }
                JoinButton.SetActive(!_isJoin);
                DropButton.SetActive(_isJoin);
            }
        }

    }
}
