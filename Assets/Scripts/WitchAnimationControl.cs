using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimationControl : MonoBehaviour
{
    
    public Animator anims;
    AnimatorStateInfo animStateInfo;
    public GameObject weaponHolder;
    bool rageAnim = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&rageAnim)
        {
            anims.Play("Witch_range");
            StartCoroutine("WitchAnim");

        }
    }
    IEnumerator WitchAnim()
    {
        rageAnim = false;
        weaponHolder.GetComponent<WeaponList>().currentWeapon.SetActive(false);
        yield return new WaitForSeconds(2f);
        weaponHolder.GetComponent<WeaponList>().currentWeapon.SetActive(true);
        rageAnim = true;
    }
}
