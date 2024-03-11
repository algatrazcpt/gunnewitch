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
    public bool ozelaktifmi;
    public float refsüre = 10f;
    public float gecensure = 0f;
    public GameObject playerss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var bullet = playerPool.GetObjectFromPool().GetComponent<BulletDamage>();
        if (ozelaktifmi == true)
        {
            
            bullet.attackDamage = 15;
            playerss.GetComponent<PlayerMovment>().moveSpeed = 6f;
            gecensure += Time.deltaTime;
            if (gecensure > refsüre)
            {
                ozelaktifmi = false;
                gecensure = 0f;
                bullet.attackDamage = 10;
                playerss.GetComponent<PlayerMovment>().moveSpeed = 4f;
            }

        }
        //Input.GetMouseButton(0)
        if (Input.GetMouseButton(0))
        {

  
            mausePos = maincam.ScreenToWorldPoint(Input.mousePosition);
            if (Time.time > nextShootTime){

                nextShootTime = Time.time + shootTime;

                

               //GameObject bullet= Instantiate(Gun, ShootPoint.position, ShootPoint.rotation);
                Vector3 direction = mausePos - ShootPoint.localPosition;
                bullet.AttackCreate(ShootPoint.position, mausePos);


                //bullet.GetComponent<Rigidbody2D>().velocity =new Vector2(direction.x, direction.y).normalized* force;
                
            }

        }
    }
}
