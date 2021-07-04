using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSizeMusic : MonoBehaviour
{
    private AudioPeer audioPeer;
    [HideInInspector] public int band;
    
    private bool is64 = false;


    // Start is called before the first frame update
    void Start()
    {
        var parent = gameObject.GetComponentInParent<spawnCubes>();
        audioPeer = parent.audioPeer;
        is64 = parent.is64;
    }

    // Update is called once per frame
    void Update()
    {
        if (is64)
        {
            
            band64();
        }
        else
        {
            band8();
        }
    }

    void band8()
    {
        transform.localScale = new Vector3(1, audioPeer._audioBandBuffer[band] * 10, 1);
    }

    void band64()
    {
        gameObject.transform.localScale = new Vector3(1, audioPeer._audioBandBuffer64[band] * 10, 1);
    }
}
