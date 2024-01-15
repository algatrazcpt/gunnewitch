using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimationControl : MonoBehaviour
{
    
    public Animator anims;
    AnimatorStateInfo animStateInfo;
    public GameObject weaponHolder;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weaponHolder.GetComponent<WeaponList>().currentWeapon.SetActive(false);
            anims.Play("Witch_range");
            





        }
        if(anims.GetCurrentAnimatorStateInfo(0).normalizedTime>0.9)
        {
            weaponHolder.GetComponent<WeaponList>().currentWeapon.SetActive(true);
            anims.Play("New State");
            

        }
    }
}
