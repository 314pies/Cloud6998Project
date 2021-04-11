using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopupManager : MonoBehaviour
{
    [Header("Popup Window Components")]
    [SerializeField]
    GameObject PopupRoot;
    [SerializeField]
    TMP_Text Title,Body;


    public static PopupManager Singleton { get; private set; }
    const string prefabPath = "Canvas_Popup";
    static void checkSingleton()
    {
        if (Singleton == null)
        {
            var instance = (GameObject)Instantiate(Resources.Load(prefabPath));
            Singleton = instance.GetComponent<PopupManager>();
        }
    }
    
    /// <summary>
    /// Open popup
    /// </summary>
    /// <param name="title"></param>
    /// <param name="bodyMsg"></param>
    public static void OpenPopup(string title, string bodyMsg)
    {
        checkSingleton();
        Singleton.PopupRoot.SetActive(true);
        Singleton.Title.text = title;
        Singleton.Body.text = bodyMsg;
    }
}
