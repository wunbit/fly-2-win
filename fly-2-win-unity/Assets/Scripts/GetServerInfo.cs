using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetServerInfo : MonoBehaviour
{
    public Text userName;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(GetUserName());
        userName.text = "";
    }

    IEnumerator SendUserName()
    {
        yield return null;
    }

    IEnumerator GetUserName()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/getUsers.php"))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //userName.text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
