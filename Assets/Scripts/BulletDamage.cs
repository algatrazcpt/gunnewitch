using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    PopController getPoper;
    public float damage;
    void Start()
    {
        getPoper = PopController.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            getPoper.GetObjectFromPool().GetComponent<DamagePop>().DamageCreate(transform.position,damage);
            Destroy(gameObject);
        }
    }
}
