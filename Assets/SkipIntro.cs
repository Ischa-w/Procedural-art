using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntro : MonoBehaviour
{
    [SerializeField] float startTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource _audioSource = GetComponent<AudioSource>();
        _audioSource.time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
