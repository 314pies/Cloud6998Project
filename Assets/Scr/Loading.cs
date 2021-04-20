using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Loading : MonoBehaviour
{

    [SerializeField]
    TMP_Text loadingMessage;

    public GameObject Root;

    public static Loading Singleton { get; private set; }
    private void Awake()
    {
        Singleton = this;
    }


    public static void ShowLoading(string message)
    {
        Singleton.loadingMessage.text = message;
        Singleton.Root.gameObject.SetActive(true);
    }

    public static void CloseLoading()
    {
        Singleton.Root.gameObject.SetActive(false);
    }
}
