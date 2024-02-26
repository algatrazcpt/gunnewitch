using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKazan : MonoBehaviour
{
    public GameObject objectToSpawn; // Oluþturulacak nesne
    public Vector2 squareSize; // Kare alanýn boyutu

    void Start()
    {

        // Oluþturulan konumda nesneyi oluþtur

        InvokeRepeating("Spawn", 0, 5);
    }
    void Spawn()
    {
        // Kare alanýn orta noktasýný hesapla
        Vector2 squareCenter = (Vector2)transform.position;

        // Kare alanýn köþe noktalarýný hesapla
        Vector2 bottomLeft = squareCenter - squareSize / 2;
        Vector2 topRight = squareCenter + squareSize / 2;

        // Kare alan içinde rastgele bir konum oluþtur
        Vector2 randomPosition = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));

        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }
    void PortalLocationChange()
    {
        Vector2 squareCenter = (Vector2)transform.position;

        // Kare alanýn köþe noktalarýný hesapla
        Vector2 bottomLeft = squareCenter - squareSize / 2;
        Vector2 topRight = squareCenter + squareSize / 2;

        // Kare alan içinde rastgele bir konum oluþtur
        Vector2 randomPosition = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));


    }
    // Kare alanýn sýnýrlarýný görselleþtirmek için OnDrawGizmos iþlevini kullanabilirsiniz.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(squareSize.x, squareSize.y, 0));
    }
}
