using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public bool lockX = true, lockY = false, lockZ = true;
    private Vector3 startRotation;
    public bool PlockX = false, PlockY = true, PlockZ = false;
    private Vector3 startPosition;

    void Awake() {

        startRotation = transform.rotation.eulerAngles;
        startPosition = transform.position;
    }

    void LateUpdate()
    {
        Vector3 newRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(
            lockX ? startRotation.x : newRotation.x,
            lockY ? startRotation.y : newRotation.y,
            lockZ ? startRotation.z : newRotation.z
        );

        Vector3 newPosition = transform.position;
        transform.position = new Vector3(
            PlockX ? startPosition.x : newPosition.x,
            PlockY ? startPosition.y : newPosition.y,
            PlockZ ? startPosition.z : newPosition.z
        );
    }
}
