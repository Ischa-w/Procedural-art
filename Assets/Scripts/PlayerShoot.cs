using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] float bulletVel = 1f;

    [SerializeField] float waitTimeToShoot = 10f;
    float i = 100f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);
        if (Input.GetMouseButton(0) && waitTimeToShoot < i)
        {
            GameObject bul = Instantiate<GameObject>(bullet);
            bul.transform.position = new Vector3(bulletOrigin.position.x, bulletOrigin.position.y, 0f);

            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePos.z = 0f;
            bul.transform.LookAt(MousePos);
            Vector3 direction = MousePos - transform.position;

            bul.GetComponent<Rigidbody>().velocity = direction.normalized * bulletVel;

            Destroy(bul, 5);

            i = 0f;
        }
        i += Time.deltaTime * 10;
    }
}