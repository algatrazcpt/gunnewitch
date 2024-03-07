using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab�
    public float bulletSpeed = 10f; // Mermi h�z�

    private Transform player; // Oyuncunun transformu


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncunun pozisyonunu bul
        InvokeRepeating("Ates", 0, 1);
    }

    void Ates()
    {
        // Mermi i�in var�� s�resini hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float bulletTravelTime = distanceToPlayer / bulletSpeed;

        // Player'�n gelecekteki konumunu tahmin et
        Vector2 futurePosition = (Vector2)player.position + player.GetComponent<Rigidbody2D>().velocity * bulletTravelTime;

        // Hedef pozisyonunu ve y�n�n� hesapla
        Vector2 direction = futurePosition - (Vector2)transform.position;

        // Mermiyi olu�tur ve hedefe do�ru f�rlat

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // Mermiyi olu�tur
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Mermiye ait Rigidbody2D bile�enini al
        rb.velocity = direction.normalized * bulletSpeed; // Mermiye h�z� uygula
    }
}
