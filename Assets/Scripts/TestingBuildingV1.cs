using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBuildingV1 : MonoBehaviour
{
    GameObject[] cube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            CreateA();
        }

        if (Input.GetKeyDown("s"))
        {
            CreateB();
        }
    }

    private void CreateA()
    {
        GameObject cubeA = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube[1] = cubeA;
    }
    private void CreateB()
    {
        for (int i = 0; i>cube.Length; i++)
        {

        }

    }
}
