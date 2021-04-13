using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http;
using Michsky.UI.ModernUIPack;

public class CreateEvent : MonoBehaviour
{
    [SerializeField]
    TMP_InputField WhenInput, HowManyInput;
    [SerializeField]

    [System.Obsolete]
    TMP_InputField RestaurentAddressInput;

    [SerializeField]
    CustomDropdown RestaurentSelector;

    Dictionary<int, string> UISelectedItemIDToRestaurentID = new Dictionary<int, string>() {
        { 0,"rid1"},
        { 1,"rid2"}
    };

    public string SelectedRestaurentID
    {
        get
        {
            if (UISelectedItemIDToRestaurentID.ContainsKey(RestaurentSelector.selectedItemIndex))
                return UISelectedItemIDToRestaurentID[RestaurentSelector.selectedItemIndex];

            Debug.LogError("CreateEvent.SelectedRestaurentID() selectedItemIndex not found");
            return "rid1";
        }
    }

    public void OnDropdownSelected(int itemID)
    {
        Debug.Log(RestaurentSelector.selectedItemIndex);
    }

    public async void OnCreateButtonClicked()
    {
        Debug.Log(WhenInput.text);
        Debug.Log(HowManyInput.text);
        Debug.Log(SelectedRestaurentID);

        using (var httpClient = new HttpClient())
        {
            //var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId="+SelectedRestaurentID;
            var reqPar = "numPeople=" + HowManyInput.text + "&time=" + WhenInput.text + "&restaurantId=" + SelectedRestaurentID + "&userId=uid1";
            using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                "https://333f7sxvgg.execute-api.us-west-2.amazonaws.com/v1/create?" + reqPar))
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