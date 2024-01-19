using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BasicEnemyMovement : MonoBehaviour
{
    public Transform playerTransform;
    public ParticleSystem meleeParticle;
    Rigidbody2D rg;
    public float attackTime;
    public float enemySpeed;
    public float attackRange;
    public bool canMeele = true;
    public float meleeDamage=10;
    private bool canAttack = true; // Ateþ etmeye hazýr mý?
    public float health = 100f;
    float currentHealth;
    Material cMaterial;
    EnemyAttackPoolController enemyAttackPoolController;
    void Start()
    {
        rg = gameObject.GetComponent<Rigidbody2D>();
        enemyAttackPoolController = EnemyAttackPoolController.instance;
        cMaterial =  gameObject.GetComponent<SpriteRenderer>().material;
        gameObject.GetComponent<SpriteRenderer>().material = cMaterial;
        currentHealth = health;
    }

    // Update is called once per frame
    Vector2 direction;
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            // Deneme için oluþtrudum Silebilirsin istersen farklý da yazýlýr (Emre)  pozisyon ile saldýrma trigger yok collision yok
            if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
            {
                if (canAttack)
                {
                    StartCoroutine("AttackRate");
                }
                Debug.Log("yan yana");
                rg.velocity = Vector2.zero;
            }
            else
            {
                EnemyMove();
            }
        }
    }
    void EnemyMove()
    {
        direction = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
        rg.velocity = direction.normalized * enemySpeed;

    }


    // Deneme için oluþtrudum Silebilirsin istersen farklý da yazýlýr (Emre)


    // Deneme için oluþtrudum Silebilirsin istersen farklý da yazýlýr (Emre)



    EnemyAttack cAttack;
     void attack()
    {
        cAttack = enemyAttackPoolController.GetObjectFromPool().GetComponent<EnemyAttack>();
        Debug.Log(cAttack.name);
        cAttack.AttackCreate(transform.position,playerTransform.position);

    }
    void meeleAttack()
    {
      meleeParticle.Play();
      playerTransform.gameObject.GetComponent<PlayerMovment>().TakeDamage(meleeDamage);

    }


    IEnumerator AttackRate()
    {
        canAttack = false;
        if (canMeele)
        {
            meleeParticle.Play();
            meeleAttack();
        }
        else
        {
            attack();
        }
        // Ateþ et
        // Belirli bir süre sonra tekrar ateþ etmeye izin ver
        yield return new WaitForSeconds(1f / attackTime);
        canAttack = true;
    }
    public void EnemyTakeDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine("EnemyTakeDamageEffect");
    }
    void EnemyDeath()
    {
        gameObject.SetActive(false);
        ResetSystem();
    }
    IEnumerator EnemyTakeDamageEffect()
    {
        if (currentHealth <= 0)
        {
            cMaterial.SetColor("_Color", Color.red);
        }
        cMaterial.SetFloat("_FlashCount",1.5f);
        yield return new WaitForSeconds(0.2f);
        cMaterial.SetFloat("_FlashCount", 0);
        if (currentHealth <= 0)
        {
            EnemyDeath();
            cMaterial.SetColor("_Color", Color.white);
        }

        
    }
    public void ResetSystem()
    {
        transform.position = Vector3.zero;
        rg.velocity = Vector2.zero;
        cMaterial.SetColor("_Color", Color.white);
        currentHealth=health;

    }

}
