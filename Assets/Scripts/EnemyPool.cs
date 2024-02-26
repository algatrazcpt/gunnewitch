using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public Transform player;
    public static EnemyPool Instance;
    public GameObject[] enemyPrefabs; // Farkl� d��man varyantlar�n� i�eren dizi
    public int poolSizePerVariant = 10;

    private List<GameObject>[] pooledEnemies; // D��man varyantlar� i�in ayr� havuzlar

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitializeObjectPools();
    }

    private void InitializeObjectPools()
    {
        // Her bir d��man varyant� i�in havuzlar� olu�tur
        pooledEnemies = new List<GameObject>[enemyPrefabs.Length];

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            pooledEnemies[i] = new List<GameObject>();

            for (int j = 0; j < poolSizePerVariant; j++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[i]);
                enemy.GetComponent<BasicEnemyMovement>().playerTransform = player;
                enemy.SetActive(false);
                pooledEnemies[i].Add(enemy);
            }
        }
    }

    public GameObject GetPooledEnemy(int variantIndex)
    {
        // Belirli bir d��man varyant�n�n havuzundan bir d��man nesnesi al
        foreach (GameObject enemy in pooledEnemies[variantIndex])
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        GameObject newEnemy = Instantiate(enemyPrefabs[variantIndex]);
        newEnemy.SetActive(true);
        newEnemy.GetComponent<BasicEnemyMovement>().playerTransform = player;
        pooledEnemies[variantIndex].Add(newEnemy); // Listeye yeni d��man� ekle
        return newEnemy;
    }
}
