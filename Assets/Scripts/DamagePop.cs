using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePop : MonoBehaviour
{
    public TMP_Text cText;
    public float closeTime;
    public void DamageCreate(Vector3 currentPos, float currentDamage)
    {
        gameObject.SetActive(true);
        cText.text = currentDamage.ToString();
        transform.position = currentPos;
        StartCoroutine(MoveObjectAndReturnToPool(closeTime));
    }
    IEnumerator MoveObjectAndReturnToPool(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.Translate(Vector3.up * 1f * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false); // Objeyi havuza geri döndür
    }


}
