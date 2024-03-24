using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fixes the rotation on the enemy canvas
public class FixedRotation : MonoBehaviour
{
    Quaternion rot;
    void Awake()
    {
        rot = transform.rotation;
    }

    //Holds the same rotation at all times
    void Update()
    {
        transform.rotation = rot;
    }
}
