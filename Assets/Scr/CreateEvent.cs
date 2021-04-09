using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateEvent : MonoBehaviour
{
    [SerializeField]
    TMP_InputField WhenInput, HowManyInput, RestaurentAddressInput;


    public void OnCreateButtonClicked()
    {
        Debug.Log(WhenInput.text);
        Debug.Log(HowManyInput.text);
        Debug.Log(RestaurentAddressInput.text);
    }
}
