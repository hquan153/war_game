using UnityEngine;

public class Damage : MonoBehaviour
{
    private DamageController damageControllerScript;

    private const float speedUp = 0.04f;

    private void Awake()
    {
        damageControllerScript = transform.GetComponentInParent<DamageController>();
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf) transform.position += new Vector3(0, speedUp, 0);
    }

    public void Recall()
    {
        damageControllerScript.HideDamageGO(gameObject);
    }
}
