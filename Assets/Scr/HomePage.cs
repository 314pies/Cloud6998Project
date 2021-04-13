using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomePage : MonoBehaviour
{
    [SerializeField]
    TMP_InputField SearchInput;

    public async void OnSearchButtonClicked()
    {
        Debug.Log(SearchInput.text);
    }
}
