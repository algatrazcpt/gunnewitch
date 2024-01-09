using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    public Transform playerTransform;
    Rigidbody2D rg;
    void Start()
    {
        rg = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    Vector2 direction;
    void Update()
    {
        direction= new Vector2(playerTransform.position.x-transform.position.x, playerTransform.position.y-transform.position.y);
        rg.velocity = direction.normalized * 1.2f;
    }
}
