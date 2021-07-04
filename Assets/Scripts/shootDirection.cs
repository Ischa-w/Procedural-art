using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0f;
        transform.LookAt(MousePos, Vector3.forward);
    }
}
