using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asaAttack : MonoBehaviour
{

    public Animator anims;
    public Transform attackPoint;
    public float MeleAttackRange = .8f;
    PopController getPoper;
    public float meleDamage = 5;
    // Start is called before the first frame update
    void Start()
    {
        getPoper = PopController.instance;
        anims.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            EnemyDetect();
        }
    }

    public void EnemyDetect()
    {
        anims.SetTrigger("maleAttack");
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(attackPoint.position, MeleAttackRange))
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                getPoper.GetObjectFromPool().GetComponent<DamagePop>().DamageCreate(collider.transform.position, meleDamage);
            }


        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, MeleAttackRange);
    }
}
