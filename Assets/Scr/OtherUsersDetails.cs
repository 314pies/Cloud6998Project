using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OtherUsersDetails : MonoBehaviour
{
    
    public Image AvatarImg;
    public TMP_Text Name, Email, Gender;

    public List<string> EventsJoined = new List<string>();


    public OtherUserEvents otherUserEvents;
    public async void OnShowEventClicked()
    {
        otherUserEvents.UserName.text = Name.text + "'s Events";
        otherUserEvents.EventList = EventsJoined;
        otherUserEvents.gameObject.SetActive(true);
    }
}
