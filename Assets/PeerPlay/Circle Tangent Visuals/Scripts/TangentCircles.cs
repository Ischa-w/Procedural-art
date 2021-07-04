using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TangentCircles : CircleTangent
{
    [Header("Setup")]
    public GameObject _circlePrefab;
    private GameObject _innerCircleGO, _outterCircleGO;
    private Vector4 _innerCircle, _outterCircle;
    public float _innerCircleRadius, _outerCircleRadius;
    private Vector4[] _tangentCircle;
    private GameObject[] _tangentObject;
    [Range(1, 64)]
    public int _circleAmount;
  
    
    [Header("Input")]
    [Range(0, 1)]
    public float _distOuterTangent;
    [Range(0, 1)]
    public float _movementSmooth;
    [Range(0.1f, 10f)]
    public float _radiusChangeSpeed;
    private float _radiusChange;
    private Vector2 _tsL, _tsLSmooth;

    [Header("Audio Visuals")]
    public AudioPeer _audioPeer;
    public Material _materialBase;
    private Material[] _material;
    public Gradient _gradient;
    [Range(0, 1)]
    public float _thresholdEmission;
    [Range(0, 4)]
    public float _emissionMultiplier;
    public bool _emissionBuffer;
    public bool _rotateBuffer;
    public float _rotateSpeed;
    private float _rotateTangentObjects;
    public bool _scaleYOnAudio;
    public bool _scaleBuffer;
    [Range(0, 1)]
    public float _thresholdScale;
    public float _scaleStart;
    public Vector2 _scaleMinMax;

    // Start is called before the first frame update
    void Awake()
    {
        _innerCircle = new Vector4(0, 0, 0, _innerCircleRadius);
        _outterCircle = new Vector4(0, 0, 0, _outerCircleRadius);
        _tangentCircle = new Vector4[_circleAmount];
        _tangentObject = new GameObject[_circleAmount];
        _material = new Material[_circleAmount];
        for (int i = 0; i < _circleAmount; i++)
        {
            GameObject tangentInstance = (GameObject)Instantiate(_circlePrefab);
            _tangentObject[i] = tangentInstance;
            _tangentObject[i].transform.parent = this.transform;

            _material[i] = new Material(_materialBase);
            _material[i].EnableKeyword("_EMISSION");
            if (_tangentObject[i].GetComponent<MeshRenderer>())
            {
                _tangentObject[i].GetComponent<MeshRenderer>().material = _material[i];
            }
            else
            {
                _tangentObject[i].transform.GetChild(0).GetComponent<MeshRenderer>().material = _material[i];
            }
        }
    }

    void PlayerInput()
    {
        _tsL = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _tsLSmooth = new Vector2(
            _tsLSmooth.x * (1 - _movementSmooth) + _tsL.x * _movementSmooth,
            _tsLSmooth.y * (1 - _movementSmooth) + _tsL.y * _movementSmooth);

       // _radiusChange = Input.GetAxis("TriggerL") - Input.GetAxis("TriggerR");


        _innerCircle = new Vector4(
            (_tsLSmooth.x * (_outterCircle.w - _innerCircle.w) * (1 - _distOuterTangent)) + _outterCircle.x,
            0.0f,
            (_tsLSmooth.y * (_outterCircle.w - _innerCircle.w) * (1 - _distOuterTangent)) + _outterCircle.z,
            _innerCircle.w + (_radiusChange * Time.deltaTime * _radiusChangeSpeed));
    }

    // Update is called once per frame
    void Update()
    {

        PlayerInput();
        if (_rotateBuffer)
        {
            _rotateTangentObjects += _rotateSpeed * Time.deltaTime * _audioPeer._AmplitudeBuffer;
        }
        else
        {
            _rotateTangentObjects += _rotateSpeed * Time.deltaTime * _audioPeer._Amplitude;
        }
        for (int i = 0; i < _circleAmount; i++)
        {
            _tangentCircle[i] = FindTangentCircle(_outterCircle, _innerCircle, (360f / _circleAmount) * i + _rotateTangentObjects);
            _tangentObject[i].transform.localPosition = new Vector3(_tangentCircle[i].x, _tangentCircle[i].y, _tangentCircle[i].z);

            if (_scaleYOnAudio)
            {
                if (_audioPeer._audioBandBuffer64[i] > _thresholdScale)
                {
                    if (_scaleBuffer)
                    {
                        _tangentObject[i].transform.localScale = new Vector3(_tangentCircle[i].w * 2, _scaleStart + Mathf.Lerp(_scaleMinMax.x, _scaleMinMax.y, _audioPeer._audioBandBuffer64[i]), _tangentCircle[i].w * 2);
                    }
                    else
                    {
                        _tangentObject[i].transform.localScale = new Vector3(_tangentCircle[i].w * 2, _scaleStart + Mathf.Lerp(_scaleMinMax.x, _scaleMinMax.y, _audioPeer._audioBand64[i]), _tangentCircle[i].w * 2);
                    }
                }
                else
                {
                    _tangentObject[i].transform.localScale = new Vector3(_tangentCircle[i].w * 2, _scaleStart, _tangentCircle[i].w * 2);
                }

            }
            else
            {
                _tangentObject[i].transform.localScale = new Vector3(_tangentCircle[i].w * 2, _tangentCircle[i].w * 2, _tangentCircle[i].w * 2);
            }
        }
        
        for (int i = 0; i < _circleAmount; i++)
        {
            _material[i].SetColor("_Color", _gradient.Evaluate((1f / _circleAmount) * i) * 0.1f);
            if (_audioPeer._audioBandBuffer64[i] > _thresholdEmission)
            {
                if (_emissionBuffer)
                {
                    _material[i].SetColor("_EmissionColor", _gradient.Evaluate((1f / _circleAmount) * i) * _audioPeer._audioBandBuffer64[i] * _emissionMultiplier);
                    _material[i].SetColor("_Color", _gradient.Evaluate((1f / _circleAmount) * i) * _audioPeer._audioBandBuffer64[i] * _emissionMultiplier);
                }
                else
                {
                    _material[i].SetColor("_EmissionColor", _gradient.Evaluate((1f / _circleAmount) * i) * _audioPeer._audioBand64[i] * _emissionMultiplier);
                }
            }
            else
            {
                _material[i].SetColor("_EmissionColor", new Color(0, 0, 0));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _innerCircle.w);
        Gizmos.DrawWireSphere(transform.position, _outterCircle.w);
    }
}
