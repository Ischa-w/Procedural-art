using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickpreset : MonoBehaviour {
	public GameObject[] _preset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Alpha1)) {
			_preset [0].SetActive (true);
			_preset [1].SetActive (false);
			_preset [2].SetActive (false);
			_preset [3].SetActive (false);
			_preset [4].SetActive (false);
		}
		if (Input.GetKey (KeyCode.Alpha2)) {
			_preset [0].SetActive (false);
			_preset [1].SetActive (true);
			_preset [2].SetActive (false);
			_preset [3].SetActive (false);
			_preset [4].SetActive (false);
		}
		if (Input.GetKey (KeyCode.Alpha3)) {
			_preset [0].SetActive (false);
			_preset [1].SetActive (false);
			_preset [2].SetActive (true);
			_preset [3].SetActive (false);
			_preset [4].SetActive (false);
		}
		if (Input.GetKey (KeyCode.Alpha4)) {
			_preset [0].SetActive (false);
			_preset [1].SetActive (false);
			_preset [2].SetActive (false);
			_preset [3].SetActive (true);
			_preset [4].SetActive (false);
		}
		if (Input.GetKey (KeyCode.Alpha5)) {
			_preset [0].SetActive (false);
			_preset [1].SetActive (false);
			_preset [2].SetActive (false);
			_preset [3].SetActive (false);
			_preset [4].SetActive (true);
		}
	}
}
