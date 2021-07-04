using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralV1 : MonoBehaviour
{
    [SerializeField] private AudioPeer audioPeer;
    [SerializeField] private Camera cam;

    [SerializeField] private float waitFor = 0f;
    private float wait = 0f;

    private Vector3 position = new Vector3(0, 0, 0);
    private float angle = 0;

    [SerializeField] private float heighAndWidth = 1f;
    //[SerializeField] private float incrementForward = 1f;
    //[SerializeField] private float incrementAngle = 1f;
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float initialVelocity = 5f;
    [HideInInspector] public float velocity = 1f;
    float angle2 = 0f;
    float xSinus = 0f;
    [SerializeField] float velocityDifference = 10f;
    [SerializeField] float velocityDifferenceSpeed = .1f;


    [SerializeField] private Material cubeMat;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private Color backgroundColorUpper;
    [SerializeField] private Color backgroundColorBottom;



    [SerializeField] GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = transform.position;

        sinusSpeed();
        changeMat();
        fogAndBackgroundColor();

        if (wait >= waitFor)
        {
            CubeSpiralSpawn(1, 1);
            wait = 0;

        }


        wait += 1;
    }

    void CubeSpiralSpawn(float heightAndWidthMult, float velocityMult)
    {
        float x = (Mathf.Cos(angle)) * heighAndWidth * heightAndWidthMult;
        float y = (Mathf.Sin(angle)) * heighAndWidth * heightAndWidthMult;

        //creating the cube
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject cube = Instantiate(model);
        //cube.GetComponent<BoxCollider>().enabled = false;
        cube.transform.GetComponentInChildren<SkinnedMeshRenderer>().material = cubeMat;
        cube.AddComponent<CubeMovement>();

        //rb
        /*Rigidbody rb = cube.AddComponent<Rigidbody>() as Rigidbody;
        rb.useGravity = false;
        rb.velocity = Vector3.forward * velocity * velocityMult;*/

        //position cube
        //z += incrementForward;
        position = new Vector3((x+ transform.position.x), ( y + transform.position.y), transform.position.z );
        cube.transform.position = position;
        cube.transform.LookAt(cameraTransform);
        angle += Mathf.Lerp(.1f, 2f, sinusAngleIncrement()) ;
        

        Destroy(cube, lifeTime);
    }   

    void changeMat()
    {
        cubeMat.SetColor("_EmissionColor", Color.Lerp(Color.blue, Color.yellow, audioPeer._audioBandBuffer[0]));
    }

    void fogAndBackgroundColor()
    {
        //Debug.Log(Color.black);
        Color theColor = Color.Lerp(backgroundColorBottom, backgroundColorUpper, audioPeer._AmplitudeBuffer);
        RenderSettings.fogColor = theColor;
        cam.backgroundColor = theColor;
    }

    void sinusSpeed()
    {
        velocity = initialVelocity + velocityDifference * Mathf.Sin(angle2) ;
        angle2 += velocityDifferenceSpeed;
    }

    float sinusAngleIncrement()
    {
        float y = .5f * Mathf.Sin(xSinus) + .5f;
        xSinus += .001f;

        return y;
    }
}
