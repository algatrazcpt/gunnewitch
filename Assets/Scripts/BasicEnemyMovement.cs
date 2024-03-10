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
    public LayerMask enemyLayer;
    Rigidbody2D rg;
    public float attackTime;
    public float enemySpeed;
    public float attackRange;
    public float damage=10;
    private bool canAttack = true; // Ateþ etmeye hazýr mý?
    public float health = 100f;
    public float enemySpeelExp = 1f;
    public float enemyDeathExp = 1f;
    public bool slime = false;
    float currentHealth;
    Material cMaterial;
    public Transform attackpoint;
    EnemyAttackPoolController enemyAttackPoolController;
    public LevelBalance levelBalance;
    Color defColor;
    [Range(0, 3)]
    public float explosionRange=0f;
    bool stopMove=false;
    public float meleeDamageD;
    public float attackTimeD;
    public float enemySpeedD;
    public float healthD;
    public float enemyDeathExpD;
    public enemyType currentEnemyType;
    public Animator playerAnim;

    public enum enemyType{
        Range,
        Meele,
        Explode

    }

    public void CreateEnemy(Transform currentPos)
    {
        levelBalance = LevelBalance.Instance;
        
        health = healthD;
        damage = meleeDamageD;
        attackTime = attackTimeD;
        enemySpeed = enemySpeedD;
        enemyDeathExp = enemyDeathExpD;
        canAttack = true;
        //Balance System;
        damage += levelBalance.damageUpBalance;
        enemySpeed = enemySpeedD + levelBalance.enemyMovementUpBalance;
        health += levelBalance.healtUpBalance;
        enemyDeathExp += levelBalance.enemyExpUpBalance;
        //
        currentHealth = health;
        transform.position = currentPos.position;
        stopMove = false;
        transform.tag = "Enemy";
        gameObject.transform.localScale = Vector3.one;
        gameObject.SetActive(true);
        courutineBugFix = true;

    }
    private void Awake()
    {
        healthD = health;
        meleeDamageD = damage;
        attackTimeD = attackTime;
        enemySpeedD = enemySpeed;
        enemyDeathExpD = enemyDeathExp;
    }
    void Start()
    {
        
        levelBalance = LevelBalance.Instance;
        popController = PopController.instance;
        rg = gameObject.GetComponent<Rigidbody2D>();
        enemyAttackPoolController = EnemyAttackPoolController.instance;
        cMaterial =  gameObject.GetComponent<SpriteRenderer>().material;
        gameObject.GetComponent<SpriteRenderer>().material = cMaterial;
        defColor = cMaterial.color;
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

            // Hedefe doðru vektör oluþtur
            Vector3 direction = targetPosition - currentPosition;

            // Hedefe doðru dönme açýsýný hesapla (radyan cinsinden)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Yön vektörünün normalini (1 ve -1 deðerlerini) al
            Vector3 scale = transform.localScale;

            // Eðer hedefin saðýnda ise, Sprite'ý normal haliyle býrak
            if (direction.x >= 0)
            {
                //SlimeSprite bugfixFlip -
                transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
            }
            // Eðer hedefin solunda ise, Sprite'ý flip et
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
            }
        }

        if (gameObject.activeSelf == true&&stopMove!=true)
        {
            // Deneme için oluþtrudum Silebilirsin istersen farklý da yazýlýr (Emre)  pozisyon ile saldýrma trigger yok collision yok
            if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
            {

                if (currentEnemyType != enemyType.Explode)
                {

                    if (canAttack)
                    {
                        StartCoroutine("AttackRate");
                    }
                    rg.velocity = Vector2.zero;
                }
                else
                {
                    playerAnim.SetTrigger("Explode");
                    EnemyDeath();
                    rg.velocity = Vector2.zero;
                    stopMove = true;
                }

            }
            else
            {
                Debug.Log("Enemy Move");
                EnemyMove();
            }
        }
    }
    public void EnemyMoveSpeedAttack(float dspeed)
    {
        float cSpeed = enemySpeed + dspeed;
        enemySpeed = Mathf.Clamp(cSpeed, enemySpeed / 2f, enemySpeed + 2);
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

        // Mermi için varýþ süresini hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        float bulletTravelTime = distanceToPlayer / cAttack.attackMoveSpeed;

        // Player'ýn gelecekteki konumunu tahmin et
        Vector2 futurePosition = (Vector2)playerTransform.position + playerTransform.GetComponent<Rigidbody2D>().velocity * bulletTravelTime;

        // Hedef pozisyonunu ve yönünü hesapla
        Vector2 direction = futurePosition - (Vector2)transform.position;
        cAttack.AttackCreate(attackpoint.position, direction,playerTransform.position);

    }
    void meeleAttack()
    {
      meleeParticle.Play();
      playerTransform.gameObject.GetComponent<PlayerMovment>().TakeDamage(damage);

    }


    void CheckForEnemies(Vector3 position)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(position, explosionRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Player")) // Düþman tag'i kontrol et
            {
                playerTransform.gameObject.GetComponent<PlayerMovment>().TakeDamage(damage);
                Debug.Log("Player found");
            }
            else
            {
                enemy.gameObject.GetComponent<BasicEnemyMovement>().EnemyTakeDamage(Mathf.RoundToInt(damage / 2));
                Debug.Log("Enemy found");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Alanýn rengi
        Gizmos.DrawWireSphere(transform.position, explosionRange); // Alaný görselleþtir
    }


    IEnumerator AttackRate()
    {
        canAttack = false;
        if (currentEnemyType==enemyType.Meele)
        {
            meleeParticle.Play();
            meeleAttack();
        }
        else if(currentEnemyType==enemyType.Range)
        {
            attack();
        }
        else if(currentEnemyType==enemyType.Explode)
        {
            //Explode
        }
        // Ateþ et
        // Belirli bir süre sonra tekrar ateþ etmeye izin ver
        yield return new WaitForSeconds(1f / attackTime);
        canAttack = true;
    }
    public void EnemyTakeDamage(float damage)
    {

        if (courutineBugFix)
        {
            currentHealth -= damage;
            DamagePop currentPop = popController.GetObjectFromPool().GetComponent<DamagePop>();
            currentPop.DamageCreate(transform.position, damage);
            currentPop.DamageColor(Color.white);
            if (currentHealth > 0)
            {
                StartCoroutine("EnemyTakeDamageEffect");
            }
            else
            {
                transform.tag = "Not";
                stopMove = true;
                rg.velocity = Vector2.zero;
                if (courutineBugFix)
                {
                    StartCoroutine("EnemyTakeDamageEffect");
                }
            }
        }
    }
    bool courutineBugFix = true;
    void EnemyDeath()
    {
        courutineBugFix = false;
        rg.velocity = Vector2.zero;
        currentHealth = 0;
        stopMove=true;
        playerTransform.GetComponent<PlayerMovment>().TakeSpeelDamage(enemySpeelExp);
        transform.localScale = Vector2.one;
        if(currentEnemyType==enemyType.Range)
        {
            levelBalance.currentRangeEnemy--;
            gameObject.SetActive(false);
            ResetSystem();
        }
       else if(currentEnemyType==enemyType.Meele)
        {
            gameObject.SetActive(false);
            ResetSystem();
        }
        else
        {
            playerAnim.SetTrigger("Explode");
            StartCoroutine("EnemyExplodeColorEffect");
        }
        levelBalance.currentEnemy--;
    }
    IEnumerator EnemyTakeDamageEffect()
    {
        if (currentHealth > 0)
        {
            cMaterial.SetColor("_Color", Color.white);
        }
        else
        {
            cMaterial.SetColor("_Color", Color.red);
        }
        cMaterial.SetFloat("_FlashCount",1.5f);
        yield return new WaitForSeconds(0.2f);
        cMaterial.SetFloat("_FlashCount", 0);
        if (currentHealth <= 0)
        {
            transform.tag= "Not";
            EnemyDeath();
            cMaterial.SetColor("_Color", Color.red);
        }
        cMaterial.SetColor("_Color", defColor);
        cMaterial.color = defColor;
    }
    IEnumerator EnemyExplodeColorEffect()
    {
        StopCoroutine("EnemyTakeDamageEffect");
        rg.velocity = Vector2.zero;
        transform.tag = "Not";
        cMaterial.SetColor("_Color", defColor);
        cMaterial.SetFloat("_FlashCount", 1.5f);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.localScale = gameObject.transform.localScale * 3;
        yield return new WaitForSeconds(0.3f);
        CheckForEnemies(transform.position);
        cMaterial.SetFloat("_FlashCount", 0);
        cMaterial.SetColor("_Color", defColor);
        gameObject.transform.localScale = Vector3.one;
        gameObject.SetActive(false);
        
    }
    public void ResetSystem()
    {
        transform.tag = "Enemy";
        transform.position = Vector3.zero;
        rg.velocity = Vector2.zero;
        if (currentEnemyType != enemyType.Explode)
        {
            cMaterial.SetColor("_Color", Color.white);
        }
        cMaterial.SetFloat("_FlashCount", 0);
        currentHealth=health;
        
    }
}
