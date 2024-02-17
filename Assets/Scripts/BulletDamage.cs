using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float attackDamage;
    public float attackMoveSpeed;
    public Animator cAnimator;
    public GameObject boomEffect;
    Rigidbody2D rg;
    Vector2 direction;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    public void AttackCreate(Vector3 defaultPos, Vector3 targetPos)
    {
        boomEffect.SetActive(false);
        transform.position = defaultPos;
        direction = new Vector2(targetPos.x - defaultPos.x, targetPos.y - defaultPos.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Yatay düzlemde dönme iþlemini saðlamak için rotasyonu belirle
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = targetRotation;

        gameObject.SetActive(true);
        Invoke("BulletDestroy", 10);
    }
    void Update()
    {
        if (gameObject.activeInHierarchy == true)
        {
            rg.velocity = direction.normalized * attackMoveSpeed;
        }
    }
    void BulletDestroy()
    {
        if (gameObject.activeInHierarchy == true)
        {
            rg.velocity = Vector2.zero;
            boomEffect.SetActive(true);
            cAnimator.SetTrigger("BoomEffect");
            CancelInvoke("BulletDestroy");
            Invoke("effectBugFix", 0.1f);

        }
    }
    void effectBugFix()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Attack(collision.GetComponent<BasicEnemyMovement>());
            BulletDestroy();


        }
        else if (collision.CompareTag("Wall"))
        {
            BulletDestroy();
        }
    }

    void Attack(BasicEnemyMovement enemy)
    {
        enemy.EnemyTakeDamage(attackDamage);
    }
}
