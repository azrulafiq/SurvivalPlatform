using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void Update()
    {
        transform.Rotate (Vector3.up * 120.0f * Time.deltaTime);
    }
}
