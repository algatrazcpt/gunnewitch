using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsaBlock : MonoBehaviour
{


    public float lifetime = 1.5f;

    private float gecenzaman = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //if(gecenzaman>= lifetime)
        //{
        //    gecenzaman = 0f;
        //    Destroy(this.gameObject);



        //}
        //gecenzaman += Time.deltaTime;


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            collision.gameObject.SetActive(false);

        }

    }
}
