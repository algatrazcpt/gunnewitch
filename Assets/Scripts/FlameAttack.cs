using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAttack : MonoBehaviour
{
    public bool isTrig = false;
    PopController getPoper;
    public float damage=5f;
    public float gecensure = 0f;
    public List<GameObject> alanaGirenler = new List<GameObject>();
    public float offset;

    public bool ozelAktifmi = false;

    public bool isFlamePowerOpen = false;
    public float refsüre=8f;

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

        //if (ozelAktifmi)
        //{
        //    isFlamePowerOpen = true;
        //    gecensure += Time.deltaTime;
        //    if (gecensure > refsüre)
        //    {
        //        ozelAktifmi = false;
        //        gecensure = 0f;
        //        isFlamePowerOpen = false;
        //    }

        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("OBJE eklendi");
            isTrig = true;
            alanaGirenler.Add(collision.gameObject);

        }
        if (isFlamePowerOpen)
        {
            if (collision.gameObject.CompareTag("enemyBullet"))
            {
                Debug.Log("lava mermi girdi");
                collision.gameObject.SetActive(false);

            }
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
            obj.gameObject.GetComponent<BasicEnemyMovement>().EnemyTakeDamage(damage);
        }

        yield return new WaitForSeconds(0.7f);
        isTrig = true;


    }


}
