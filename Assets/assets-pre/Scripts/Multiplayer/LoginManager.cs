using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName_InputName;
    [SerializeField] TMP_InputField userField;
    [SerializeField] TMP_InputField passField;
    [SerializeField] TextMeshProUGUI notice;
    #region  Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        //PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoginCheck()
    {
        print(userField.text);
        print(passField.text);
        StartCoroutine(ConnectToPhotonServer());
    }
    #endregion

    #region UI Callbacks Methods
    public void ConnectAnonnymously(){
        PhotonNetwork.ConnectUsingSettings();
    }

    public IEnumerator ConnectToPhotonServer(){
        if(userField != null){

            string user = userField.text;
            string pass = passField.text;

            string uri = "https://pentagrama.io/CellApp/login.php";
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
    }

    IEnumerator LoadAvatar()
    {
        string user = PlayerPrefs.GetString("user");

        string uri = "https://pentagrama.io/CellApp/userurl.php";

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
            PhotonNetwork.NickName = userField.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    #endregion

    #region Photon Callback Methods
    public override void OnConnected()
    {
        Debug.Log("OnConnected is called. The server is available");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master with player name: " + PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("HomeScene");
    }

    #endregion
}
