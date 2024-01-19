using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAttack : MonoBehaviour
{
    public bool isTrig = false;
    PopController getPoper;
    public float damage;
    public List<GameObject> alanaGirenler = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        getPoper = PopController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy == false)
        {
            alanaGirenler.Clear();
        }

        if (isTrig == true)
        {
            StartCoroutine("attackColdown");
            Debug.Log("ListeDönüldü");
            

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("OBJE eklendi");
            isTrig = true;
            alanaGirenler.Add(collision.gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            alanaGirenler.Remove(collision.gameObject);
            if (alanaGirenler.Count == 0)
            {
                isTrig = false;
            }

            

        }

    }
    IEnumerator attackColdown()
    {
        isTrig = false;
        foreach (GameObject obj in alanaGirenler)
        {
            getPoper.GetObjectFromPool().GetComponent<DamagePop>().DamageCreate(obj.transform.position, damage);
        }

        yield return new WaitForSeconds(0.5f);
        isTrig = true;


    }
}
