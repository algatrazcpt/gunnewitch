using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BasicEnemyMovement : MonoBehaviour
{
    public PopController popController;
    public Transform playerTransform;
    public ParticleSystem meleeParticle;
    Rigidbody2D rg;
    public float attackTime;
    public float enemySpeed;
    public float attackRange;
    public bool canMeele = true;
    public float meleeDamage=10;
    private bool canAttack = true; // Ate� etmeye haz�r m�?
    public float health = 100f;
    public float enemySpeelExp = 1f;
    public float enemyDeathExp = 1f;
    public bool slime = false;
    float currentHealth;
    Material cMaterial;
    public Transform attackpoint;
    EnemyAttackPoolController enemyAttackPoolController;
    public LevelBalance levelBalance;

    public void CreateEnemy(Transform currentPos)
    {
        levelBalance = LevelBalance.Instance;
        canAttack = true;
        //Balance System;
        meleeDamage += levelBalance.damageUpBalance;
        enemySpeed += levelBalance.enemyMovementUpBalance;
        health += levelBalance.healtUpBalance;
        enemyDeathExp += levelBalance.enemyExpUpBalance;
        //
        currentHealth = health;
        transform.position = currentPos.position;
        gameObject.SetActive(true);
    }



    void Start()
    {
        levelBalance = LevelBalance.Instance;
        popController = PopController.instance;
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
        if (true)
        {
            // Hedef nesnenin konumunu al
            Vector3 targetPosition = playerTransform.position;

            // Kendi konumunu al
            Vector3 currentPosition = transform.position;

            // Hedefe do�ru vekt�r olu�tur
            Vector3 direction = targetPosition - currentPosition;

            // Hedefe do�ru d�nme a��s�n� hesapla (radyan cinsinden)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Y�n vekt�r�n�n normalini (1 ve -1 de�erlerini) al
            Vector3 scale = transform.localScale;

            // E�er hedefin sa��nda ise, Sprite'� normal haliyle b�rak
            if (direction.x >= 0)
            {
                //SlimeSprite bugfixFlip -
                transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
            }
            // E�er hedefin solunda ise, Sprite'� flip et
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
            }
        }

        if (gameObject.activeSelf == true)
        {
            // Deneme i�in olu�trudum Silebilirsin istersen farkl� da yaz�l�r (Emre)  pozisyon ile sald�rma trigger yok collision yok
            if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
            {

                if (canAttack)
                {
                    StartCoroutine("AttackRate");
                }
                rg.velocity = Vector2.zero;
            }
            else
            {
                Debug.Log("Enemy Move");
                EnemyMove();
            }
        }
    }
    void EnemyMove()
    {
        direction = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
        rg.velocity = direction.normalized * enemySpeed;

    }

    EnemyAttack cAttack;
     void attack()
    {
        cAttack = enemyAttackPoolController.GetObjectFromPool().GetComponent<EnemyAttack>();
        cAttack.AttackCreate(attackpoint.position,playerTransform.position);

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
        // Ate� et
        // Belirli bir s�re sonra tekrar ate� etmeye izin ver
        yield return new WaitForSeconds(1f / attackTime);
        canAttack = true;
    }
    public void EnemyTakeDamage(float damage)
    {
        currentHealth -= damage;
        DamagePop currentPop = popController.GetObjectFromPool().GetComponent<DamagePop>();
        currentPop.DamageCreate(transform.position, damage);
        currentPop.DamageColor(Color.red);
        StartCoroutine("EnemyTakeDamageEffect");
    }
    void EnemyDeath()
    {
        playerTransform.GetComponent<PlayerMovment>().TakeSpeelDamage(enemySpeelExp);
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
