using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float maxSpeed = 10f; // Maksimum mermi hýzý
    public float accelerationTime = 2f; // Hýz artýþý süresi

    private Vector2 targetPosition; // Mermi hedef pozisyonu
    private Rigidbody2D rb; // Mermiye ait Rigidbody bileþeni
    private Vector2 direction; // Mermi hareket yönü
    private float currentSpeed = 0f; // Anlýk mermi hýzý

}
