using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PostProcessingOnMusic : MonoBehaviour
{

    [SerializeField] AudioPeer audioPeerDrum;
    [SerializeField] AudioPeer audioPeerJuno;
    Volume postprocesVolume;

    Bloom bloom;
    ColorAdjustments colorAdj;

    [SerializeField] float bloomIntensity = 1;
    [SerializeField] float beatBloomMultiply = .5f;

    [SerializeField] float colorChangeOnJuno = 1f;

    float[] colorAdjArray = new float[60];
    float colorAdjAvarage;

    int j = 0;

    //[SerializeField] float minBeatTime = .2f;
    float timerr = 1f;
    // Start is called before the first frame update
    void Start()
    {
        postprocesVolume = GetComponent<Volume>();
        postprocesVolume.profile.TryGet(out bloom);
        postprocesVolume.profile.TryGet(out colorAdj);
    }

    // Update is called once per frame
    void Update()
    {
        bloom.intensity.value = 
            (audioPeerDrum._audioBandBuffer[1] + audioPeerDrum._audioBandBuffer[2]) * beatBloomMultiply + bloomIntensity;

        //read high or low and add to array
        colorAdjArray[j] = ( ( audioPeerJuno._audioBandBuffer[0] + audioPeerJuno._audioBandBuffer[1] + audioPeerJuno._audioBandBuffer[2] )
            - (audioPeerJuno._audioBandBuffer[3] + audioPeerJuno._audioBandBuffer[4] + audioPeerJuno._audioBandBuffer[5] 
            + audioPeerJuno._audioBandBuffer[6] + audioPeerJuno._audioBandBuffer[7]) )
            * colorChangeOnJuno;
        j++;
        if (j>=colorAdjArray.Length) j = 0;

        //average of array of color adjustment
        for (var i = 0; i < colorAdjArray.Length; i++)
        {
            colorAdjAvarage += colorAdjArray[i];
        }
        colorAdjAvarage = colorAdjAvarage / colorAdjArray.Length;

        //adjust color
        colorAdj.hueShift.value = colorAdjAvarage;

        //Change on drum
        /*if ((audioPeerDrum._audioBandBuffer[1] + audioPeerDrum._audioBandBuffer[2]) / 2 > .55f && timerr < 0)
        {
            timerr = minBeatTime;
        }
        timerr -= Time.deltaTime;*/
        //Debug.Log((audioPeerDrum._audioBandBuffer[1] + audioPeerDrum._audioBandBuffer[2]) / 2);
    }
}
