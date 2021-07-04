using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    float rotationSpeed = 1f;
    float rotation = 0f;
    public float moveSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spiralSpawner = GameObject.Find("SpiralSpawner");
        moveSpeed = spiralSpawner.GetComponent<SpiralV1>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject spiralSpawner = GameObject.Find("SpiralSpawner");
        moveSpeed = spiralSpawner.GetComponent<SpiralV1>().velocity;
        transform.position = transform.position + (new Vector3(0, 0, moveSpeed) * Time.deltaTime);

        transform.rotation = Quaternion.Euler( new Vector3(0, 0, rotation));
        rotation += rotationSpeed;
    }
}
