using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageController : MonoBehaviour
{
    private readonly Queue<GameObject> damagesGO = new();
    private readonly Vector3 extendPosition = new(0.2f, 0.2f, 0);

    private void Awake()
    {
        foreach (GameObject damageGO in GameObject.FindGameObjectsWithTag("Damage"))
        {
            damagesGO.Enqueue(damageGO);
            damageGO.SetActive(false);
        }
    }

    public void ShowDamageGO(Vector3 hitPosition, int damage)
    {
        GameObject damageGO;
        if (damagesGO.Count > 0)
        {
            damageGO = damagesGO.Dequeue();
            damageGO.SetActive(true);
        }
        else
        {
            GameObject newDamageGO = Resources.Load<GameObject>("Prefabs/Damage");
            damageGO = Instantiate(newDamageGO, transform.localPosition, Quaternion.identity, transform);
            damageGO.name = "Damage";
        }

        damageGO.transform.position = hitPosition + extendPosition;
        damageGO.GetComponent<TMP_Text>().text = $"-{damage}";
    }

    public void HideDamageGO(GameObject gameObjectGO)
    {
        damagesGO.Enqueue(gameObjectGO);
        gameObjectGO.SetActive(false);
    }
}

