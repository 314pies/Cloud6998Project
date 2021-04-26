using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OtherUserEvents : MonoBehaviour
{
    public TMP_Text UserName;
    public List<string> EventList = new List<string>();
    public Transform EventsRoot;
    public GameObject EventCardTemplate;

    private async void OnEnable()
    {
        var eventIdList = EventList;

        foreach (Transform child in EventsRoot.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var _eventId in eventIdList)
        {
            var _cardInstance = (GameObject)Instantiate(EventCardTemplate);
            _cardInstance.transform.SetParent(EventsRoot);
            _cardInstance.transform.localScale = new Vector3(1, 1, 1);
            var _eventCard = _cardInstance.GetComponent<EventCard>();
            _eventCard.eventID = _eventId;
            _eventCard.LoadEventDetails();
        }
    }
}
