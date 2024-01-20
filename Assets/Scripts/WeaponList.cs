using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    int weaponmax =1;
    public int currentWeaponIndex;
    public GameObject[] Weapon;
    public GameObject weaponHolder;
    public GameObject currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        weaponmax = weaponHolder.transform.childCount;
        Weapon = new GameObject[weaponmax];
        for(int i = 0; i < weaponmax; i++)
        {
            Weapon[i] = weaponHolder.transform.GetChild(i).gameObject;
            Weapon[i].SetActive(false);


        }
        Weapon[0].SetActive(true);
        currentWeapon = Weapon[0];
        currentWeaponIndex = 0;
    }
}
