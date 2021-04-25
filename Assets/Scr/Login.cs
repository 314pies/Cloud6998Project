using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Login : MonoBehaviour
{
    [SerializeField]
    TMP_InputField UserIDInput;
    public void OnLoginPressed()
    {
        UserProfile.UserID = UserIDInput.text;
        StartCoroutine(WaitAndLoad());

    }

    public GameObject HomePage;
    IEnumerator WaitAndLoad()
    {
        Loading.ShowLoading("Loggin In");
        yield return new WaitForSeconds(Random.Range(0.5f,5.0f));
        HomePage.SetActive(true);
        gameObject.SetActive(false);
        Loading.CloseLoading();
        Debug.Log("Login Success, UserID: " + UserProfile.UserID);
    }
}
