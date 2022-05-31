using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InsertarBD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IngresarDatos();
    }

 

    public void IngresarDatos()
    {
        StartCoroutine(DatosPost());
    }


    IEnumerator DatosPost()
    {
     
        string uri = "https://metaxsp.com/pentagrama/registro.php";
        WWWForm form = new WWWForm();
        form.AddField("user", "Rafla");
        form.AddField("password", "Esw31!8#Ja");
        form.AddField("email", "test3@hotmail.com");
        form.AddField("url", "https://d1a370nemizbjq.cloudfront.net/749a2c08-03de-4c55-8110-dc2d421396b6.glb");

        UnityWebRequest www = UnityWebRequest.Post(uri, form);

        yield return www.SendWebRequest();
        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
