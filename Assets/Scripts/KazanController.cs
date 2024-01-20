using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KazanController : MonoBehaviour
{
    public GameObject speels;
    public Sprite[] randomItems;
    bool oneTime = true;
   public  int currentRandom=0;
    public SpriteRenderer itemSprite;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (oneTime)
        {
            if (collision.transform.CompareTag("Player"))
            {
                currentRandom = Random.Range(0, randomItems.Length);
                itemSprite.sprite = randomItems[currentRandom];
                oneTime = false;
            }
            animator.SetTrigger("Idle");
        }
    }
    IEnumerator KazanDestroy()
    {
        animator.SetTrigger("Destroy");
        yield return new WaitForSeconds(1f);
        if (currentRandom > 2)
        {
            GameObject current = Instantiate(speels, transform.position,transform.rotation);
            current.GetComponent<ItemControl>().healt = Random.Range(0, 100);
            current.GetComponent<ItemControl>().speel = Random.Range(0, 100);
            PlayerKazanDestroy();
        }
        oneTime = true;
        gameObject.SetActive(false);
    }

    public int GetRandomItem()
    {
        if(currentRandom>2)
        {
            return -1;
        }
        return currentRandom;
    }
   public void PlayerKazanDestroy()
    {
        StartCoroutine("KazanDestroy");
    }
}
