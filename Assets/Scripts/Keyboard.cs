using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{

    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";
    public TextMeshProUGUI texto;


    public string stringToEdit = "Hello World";
    private TouchScreenKeyboard keyboard;

    // Opens native keyboard
    void OnGUI()
    {
        
    }

    public void OpenKeyboard()
    {
        //Debug.Log("enter");
        texto.text = "enter" ;
        


    }
    public void CloseKeyboard()
    {
        
    }
}
