using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject jhon;

    // Start is called before the first frame update
    void Update()
    {
        Vector3 position = transform.position;
        position.x = jhon.transform.position.x;
        transform.position = position;
    }
}
