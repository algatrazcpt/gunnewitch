using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asaAttack : MonoBehaviour
{

    public Animator anims;
    public Transform attackPoint;
    public float MeleAttackRange = 0.8f;
    PopController getPoper;
    public float meleDamage = 5;
    private bool canAttack=true;
    private float attackTime=2;

    // Start is called before the first frame update
    void Start()
    {
        getPoper = PopController.instance;
        anims.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(canAttack)
            {
                StartCoroutine("AttackRate");
            }
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
    IEnumerator AttackRate()
    {
        canAttack = false;
        EnemyDetect();
        yield return new WaitForSeconds(1f / attackTime);
        anims.ResetTrigger("maleAttack");
        canAttack = true;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, MeleAttackRange);
    }
}
