using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public float healt = 10;
    public float speel = 10;
    public float deleteTime = 10f;
    Material cMaterial;

    public void Start()
    {
        cMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        
        InvokeRepeating("Effecct", 0, 0.5f);
        Invoke("Delete", deleteTime);
    }
    void Effecct()
    {
        cMaterial.SetFloat("_FlashCount", deleteTime/10);
        deleteTime -= 1;
    }


    void Delete()
    {
        Destroy(gameObject);
    }
}
