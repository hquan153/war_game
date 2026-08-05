using UnityEngine;

public class Damage : MonoBehaviour
{
    private DamageController damageControllerScript;

    private readonly Vector3 moveDirector = new(0, .04f, 0);

    private void Awake()
    {
        damageControllerScript = transform.GetComponentInParent<DamageController>();
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf) transform.position += moveDirector;
    }

    public void Recall()
    {
        damageControllerScript.HideDamageGO(gameObject);
    }
}
