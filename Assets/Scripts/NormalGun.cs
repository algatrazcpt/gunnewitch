using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : MonoBehaviour
{


    public Transform ShootPoint;
    private Vector3 mausePos;
    public GameObject Gun;
    public Camera maincam;
    public float shootTime;
    public float force;
    float nextShootTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            mausePos = maincam.ScreenToWorldPoint(Input.mousePosition);
            if (Time.time > nextShootTime){

                nextShootTime = Time.time + shootTime;
                GameObject bullet= Instantiate(Gun, ShootPoint.position, ShootPoint.rotation);
                Vector3 direction = mausePos - bullet.transform.position;
                bullet.GetComponent<Rigidbody2D>().velocity =new Vector2(direction.x, direction.y).normalized* force;
                
            }

        }
    }
}
