using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesGlobales : MonoBehaviour
{
    public static bool toPontAeri = false;
    public static bool toBar = false;
    public static bool toEscenary = false;
    public static bool toKafe = false;
    public static bool toPrimavera = false;

    public static void DesactivaVariables()
    {
        toPontAeri = false;
        toBar = false;
        toEscenary = false;
        toKafe = false;
        toPrimavera = false;
    }
}
