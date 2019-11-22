using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.A)) transform.position += Vector3.up * Time.deltaTime;
        if(Input.GetKey(KeyCode.D)) transform.position += Vector3.up * Time.deltaTime * -1;
    }
}
