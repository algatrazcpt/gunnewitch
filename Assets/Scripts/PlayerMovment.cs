using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    public float moveSpeed;
    Rigidbody2D rb;
    Vector2 moveDir;
    public Transform weapon;
    public float offset;
    public SpriteRenderer sp;
    public float Health;
    

   public bool faceRight = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp=GetComponent<SpriteRenderer>();
    }


    public void TakeDamage(float damage)
    {
        Health -= damage;

    }
    // Update is called once per frame
    void Update()
    {
        InputManager();
        weaponRotate();
    }
    private void FixedUpdate()
    {
        move();
    }

    void InputManager()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;


    }

    void move()
    {

        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);

        if (moveDir.x > 0 && !faceRight)
        {
            
            filipWeapon();
            filipWitch();
            //sp.flipX = true;



        }
       else if (moveDir.x < 0 && faceRight)
        {
           filipWeapon();
           filipWitch();
           


        }
    }

    void filipWitch()
    {
        Vector3 scale = gameObject.transform.localScale;

        scale.x *= -1;
        gameObject.transform.localScale = scale;
        faceRight = !faceRight;



    }
    void filipWeapon()
    {
        Vector3 scale = weapon.transform.localScale;

        scale.x *= -1;
        weapon.transform.localScale = scale;
        



    }
  
    void weaponRotate()
    {

        //Vector3 scaleWeapon = weapon.transform.localScale;
        Vector3 displayWeapon = weapon.position - ( Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float angles = Mathf.Atan2(displayWeapon.y, displayWeapon.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0f, 0f, angles+ offset);
        Vector3 localScales = weapon.transform.localScale;
        if (faceRight ==false)
        {
            Debug.Log("False girdi");
            
            localScales.x = -1f;
        }
        else if (faceRight == true)
        {
            localScales.x = 1f;

        }
        //weapon.eulerAngles = new Vector3(0, 0, angles);

        
        //Debug.Log(localScales);
        if(angles>90 || angles < -90)
        {
            localScales.y = 1f;
            


        }
        else
        {
            localScales.y = -1f;
            


        }
        weapon.transform.localScale = localScales;
            

        //if (gameObject.transform.localScale.x<0)
        //{
        //    scaleWeapon.x = -1;
        //    weapon.transform.localScale = scaleWeapon;
        //}
        //else if (gameObject.transform.localScale.x > 0)
        //{
        //    scaleWeapon.x = 1;
        //    weapon.transform.localScale = scaleWeapon;
        //}


    }
}
