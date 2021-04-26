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
        if(string.IsNullOrEmpty(UserIDInput.text) == false)
        {
            UserProfile.UserID = UserIDInput.text;
        }       
        StartCoroutine(WaitAndLoad());

    }

    public GameObject HomePage;
    IEnumerator WaitAndLoad()
    {
        Loading.ShowLoading("Logging In");
        yield return new WaitForSeconds(Random.Range(0.5f,4.5f));
        Loading.CloseLoading();
        HomePage.SetActive(true);
        gameObject.SetActive(false);
       
        Debug.Log("Login Success, UserID: " + UserProfile.UserID);
    }


    public void OnSigninWithGoogleClicked()
    {
        Application.OpenURL("https://wefoodiestest2.auth.us-west-2.amazoncognito.com/login?client_id=1dhvrvbcd6kfp3dp54h9j3kppi&response_type=code&scope=aws.cognito.signin.user.admin+email+openid+phone+profile&redirect_uri=https://www.google.com");
        StartCoroutine(WaitAndLoad());
    }
}
