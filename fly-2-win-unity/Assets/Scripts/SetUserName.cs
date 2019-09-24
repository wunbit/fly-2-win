using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetUserName : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popup;
    public InputField inputUsername;
    public Text errorText;
    
    void Awake()
    {
        popup = GameObject.Find("popup");
        popup.SetActive(false);
    }

    public void OpenPopup()
    {
        popup.SetActive(true);
    }

    public void ClosePopup()
    {
        popup.SetActive(false);
    }

    public void SetUserButton()
    {
        StartCoroutine(SetUser());
        //Debug.Log(inputUsername.text);
    }

    IEnumerator SetUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", inputUsername.text);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/SetUser.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            { 
                string setUserResult = www.downloadHandler.text;
                if (setUserResult == "Success")
                {
                    errorText.text = "Success";
                    yield return new WaitForSeconds(1);
                    popup.SetActive(false);
                    errorText.text = "";
                    StaticVars.currentUser = inputUsername.text;
                }
                else
                {
                    errorText.text = "No user found";
                    yield return new WaitForSeconds(1);
                    errorText.text = "";
                }
            }
        }
    }
}
