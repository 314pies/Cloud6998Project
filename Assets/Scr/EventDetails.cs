using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDetails : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public async void OnDropButtonClicked()
    {
        Debug.Log("OnDropButtonClicked");
    }

    public async void OnJoinButtonClicked()
    {
        Debug.Log("OnJoinButtonClicked");
    }
}
