using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] AudioPeer audioPeerDrum;
    [SerializeField] AudioPeer audioPeerJuno;
    [SerializeField] GameObject obstacle1;
    [SerializeField] GameObject obstacle2;
    [SerializeField] ParticleSystem spawnPoof;

    [SerializeField] float kickSensitivity = .5f;
    [SerializeField] float snareSensitivity = .5f;
    [SerializeField] float snareRelativeSensitivity = 1.1f;
    [SerializeField] float hiHatSensitivity = .75f;
    [SerializeField] float hiHatRelativeSensitivity = 2.5f;

    Vector3 startingPos = new Vector3(10, 0, 0);
    [SerializeField] float speed = .5f;
    //[SerializeField] int spawnSpeed = 100;

    [SerializeField] float junoSpawnFreq =  1f;

    Vector3 vecSpeed = Vector3.zero;

    int i = 100;
    [SerializeField] float minBeatTime = .2f;
    float snareTimer = 1f;
    float kickTimer = 1f;
    float hiHatTimer = 1f;
    float junoTimer = 1f;

    float currentTimeScaled;
    float tunnelWidth;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //the tunnel width
        currentTimeScaled = Time.timeSinceLevelLoad / audioPeerDrum._audioClip.length;
        tunnelWidth = 4.5f;

        //spawn on juno
        if (audioPeerJuno._AmplitudeBuffer > .5)
        {
            tunnelWidth = Mathf.Lerp(4.5f, 2.5f, currentTimeScaled);

            if (junoTimer < 0)
            {
                spawnCube(tunnelWidth, false, obstacle1);
                spawnCube(-tunnelWidth, false, obstacle1);

                junoTimer = junoSpawnFreq;
            }
        }
        junoTimer -= Time.deltaTime;

        //Spawn on snare
        if (audioPeerDrum._audioBandBuffer[1] > audioPeerDrum._audioBandBuffer[0] * snareRelativeSensitivity
            && (audioPeerDrum._audioBandBuffer[1] + audioPeerDrum._audioBandBuffer[2] + audioPeerDrum._audioBandBuffer[3]) / 3 > snareSensitivity && snareTimer < 0)
        {
            spawnCube(Random.Range(-tunnelWidth + 1, tunnelWidth - 1), true, obstacle1);
            snareTimer = minBeatTime;
        }
        snareTimer -= Time.deltaTime;

        //Spawn on snare v2
        /*if (audioPeerDrum._audioBandBuffer[1] > audioPeerDrum._audioBandBuffer[0] &&
            audioPeerDrum._audioBandBuffer[1] > .35f && snareTimer < 0)
        {
            spawnCube(Random.Range(-3.5f, 3.5f), true, obstacle2);
            snareTimer = minBeatTime;
        }
        snareTimer -= Time.deltaTime;*/


        //spawn on kick
        if (audioPeerDrum._audioBandBuffer[0] > kickSensitivity 
            && (audioPeerDrum._audioBandBuffer[1] + audioPeerDrum._audioBandBuffer[2]) / 2 < audioPeerDrum._audioBandBuffer[0] 
            && kickTimer < 0)
        {
            spawnCube(Random.Range(-tunnelWidth + 1, tunnelWidth - 1), true, obstacle1);
            kickTimer = minBeatTime;
        }
        kickTimer -= Time.deltaTime;

        //spawn on kick v2
        /*if (audioPeerDrum._audioBandBuffer[0] > .5f && audioPeerDrum._audioBandBuffer[1] <  audioPeerDrum._audioBandBuffer[0] && kickTimer < 0)
        {
            spawnCube(Random.Range(-3.5f, 3.5f), true);
            kickTimer = minBeatTime;
        }
        kickTimer -= Time.deltaTime;*/

        //Spawn on hi hat
        if (audioPeerDrum._audioBandBuffer[7] > hiHatSensitivity
            && audioPeerDrum._audioBandBuffer[7] > (audioPeerDrum._audioBandBuffer[0] + audioPeerDrum._audioBandBuffer[1] 
            + audioPeerDrum._audioBandBuffer[4] + audioPeerDrum._audioBandBuffer[5] + audioPeerDrum._audioBandBuffer[6]) 
            /5* hiHatRelativeSensitivity 
            && hiHatTimer < 0)
        {
            spawnCube(Random.Range(-tunnelWidth + 1, tunnelWidth - 1), true, obstacle2);
            hiHatTimer = minBeatTime;
        }
        hiHatTimer -= Time.deltaTime;

        //spawn random
        /*if (i > spawnSpeed + Random.Range(spawnSpeed/2, spawnSpeed*2)) 
        {
            spawnCube();
            i = 0;
        }
        i += 1;*/
    }

    void spawnCube(float startingPosY, bool spawnParticle, GameObject theEnemy)
    {
        startingPos.y = startingPosY;

        GameObject obs = Instantiate<GameObject>(theEnemy);
        if (spawnParticle) 
        { 
            ParticleSystem poof = Instantiate<ParticleSystem>(spawnPoof);
            poof.transform.position = startingPos;
            Destroy(poof, 2);
        }
        Destroy(obs, 5);

        obs.transform.position = startingPos;
        vecSpeed.x = speed;
        obs.GetComponent<Rigidbody>().velocity = vecSpeed;
        obs.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, Random.Range(3, 7), 0 );
    }
}
