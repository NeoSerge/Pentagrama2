using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionEntradas : MonoBehaviour
{
    [SerializeField] string nombreEscenaColision;

    private void OnCollisionEnter(Collision collision)
    {
        VariablesGlobales.DesactivaVariables();

        if (nombreEscenaColision == "PontAeri")
        {
            VariablesGlobales.toPontAeri = true;
        }

        else if (nombreEscenaColision == "Bar")
        {
            VariablesGlobales.toBar = true;
        }

        else if (nombreEscenaColision == "Escenary")
        {
            VariablesGlobales.toEscenary = true;
        }

        else if (nombreEscenaColision == "Kafe")
        {
            VariablesGlobales.toKafe = true;
        }

        else if (nombreEscenaColision == "Primavera")
        {
            VariablesGlobales.toPrimavera = true;
        }

        SceneManager.LoadScene("Lobby_Test");
        
    }
}
