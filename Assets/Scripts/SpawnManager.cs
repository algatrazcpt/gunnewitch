using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   
    public Vector2 squareSize; // Kare alan�n boyutu
    public int rangeEnemyId=0;//RangeEveryFirst
    public GameObject spawnKazanObject; // Olu�turulacak nesne

    public int timeBalancePerEnemy = 100;
    public int balancePerEnemy = 20;
    private int currentTimeBalancePerEnemy = 100;
    private int currentEnemyCount=0;

    private LevelBalance currentLevelBlance;
    EnemyPool pool;
    public Transform[] enemySpawnLocations;
    private int enemysTypeCount;
    private int spawnLocationCount;


    private void Start()
    {
        currentLevelBlance = LevelBalance.Instance;
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
        if (currentLevelBlance.currentEnemy < currentLevelBlance.maxEnemyCount)
        {
            if (balancePerEnemy == currentEnemyCount)
            {
                currentEnemyCount = 0;
                currentLevelBlance.LevelBalanceUp();
            }
            if (timeBalancePerEnemy == currentTimeBalancePerEnemy)
            {
                currentTimeBalancePerEnemy = 0;
                currentLevelBlance.TimeBalanceUp();
                CancelInvoke();
                SpawnEnemySystem(currentLevelBlance.GetTimeBalance());
                //
                LevelPositionChange();
                //
                for (int index = 0; index < currentLevelBlance.levelUpgradeBalance; index++)
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
        int currentType= Random.Range(0, enemysTypeCount);
        if(currentType==rangeEnemyId)
        {
            if (currentLevelBlance.maxRangeEnemyCount > currentLevelBlance.currentRangeEnemy)
            {
                currentLevelBlance.currentRangeEnemy++;
                return pool.GetPooledEnemy(rangeEnemyId);
            }
            else
            {
                return pool.GetPooledEnemy(Random.Range(rangeEnemyId+1, enemysTypeCount));
            }
        }
        return pool.GetPooledEnemy(currentType);
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
        // Kare alan�n orta noktas�n� hesapla
        Vector2 squareCenter = (Vector2)transform.position;
        // Kare alan�n k��e noktalar�n� hesapla
        Vector2 bottomLeft = squareCenter - squareSize / 2;
        Vector2 topRight = squareCenter + squareSize / 2;

        // Kare alan i�inde rastgele bir konum olu�tur
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
