using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGun : MonoBehaviour

{

    public GameObject flameRange;
    public GameObject flame;
    public ParticleSystem flamepart;
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

        }
        if (Input.GetMouseButtonUp(0))
        {
            flameRange.SetActive(false);
            flamepart.Pause();
            flamepart.Clear();
            flame.SetActive(false);

        }
    }
    
}
