using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : MonoBehaviour
{


    public Transform ShootPoint;
    private Vector3 mausePos;
    public Camera maincam;
    public float shootTime;
    public float force;
    float nextShootTime;
    public PlayerPool playerPool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Input.GetMouseButton(0)
        if (Input.GetMouseButtonDown(0))
        {

            mausePos = maincam.ScreenToWorldPoint(Input.mousePosition);
            if (Time.time > nextShootTime){

                nextShootTime = Time.time + shootTime;

                

               //GameObject bullet= Instantiate(Gun, ShootPoint.position, ShootPoint.rotation);
                Vector3 direction = mausePos - ShootPoint.localPosition;
                Debug.Log(ShootPoint.localPosition);
                playerPool.GetObjectFromPool().GetComponent<BulletDamage>().AttackCreate(ShootPoint.position, mausePos);


                //bullet.GetComponent<Rigidbody2D>().velocity =new Vector2(direction.x, direction.y).normalized* force;
                
            }

        }
    }
}
