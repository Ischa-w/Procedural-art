using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBackgroundOnMusic : MonoBehaviour
{
    [SerializeField] AudioPeer audioPeerDrum;

    [SerializeField] float SineAmplitudeIntensity;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        //amplitude
        rend.material.SetFloat("Vector1_ED049F74", audioPeerDrum._audioBandBuffer[1] * SineAmplitudeIntensity);
    }
}
