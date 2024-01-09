using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    public Transform playerTransform;
    Rigidbody2D rg;
    public float attackTime;
    void Start()
    {
        rg = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    Vector2 direction;
    void Update()
    {
        direction = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
        rg.velocity = direction.normalized * 1.2f;


        // Deneme için oluþtrudum Silebilirsin istersen farklý da yazýlýr (Emre)  pozisyon ile saldýrma trigger yok collision yok
        if (Vector2.Distance(transform.position, playerTransform.position)<1)  
        {
            attack();
            Debug.Log("yan yana");
        }
    }



    // Deneme için oluþtrudum Silebilirsin istersen farklý da yazýlýr (Emre)


    // Deneme için oluþtrudum Silebilirsin istersen farklý da yazýlýr (Emre)
    public void attack()
    {
        playerTransform.GetComponent<PlayerMovment>().TakeDamage(1);

    }
   
}
