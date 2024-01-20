using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopController : MonoBehaviour
{
    public static PopController instance;
    public GameObject prefab; // Nesnenin prefab'ý
    public int poolSize = 10; // Nesne havuzunun boyutu

    private List<GameObject> objectPool = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        // Havuzdaki nesneleri oluþtur
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(gameObject.transform);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        // Havuzdan etkin olmayan bir nesne al
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }
         
        // Havuzdaki tüm nesneler etkinse, yeni bir nesne oluþtur
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        newObj.transform.SetParent(gameObject.transform);
        objectPool.Add(newObj);

        return newObj;
    }

}
