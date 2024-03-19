using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteInterractor : MonoBehaviour
{
    public GameObject handle;
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = handle.transform.rotation;
    }

    public void DoorHandleActionRight()
    {
        handle.transform.Rotate(Vector3.right, -45f, Space.Self);
    }

    public void DoorHandleActionLeft()
    {
        handle.transform.Rotate(Vector3.right, 45f, Space.Self);
    }

    public void ResetHandleRotation()
    {
        handle.transform.rotation = originalRotation;
    }
}
