using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w") && transform.position.y < 4.7f)
        {
            transform.position += new Vector3(0f, movementSpeed, 0f);
        }

        if (Input.GetKey("s") && transform.position.y > -4.7f)
        {
            transform.position += new Vector3(0f, -movementSpeed, 0f);
        }

        if (Input.GetKey("a") && transform.position.x > -8.4f)
        {
            transform.position += new Vector3(-movementSpeed, 0f, 0f);
        }

        if (Input.GetKey("d") && transform.position.x < 8.4f)
        {
            transform.position += new Vector3(movementSpeed, 0f, 0f);
        }
    }
}
