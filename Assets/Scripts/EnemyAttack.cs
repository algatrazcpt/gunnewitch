using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage;
    public float attackMoveSpeed;
    public ParticleSystem attackAnim;
    public ParticleSystem boomEffect;
    public GameObject effectTrail;

    //
    public float initialSpeed = 2.5f; // Baþlangýç hýzý
    public float acceleration = 10f; // Hýzlanma miktarý
    public float maxSpeed = 10f; // Maksimum hýz
    public float currentSpeed=2.5f; // Þu anki hýz
                                    //
    public float attackDamageD;
    public float attackMoveSpeedD;
    public float initialSpeedD = 2.5f; // Baþlangýç hýzý
    public float accelerationD = 10f; // Hýzlanma miktarý
    public float maxSpeedD = 10f; // Maksimum hýz

    //
    public bool slowEffect = false;
    Rigidbody2D rg;
    Vector2 direction;
    
    void Awake()
    {
        attackDamageD = attackDamage;
        attackMoveSpeedD = attackMoveSpeed;
        initialSpeedD = initialSpeed;
        accelerationD = acceleration;
        maxSpeedD = maxSpeed;
        rg = GetComponent<Rigidbody2D>();
    }
    public void AttackCreate(Vector3 defaultPos,Vector3  targetPos)
    {
        //
        attackDamage = attackDamageD;
        attackMoveSpeed = attackMoveSpeedD;
        initialSpeed = initialSpeedD;
        acceleration = accelerationD;
        maxSpeed = maxSpeedD;
        //


        attackDamage += LevelBalance.Instance.damageUpBalance;
        attackMoveSpeed += LevelBalance.Instance.attackMoveSpeed;
        maxSpeed+= LevelBalance.Instance.attackMoveSpeed; 
        currentSpeed = initialSpeed;
        transform.position = defaultPos;
        direction = new Vector2(targetPos.x - defaultPos.x, targetPos.y - defaultPos.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Yatay düzlemde dönme iþlemini saðlamak için rotasyonu belirle
        
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        effectTrail.transform.rotation = Quaternion.Euler(0, 0, angle+90f);
        gameObject.SetActive(true);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

    }
     void Update()
    {
        if (gameObject.activeInHierarchy == true&& !slowEffect)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            rg.velocity = direction.normalized * currentSpeed;
        }
    }

    
    void BulletDestroy()
    {
        if (gameObject.activeInHierarchy == true)
        {
            rg.velocity = Vector2.zero;
            currentSpeed = initialSpeed;
            boomEffect.Play();
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Invoke("effectBugFix", 0.1f);
            
        }
    }
    void effectBugFix()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Attack(collision.GetComponent<PlayerMovment>());
            BulletDestroy();


        }
        else if(collision.CompareTag("Wall"))
        {
            BulletDestroy();
        }
    }

    void Attack(PlayerMovment player)
    {
        player.TakeDamage(attackDamage);
    }
}
