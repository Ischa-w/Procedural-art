using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoorbeeldPeerPlay : MonoBehaviour
{
    [SerializeField] AudioPeer audioPeer;
    
    void Start()
    {
        float lowTones = audioPeer._audioBand[0];
        float highTones = audioPeer._audioBand[7];
        float volume = audioPeer._Amplitude;
    }

}
