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
    
    private async void OnShowEventClicked()
    {
        
    }
}
