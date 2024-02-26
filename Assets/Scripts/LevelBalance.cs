using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBalance : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelBalance Instance;

    public int currentEnemy=0;
    public int maxEnemyCount = 50;


    public int healtUpBalance = 1;
    public int damageUpBalance=1;
    public int enemyExpUpBalance=1;
    public int iksirUpBalance=1;
    public float attackMoveSpeed = 1;
    public int enemyMovementUpBalance=1;
    public float timeBalance = 1;
    public float timeBalanceMax = 10;
    
    public int levelUpgradeBalance = 4;


    private int healtUpBalanceC = 1;
    private int damageUpBalanceC = 1;
    private int enemyExpUpBalanceC = 1;
    private int iksirUpBalanceC = 1;
    private float attackMoveSpeedC = 1;
    private int enemyMovementUpBalanceC = 1;
    private float timeBalanceC = 1;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        healtUpBalanceC = healtUpBalance;
        damageUpBalanceC = damageUpBalance;
        enemyExpUpBalanceC = enemyExpUpBalance;
        iksirUpBalanceC = iksirUpBalance;
        attackMoveSpeedC = attackMoveSpeed;
        enemyMovementUpBalanceC = enemyMovementUpBalance;
        timeBalanceC = timeBalance;
    }
    public void LevelBalanceUp()
    {
        healtUpBalance += healtUpBalanceC;
        damageUpBalance += damageUpBalanceC;
        enemyExpUpBalance += enemyExpUpBalanceC;
        iksirUpBalance += iksirUpBalanceC;
        enemyMovementUpBalance += enemyMovementUpBalanceC;
        attackMoveSpeed += attackMoveSpeedC;
        Debug.Log("LevelBalance");
    }
    public void TimeBalanceUp()
    {
        timeBalance +=timeBalanceC;
        Debug.Log("TimeBalance");
    }
    public float GetTimeBalance()
    {
        if(timeBalance<=timeBalanceMax)
        {
            return timeBalance;
        }
        else
        {
            return timeBalanceMax;
        }
    }
}
