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
    public float initialSpeed = 2.5f; // Ba�lang�� h�z�
    public float acceleration = 10f; // H�zlanma miktar�
    public float maxSpeed = 10f; // Maksimum h�z
    private float currentSpeed=2.5f; // �u anki h�z
    //

    Rigidbody2D rg;
    Vector2 direction;
    
    void Awake()
    {
        rg= GetComponent<Rigidbody2D>();
    }
    public void AttackCreate(Vector3 defaultPos,Vector3  targetPos)
    {
        attackDamage += LevelBalance.Instance.damageUpBalance;
        attackMoveSpeed += LevelBalance.Instance.attackMoveSpeed;
        maxSpeed+= LevelBalance.Instance.attackMoveSpeed; 
        currentSpeed = initialSpeed;
        transform.position = defaultPos;
        direction = new Vector2(targetPos.x - defaultPos.x, targetPos.y - defaultPos.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Yatay d�zlemde d�nme i�lemini sa�lamak i�in rotasyonu belirle
        
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        effectTrail.transform.rotation = Quaternion.Euler(0, 0, angle+90f);
        gameObject.SetActive(true);
        
    }
     void Update()
    {
        if (gameObject.activeInHierarchy == true)
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
            Invoke("effectBugFix", 0.2f);
            
        }
    }
    void effectBugFix()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
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
