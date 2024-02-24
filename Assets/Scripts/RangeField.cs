using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeField : MonoBehaviour
{
    public float range = 0f;
    public Transform playertrf;
    public bool OzelAktifmi = false;
    private List<GameObject> affectedBullets = new List<GameObject>();
    public float ozelguçsüre = 8f;
    public float gecensure = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (OzelAktifmi)
        {
            transform.position = playertrf.position;
            gecensure += Time.deltaTime;

            if(gecensure>= ozelguçsüre)
            {
                OzelGucKapandý();
                
            }

        }
        

    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Daire rengi
        Gizmos.DrawWireSphere(transform.position, range);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            affectedBullets.Add(collision.gameObject);
            collision.GetComponent<EnemyAttack>().attackMoveSpeed = 1;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            collision.GetComponent<EnemyAttack>().attackMoveSpeed = 6;
            affectedBullets.Remove(collision.gameObject);
        }

    }

    public void OzelGucKapandý()
    {
        OzelAktifmi = false;
        foreach (var a in affectedBullets)
        {

            a.GetComponent<EnemyAttack>().attackMoveSpeed = 6;
        }
        affectedBullets.Clear();
        gecensure = 0f;
        gameObject.SetActive(false);
    }
}
