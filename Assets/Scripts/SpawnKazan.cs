using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKazan : MonoBehaviour
{
    public GameObject objectToSpawn; // Olu�turulacak nesne
    public Vector2 squareSize; // Kare alan�n boyutu

    void Start()
    {

        // Olu�turulan konumda nesneyi olu�tur

        InvokeRepeating("Spawn", 0, 5);
    }
    void Spawn()
    {
        // Kare alan�n orta noktas�n� hesapla
        Vector2 squareCenter = (Vector2)transform.position;

        // Kare alan�n k��e noktalar�n� hesapla
        Vector2 bottomLeft = squareCenter - squareSize / 2;
        Vector2 topRight = squareCenter + squareSize / 2;

        // Kare alan i�inde rastgele bir konum olu�tur
        Vector2 randomPosition = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));

        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }
    void PortalLocationChange()
    {
        Vector2 squareCenter = (Vector2)transform.position;

        // Kare alan�n k��e noktalar�n� hesapla
        Vector2 bottomLeft = squareCenter - squareSize / 2;
        Vector2 topRight = squareCenter + squareSize / 2;

        // Kare alan i�inde rastgele bir konum olu�tur
        Vector2 randomPosition = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));


    }
    // Kare alan�n s�n�rlar�n� g�rselle�tirmek i�in OnDrawGizmos i�levini kullanabilirsiniz.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(squareSize.x, squareSize.y, 0));
    }
}
