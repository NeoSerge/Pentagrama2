using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObj1 : MonoBehaviour
{
    public float speed = 5;
    void Update()
    {
        transform.Rotate(Vector3.down * speed);
    }
}
