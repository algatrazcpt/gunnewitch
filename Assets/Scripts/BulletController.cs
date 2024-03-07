using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float maxSpeed = 10f; // Maksimum mermi h�z�
    public float accelerationTime = 2f; // H�z art��� s�resi

    private Vector2 targetPosition; // Mermi hedef pozisyonu
    private Rigidbody2D rb; // Mermiye ait Rigidbody bile�eni
    private Vector2 direction; // Mermi hareket y�n�
    private float currentSpeed = 0f; // Anl�k mermi h�z�

}
