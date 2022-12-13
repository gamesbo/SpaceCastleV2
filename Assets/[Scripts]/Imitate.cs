using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imitate : MonoBehaviour
{
    public Transform transformImitate;

    void FixedUpdate()
    {
        this.transform.position = Vector3.Slerp(new Vector3(transformImitate.position.x, transformImitate.position.y, transformImitate.transform.position.z), transform.position, 0.5f);
        transform.LookAt(transformImitate.transform.GetChild(0).position);
    }
}
