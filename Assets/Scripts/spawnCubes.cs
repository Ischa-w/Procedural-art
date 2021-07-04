using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCubes : MonoBehaviour
{

    public bool is64 = false;
    public AudioPeer audioPeer;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = transform.position;
        if (is64)
        {
            Debug.Log("Is 64");
            for (int i = 0; i < 64; i++)
            {
                //GameObject cube = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), position, Quaternion.identity, gameObject.transform);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = position;
                cube.transform.rotation = Quaternion.identity;
                cube.transform.SetParent(gameObject.transform);

                position += new Vector3(1, 0, 0);

                cube.AddComponent<cubeSizeMusic>();

                cube.GetComponent<cubeSizeMusic>().band = i;
            }
        }
        else
        {
            Debug.Log("Is 8");
            for (int i = 0; i < 8; i++)
            {
                //GameObject cube = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), position, Quaternion.identity, gameObject.transform);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = position;
                cube.transform.rotation = Quaternion.identity;
                cube.transform.SetParent(gameObject.transform);

                position += new Vector3(1, 0, 0);

                cube.AddComponent<cubeSizeMusic>();

                cube.GetComponent<cubeSizeMusic>().band = i;
            }
        }
    }
}
