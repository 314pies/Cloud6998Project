using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http;
using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CreateEvent : MonoBehaviour
{
    [SerializeField]
    TMP_InputField WhenInput, HowManyInput;
    [SerializeField]

    [System.Obsolete]
    TMP_InputField RestaurentAddressInput;

    [SerializeField]
    CustomDropdown RestaurentSelector;
    public Sprite RestaruentIcon;

    [SerializeField]
    TMP_InputField restaurentSearch;

    /// <summary>
    /// Called from restaurentSearch
    /// </summary>
    /// <param name="v"></param>
    public async void OnEndEditCalled(string v)
    {
        Debug.Log(restaurentSearch.text);

        Loading.ShowLoading("Searching For Restaurant...");
        using (var httpClient = new HttpClient())
        {
            var reqPar = restaurentSearch.text;
            Debug.Log(reqPar);
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
            "https://ir5pgsnsfk.execute-api.us-west-2.amazonaws.com/v_0_0/search?zipcode=10027&q=" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);
                Loading.CloseLoading();
                var stuff = (JArray)JsonConvert.DeserializeObject(body);

                RestaurentNameToID.Clear();
                var _newDropDownList = new List<CustomDropdown.Item>();
                foreach (var _rInfo in stuff)
                {
                    Debug.Log(_rInfo["name"] + ", " + _rInfo["id"]);
                    if (!RestaurentNameToID.ContainsKey((string)_rInfo["name"]))
                    {
                        RestaurentNameToID.Add((string)_rInfo["name"], (string)_rInfo["id"]);
                    }
                    _newDropDownList.Add(new CustomDropdown.Item() { itemIcon = RestaruentIcon, itemName = (string)_rInfo["name"] });
                }

                RestaurentSelector.dropdownItems = _newDropDownList;
                RestaurentSelector.SetupDropdown();
            }
        }
    }



    Dictionary<string, string> RestaurentNameToID = new Dictionary<string, string>() { };


    public void OnDropdownSelected(int itemID)
    {
        Debug.Log(RestaurentSelector.selectedText.text);
        Debug.Log(RestaurentSelector.selectedItemIndex);
       
    }

    public EventDetails eventDetails;
    public async void OnCreateButtonClicked()
    {
        Debug.Log(WhenInput.text);
        Debug.Log(HowManyInput.text);
        var _rName = RestaurentSelector.selectedText.text;
        string SelectedRestaurentID="";
        if (RestaurentNameToID.ContainsKey(_rName))
            SelectedRestaurentID = RestaurentNameToID[_rName];
        else
        {
            PopupManager.OpenPopup("", "Please pick a restaurant");
            return;
        }

        Debug.Log(SelectedRestaurentID);

        using (var httpClient = new HttpClient())
        {
            Loading.ShowLoading("Creating Event...");
            //var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId="+SelectedRestaurentID;
            var reqPar = "numPeople=" + HowManyInput.text + "&time=" + WhenInput.text + "&restaurantId=" + SelectedRestaurentID + "&userId=" + UserProfile.UserID;
            using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/create?" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);

                Loading.CloseLoading();
                var stuff = (JObject)JsonConvert.DeserializeObject(body);
                if ((string)stuff["statusCode"] == "200")
                {
                    string evendID = (string)stuff["body"];
                    Debug.Log("Create Success, eventID: " + evendID);
                    eventDetails.SetEventID(evendID);
                    eventDetails.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    PopupManager.OpenPopup("Sth Go Wrong", body);
                }
            }

        }
    }
}