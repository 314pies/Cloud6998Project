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
            _eventCard.eventID = _eventId;
            _eventCard.LoadEventDetails();
        }
    }
}
