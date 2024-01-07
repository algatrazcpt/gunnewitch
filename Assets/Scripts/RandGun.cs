using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandGun : MonoBehaviour
{
    public WeaponList weapons;

    void Start()
    {
        weapons = GetComponentInChildren<WeaponList>();

        //weapons=GameObject.Find("WeaponHolder").GetComponent<WeaponList>(); //üsttekini Aynýsý farklý yöntem
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hittingObj = collision.gameObject;

        if (hittingObj.CompareTag("Random"))
        {
            weapons.currentWeapon.SetActive(false);
            int rand = Random.Range(0, 2);
            weapons.Weapon[rand].SetActive(true);
            weapons.currentWeapon = weapons.Weapon[rand];
            Debug.Log(rand);
        }
    }   
}
