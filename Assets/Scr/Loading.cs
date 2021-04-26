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
    const string prefabPath = "Canvas_Loading";
    static void checkSingleton()
    {
        if (Singleton == null)
        {
            var instance = (GameObject)Instantiate(Resources.Load(prefabPath));
            Singleton = instance.GetComponent<Loading>();
        }
    }

    public static void ShowLoading(string message)
    {
        checkSingleton();
        Singleton.loadingMessage.text = message;
        Singleton.Root.gameObject.SetActive(true);
    }

    public static void CloseLoading()
    {
        checkSingleton();
        Singleton.Root.gameObject.SetActive(false);
    }
}
