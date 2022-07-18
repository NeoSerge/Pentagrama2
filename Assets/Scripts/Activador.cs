using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activador : MonoBehaviour
{

    public GameObject objeto;

    public void ActivaObjeto()
    {
        objeto.SetActive(true);
    }
}
