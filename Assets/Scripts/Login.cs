using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    [SerializeField] TMP_InputField userField;
    [SerializeField] TMP_InputField passField;
    [SerializeField] TextMeshProUGUI notice;


    private void Start()
    {
        if (PlayerPrefs.GetString("user") != null)
        {
            userField.text = PlayerPrefs.GetString("user");
        }

    }

    public void SendLogin()
    {
        StartCoroutine(LoginUser());
    }


    IEnumerator LoginUser()
    {
        string user = userField.text;
        string pass = passField.text;

        string uri = "https://metaxsp.com/pentagrama/login.php";
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("password", pass);


        UnityWebRequest www = UnityWebRequest.Post(uri, form);

        yield return www.SendWebRequest();
        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log(www.error);
            notice.text = www.error;
            yield return new WaitForSeconds(5);
            notice.text = "";
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            notice.text = www.downloadHandler.text;
        }

        if (notice.text != "correcto")
        {
            notice.text = "The user name or password is incorrect";
            yield return new WaitForSeconds(5);
            notice.text = "";
        }
        else
        {
            PlayerPrefs.SetString("user", user);
            StartCoroutine(LoadAvatar());
        }
    }

    IEnumerator LoadAvatar()
    {
        string user = PlayerPrefs.GetString("user");

        string uri = "https://metaxsp.com/pentagrama/userurl.php";

        WWWForm form = new WWWForm();
        form.AddField("user", user);
        UnityWebRequest www = UnityWebRequest.Post(uri, form);
        yield return www.SendWebRequest();
        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log(www.error);
            notice.text = www.error;
            yield return new WaitForSeconds(5);
            notice.text = "";
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            PlayerPrefs.SetString("avatar", www.downloadHandler.text);
        }
    }

}
