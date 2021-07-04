using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetSelect : MonoBehaviour
{
    public Transform[] _preset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            {
            _preset[0].gameObject.SetActive(true);
            _preset[1].gameObject.SetActive(false);
            _preset[2].gameObject.SetActive(false);
            _preset[3].gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
            {
            _preset[0].gameObject.SetActive(false);
            _preset[1].gameObject.SetActive(true);
            _preset[2].gameObject.SetActive(false);
            _preset[3].gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
            {
            _preset[0].gameObject.SetActive(false);
            _preset[1].gameObject.SetActive(false);
            _preset[2].gameObject.SetActive(true);
            _preset[3].gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
            {
            _preset[0].gameObject.SetActive(false);
            _preset[1].gameObject.SetActive(false);
            _preset[2].gameObject.SetActive(false);
            _preset[3].gameObject.SetActive(true);
        }
    }
}
