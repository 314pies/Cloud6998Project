using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventCard : MonoBehaviour
{
    public TMP_Text Time, PeopleJoin, RestaurentName;
    public string eventID;
    public void OnCardClicked()
    {
        Debug.Log("EventCard OnCardClicked();");
        HomePage.Singleton.eventDetails.SetEventID(eventID);
        HomePage.Singleton.eventDetails.gameObject.SetActive(true);
    }
}
