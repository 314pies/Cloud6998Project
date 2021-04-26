using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Net.Http;
using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.UI;


public class RecommendUserButton : MonoBehaviour
{
    public Image AvatarImg;
    public string userId;

    // Data Cached
    string UserName, Email, Gender;
    //

    public void OnEnable()
    {
        if(!string.IsNullOrEmpty(userId))
            Load(userId);
    }

    public async void Load(string _userId)
    {
        this.userId = _userId;

        var reqPar = "q=" + UserProfile.UserID; // uid1";//+ userID;
        Debug.Log(reqPar);
        using (var httpClient = new HttpClient())
        {
            Loading.ShowLoading("Loading User Profile...");
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
                    var _name = stuff.GetValue("name");
                    if (_name != null)
                        UserName = (string)_name;

                    var _email = stuff.GetValue("email");
                    if (_email != null)
                        Email = (string)_email;

                    var _gender = stuff.GetValue("gender");
                    if (_gender != null)
                    {
                        int gender = (int)_gender;
                        string gender_str = "";
                        if (gender == 0)
                        {
                            gender_str = "Male";
                        }
                        else if (gender == 1)
                        {
                            gender_str = "Female";
                        }
                        else if (gender == 2)
                        {
                            gender_str = "Non-binary";
                        }
                        else
                        {
                            gender_str = "Unknown";
                        }
                        Gender = gender_str;
                    }
                    else { Gender = "Unknown"; }

                    var _picture = stuff.GetValue("picture");
                    if (_picture != null)
                    {
                        StartCoroutine(LoadImage(AvatarImg, (string)_picture));
                    }
                }
                catch (Exception exp)
                {
                    Debug.Log(exp);
                }
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
