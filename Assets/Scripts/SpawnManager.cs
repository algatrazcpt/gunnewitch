using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   
    public Vector2 squareSize; // Kare alanýn boyutu

    public GameObject spawnKazanObject; // Oluþturulacak nesne

    public int timeBalancePerEnemy = 100;
    public int balancePerEnemy = 20;
    private int currentTimeBalancePerEnemy = 100;
    private int currentEnemyCount=0;
    EnemyPool pool;
    public Transform[] enemySpawnLocations;
    private int enemysTypeCount;
    private int spawnLocationCount;


    private void Start()
    {
        pool = EnemyPool.Instance;
        enemysTypeCount = pool.enemyPrefabs.Length;
        spawnLocationCount = enemySpawnLocations.Length;
        LevelPositionChange();
        SpawnEnemySystem(0.1f);
    }
    public void SpawnEnemySystem(float time)
    {
        InvokeRepeating("SpawnEnemy", 0, 1/time);
    }

    void SpawnEnemy()
    {
        if (LevelBalance.Instance.currentEnemy < LevelBalance.Instance.maxEnemyCount)
        {
            if (balancePerEnemy == currentEnemyCount)
            {
                currentEnemyCount = 0;
                LevelBalance.Instance.LevelBalanceUp();
            }
            if (timeBalancePerEnemy == currentTimeBalancePerEnemy)
            {
                currentTimeBalancePerEnemy = 0;
                LevelBalance.Instance.TimeBalanceUp();
                CancelInvoke();
                SpawnEnemySystem(LevelBalance.Instance.GetTimeBalance());
                //
                LevelPositionChange();
                //
                for (int index = 0; index < LevelBalance.Instance.levelUpgradeBalance; index++)
                {
                    //LevelGift
                    SpawnKazan();
                }
            }
            GetRandomEnemyType().GetComponent<BasicEnemyMovement>().CreateEnemy(GetRandomSpawnLocationPosition());
            currentEnemyCount += 1;
            currentTimeBalancePerEnemy += 1;
            LevelBalance.Instance.currentEnemy++;
        }

    }
    GameObject GetRandomEnemyType()
    {
        return pool.GetPooledEnemy(Random.Range(0, enemysTypeCount));
    }
    Transform GetRandomSpawnLocationPosition()
    {
        return enemySpawnLocations[Random.Range(0, spawnLocationCount)];
    }

    void SpawnKazan()
    {
        Instantiate(spawnKazanObject, RandomPositionCreate(), Quaternion.identity);
    }
    Vector2 RandomPositionCreate()
    {
        // Kare alanýn orta noktasýný hesapla
        Vector2 squareCenter = (Vector2)transform.position;
        // Kare alanýn köþe noktalarýný hesapla
        Vector2 bottomLeft = squareCenter - squareSize / 2;
        Vector2 topRight = squareCenter + squareSize / 2;

        // Kare alan içinde rastgele bir konum oluþtur
        Vector2 randomPosition = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
        return randomPosition;

    }
    void LevelPositionChange()
    {
        foreach(Transform pos in enemySpawnLocations)
        {
            pos.position = RandomPositionCreate();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(squareSize.x, squareSize.y, 0));
    }

}
