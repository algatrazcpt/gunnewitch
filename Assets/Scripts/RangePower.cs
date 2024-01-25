using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePower : MonoBehaviour
{

    public float range = 0f;
    public List<GameObject> alanaGirenler = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Daire rengi
        Gizmos.DrawWireSphere(transform.position, range);
    }


    public void alaniçiObjebul()
    {

       foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, range))
        {
            if (collider.gameObject.CompareTag("enemyBullet"))
            {
                alanaGirenler.Add(collider.gameObject);
            }
            
        }



    }
    public void alantemizle()
    {

        alanaGirenler.Clear();
    }
}
