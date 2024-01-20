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
    public void setWeapon(int id)
    {
        weapons.currentWeapon.SetActive(false);
        weapons.Weapon[id].SetActive(true);
        weapons.currentWeapon = weapons.Weapon[id];
    }
}
