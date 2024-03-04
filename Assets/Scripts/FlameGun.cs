using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGun : MonoBehaviour

{

    public GameObject flameRange;
    public GameObject flame;
    public ParticleSystem flamepart;
    public Transform weapon;
    public bool ozelkullan = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            flameRange.SetActive(true);
            flame.SetActive(true);
            flamepart.Play();
            flameRange.transform.position = weapon.position;
            flameRange.transform.rotation = weapon.rotation;
            if (ozelkullan)
            {
                flameRange.GetComponent<FlameAttack>().ozelAktifmi = true;
            }
            

        }
        if (Input.GetMouseButtonUp(0))
        {
            flameRange.SetActive(false);
            flamepart.Pause();
            flamepart.Clear();
            flame.SetActive(false);
            ozelkullan = false;
        }

        if (flameRange.GetComponent<FlameAttack>().ozelAktifmi)
        {
            flameRange.GetComponent<FlameAttack>().isFlamePowerOpen = true;
            flameRange.GetComponent<FlameAttack>().gecensure += Time.deltaTime;
            if (flameRange.GetComponent<FlameAttack>().gecensure > flameRange.GetComponent<FlameAttack>().refsüre)
            {
                flameRange.GetComponent<FlameAttack>().ozelAktifmi = false;
                flameRange.GetComponent<FlameAttack>().gecensure = 0f;
                flameRange.GetComponent<FlameAttack>().isFlamePowerOpen = false;
            }

        }


    }

   

}
