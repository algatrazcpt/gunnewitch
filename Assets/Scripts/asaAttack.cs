using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asaAttack : MonoBehaviour
{
    public GameObject AsaAlan;
    public Animator anims;
    public Transform attackPoint;
    public float MeleAttackRange = 0.8f;
    PopController getPoper;
    public float meleDamage = 5;
    public bool canAttack=true;
    private float attackTime=2;

    public bool ishold = false;
    public float holtime = 0f;
    public float holdRef = 2f;
    public GameObject refAsaAlan;

    public bool asaalangeldini = false;

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
            ishold = true;
            if (canAttack)
            {
                StartCoroutine("AttackRate");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ishold = false;
            holtime = 0f;
            Destroy(refAsaAlan,1f);
            asaalangeldini = false;
        }

        if (ishold)
        {
            holtime += Time.deltaTime;

            if (holtime >holdRef)
            {
                if (!asaalangeldini)
                {
                    refAsaAlan = Instantiate(AsaAlan, transform.position, Quaternion.identity);
                    asaalangeldini = true;
                }
                

            }

        }
    }

    public void EnemyDetect()
    {
        
        
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(attackPoint.position, MeleAttackRange))
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<BasicEnemyMovement>().EnemyTakeDamage(meleDamage);
            }
        }


    }
    IEnumerator AttackRate()
    {
        canAttack = false;
        anims.SetTrigger("maleAttack");
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
