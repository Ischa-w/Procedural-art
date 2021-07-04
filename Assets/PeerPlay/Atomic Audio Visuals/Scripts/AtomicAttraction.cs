// This script is created by Peter Olthof from PeerPlay
// WWW.PEERPLAY.NL
// v1.02

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomicAttraction : MonoBehaviour {
	public AudioPeer _audioPeer;
	public GameObject _atom, _attractor;
	GameObject[] _attractorArray;
	GameObject[] _atomArray;
	float[] _AtomicSphereScaleSet;
	public Material _material;
	Material[] _sharedMaterial;
	Color[] _sharedColor;
	public Gradient _gradient;

	public int[] _attractPoints;
	public int _amountOfAtomsPerPoint;
	public float _scaleAttractPoints;
	public Vector3 _spacingDirection;
	public float _spacingBetweenAttractPoints, _strengthOfAttraction, _maxMagnitude, _randomPosDistance;
	public bool _useGravity;

	public bool _randomAtomScale;
	public float _AtomScale,_AtomRandomScaleMin,_AtomRandomScaleMax;
	public bool _invertDirection;
	public float _audioScaleMultiplier, _audioEmissionMultiplier;

	[Range(0.0f,1.0f)]
	public float _tresholdEmission;

	float[] _audioBandEmissionTreshold;
	float[] _audioBandEmissionColor;
	float[] _audioBandScale;

	public enum _emissionTreshold {Buffered, NoBuffer};
	public _emissionTreshold emissionTreshold = new _emissionTreshold ();
	public enum _emissionColor {Buffered, NoBuffer};
	public _emissionColor emissionColor = new _emissionColor ();
	public enum _atomScale {Buffered, NoBuffer};
	public _atomScale atomScale = new _atomScale ();

	public bool _animatePos;
	public AnimationCurve _animationCurve;
	public Vector3 _from, _to;
	float _animTimer;
	public float _posAnimSpeed;
	public int _posAnimBand;
	public bool _posAnimBuffered;


	//showing in editor attractor positions & colors
	private void OnDrawGizmos()
	{
		for (int i = 0; i < _attractPoints.Length; i++) {
			float evaluateStep = 1.0f / _attractPoints.Length;
			Color color = _gradient.Evaluate (evaluateStep * i);
			Gizmos.color = color;
	
			Vector3 pos = new Vector3 (transform.position.x + (_spacingBetweenAttractPoints * i * _spacingDirection.x),
				transform.position.y + (_spacingBetweenAttractPoints * i * _spacingDirection.y),
				transform.position.z + (_spacingBetweenAttractPoints * i * _spacingDirection.z));
			Gizmos.DrawSphere (pos , _scaleAttractPoints);
		}
	}

		

	// Use this for initialization
	void Start () {
		int countAtomicSpheres = 0;

		_audioBandEmissionTreshold = new float[8];
		_audioBandEmissionColor = new float[8];
		_audioBandScale = new float[8];

		//define array lengths
		_attractorArray = new GameObject[_attractPoints.Length];
		_sharedMaterial = new Material[_attractPoints.Length];
		_atomArray = new GameObject[_attractPoints.Length * _amountOfAtomsPerPoint];
		_AtomicSphereScaleSet = new float[_attractPoints.Length * _amountOfAtomsPerPoint];
		_sharedColor = new Color[_attractPoints.Length];

		//instantiate attract points

		for (int i = 0; i < _attractPoints.Length; i++) {

			GameObject _attractorInstance = (GameObject)Instantiate (_attractor);
			_attractorArray [i] = _attractorInstance;

		
				_attractorInstance.transform.position = new Vector3 (transform.position.x + (_spacingBetweenAttractPoints * i * _spacingDirection.x),
					transform.position.y + (_spacingBetweenAttractPoints * i * _spacingDirection.y),
					transform.position.z + (_spacingBetweenAttractPoints * i * _spacingDirection.z));
	
			_attractorInstance.transform.parent = this.transform;
			_attractorInstance.transform.localScale = new Vector3 (_scaleAttractPoints, _scaleAttractPoints, _scaleAttractPoints);
			//set colors to the material
			Material _AtomicSphereMatInstance = new Material (_material);
            _AtomicSphereMatInstance.EnableKeyword("_EMISSION");
            _sharedMaterial[i] = _AtomicSphereMatInstance;
			float evaluateStep = 1.0f / _attractPoints.Length;
			_sharedColor [i] = _gradient.Evaluate (evaluateStep * i);
				

		for (int j = 0; j < _amountOfAtomsPerPoint; j++)
			{
				GameObject _AtomicInstance = (GameObject)Instantiate (_atom);
				_atomArray [countAtomicSpheres] = _AtomicInstance;
				_AtomicInstance.GetComponent<AttractTo> ()._attractedTo = _attractorArray [i].transform;
				_AtomicInstance.GetComponent<AttractTo> ()._strengthOfAttraction = _strengthOfAttraction;
				_AtomicInstance.GetComponent<AttractTo> ()._maxMagnitude = _maxMagnitude;
				if (_useGravity) {
					_AtomicInstance.GetComponent<Rigidbody> ().useGravity = true;
				} else {
					_AtomicInstance.GetComponent<Rigidbody> ().useGravity = false;
				}

				_AtomicInstance.transform.position = new Vector3 (
				_attractorArray [i].transform.position.x+ Random.Range (-_randomPosDistance, _randomPosDistance),
				_attractorArray [i].transform.position.y + Random.Range (-_randomPosDistance, _randomPosDistance),
				_attractorArray [i].transform.position.z + Random.Range (-_randomPosDistance, _randomPosDistance));

				if (_randomAtomScale)
				{
					float randomScale = Random.Range(_AtomRandomScaleMin,_AtomRandomScaleMax);
					_AtomicSphereScaleSet [countAtomicSpheres] = randomScale;
				}
				if (!_randomAtomScale)
				{
					_AtomicSphereScaleSet [countAtomicSpheres] = _AtomScale;
				}
	
				_AtomicInstance.GetComponent<MeshRenderer>().material = _sharedMaterial[i];
				_AtomicInstance.transform.parent = transform.parent.transform;
				countAtomicSpheres++;
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		SelectAudioValues ();
		AtomBehaviour ();
		AnimatePosition ();
	}

	void AtomBehaviour()
	{
		int countAtomicSpheres = 0;
		for (int i = 0; i < _attractPoints.Length; i++) {
			if (_audioBandEmissionTreshold[_attractPoints[i]] >= _tresholdEmission) {
				Color _audioColor = new Color (_sharedColor [i].r * _audioBandEmissionColor[_attractPoints[i]] * _audioEmissionMultiplier,
					_sharedColor [i].g * _audioBandEmissionColor[_attractPoints[i]] * _audioEmissionMultiplier,
					_sharedColor [i].b * _audioBandEmissionColor[_attractPoints[i]] * _audioEmissionMultiplier, 1);
				_sharedMaterial [i].SetColor ("_EmissionColor", _audioColor );
			}
			if (_audioBandEmissionTreshold[_attractPoints[i]] < _tresholdEmission) {
				Color _audioColor = new Color (0,0,0,1);
				_sharedMaterial [i].SetColor ("_EmissionColor", _audioColor );
			}
			for (int j = 0; j < _amountOfAtomsPerPoint; j++) {
				_atomArray [countAtomicSpheres].transform.localScale = new Vector3 (
					_AtomicSphereScaleSet [countAtomicSpheres] + _audioBandScale[_attractPoints[i]] * _audioScaleMultiplier,
					_AtomicSphereScaleSet [countAtomicSpheres] +_audioBandScale[_attractPoints[i]] * _audioScaleMultiplier,
					_AtomicSphereScaleSet [countAtomicSpheres] + _audioBandScale [_attractPoints[i]] * _audioScaleMultiplier);

				countAtomicSpheres++;
			}
		}
	}



	void AnimatePosition()
	{
		if (_animatePos) {
			if (_posAnimBuffered) {
				_animTimer += Time.deltaTime * _audioPeer._audioBandBuffer[_posAnimBand] * _posAnimSpeed;
			} else {
				_animTimer += Time.deltaTime * _audioPeer._audioBand[_posAnimBand] * _posAnimSpeed;
			}
			if (_animTimer >= 1) {
				_animTimer -= 1f;
			}
			float _alphaTime = _animationCurve.Evaluate (_animTimer);
			transform.position = Vector3.Lerp (_from, _to, _alphaTime);
		}
	}


	void SelectAudioValues()
	{
		//treshold
		if (emissionTreshold == _emissionTreshold.Buffered) 
		{
			for (int i = 0; i < 8; i++) {
				_audioBandEmissionTreshold [i] = _audioPeer._audioBandBuffer [i];
			}
		}
		if (emissionTreshold == _emissionTreshold.NoBuffer) 
		{
			for (int i = 0; i < 8; i++) {
				_audioBandEmissionTreshold [i] = _audioPeer._audioBand[i];
			}
		}

		//emission color
		if (emissionColor == _emissionColor.Buffered) 
		{
			for (int i = 0; i < 8; i++) {
				_audioBandEmissionColor [i] = _audioPeer._audioBandBuffer [i];
			}
		}
		if (emissionColor == _emissionColor.NoBuffer) 
		{
			for (int i = 0; i < 8; i++) {
				_audioBandEmissionColor [i] = _audioPeer._audioBand[i];
			}
		}

		//atom scale
		if (atomScale == _atomScale.Buffered) 
		{
			for (int i = 0; i < 8; i++) {
				_audioBandScale [i] = _audioPeer._audioBandBuffer [i];
			}
		}
		if (atomScale == _atomScale.NoBuffer) 
		{
			for (int i = 0; i < 8; i++) {
				_audioBandScale [i] = _audioPeer._audioBand[i];
			}
		}

	}
}
