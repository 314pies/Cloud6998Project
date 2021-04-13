using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserProfile : MonoBehaviour
{

    //This will be called when this page is enabled
    private void OnEnable()
    {
        UpdateUI("John","John@columbia.edu","Male");
    }

    [SerializeField]
    TMP_InputField NameInput, EmailInput,GenderInput;

    public async void OnUpdateButtonClicked() {
        Debug.Log(NameInput.text);
        Debug.Log(EmailInput.text);
        Debug.Log(GenderInput.text);
        UpdateUI("John", "John@columbia.edu", "Male");
    }

    public void UpdateUI(string Name, string Email, string Gender)
    {
        NameInput.text = Name;
        EmailInput.text = Email;
        GenderInput.text = Gender;

    }
}
