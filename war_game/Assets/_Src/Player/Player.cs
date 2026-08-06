using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private const float degree = 360;
    private const float speed = 12f;

    private PlayerController playerControllerScript;
    private DamageController damageControllerScript;

    private Audio audioScript;

    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;
    private TMP_Text healthTMP;

    private SpriteRenderer[] spriteRenderers;

    private PlayerData m_player;
    public PlayerData PlayerF
    {
        get => m_player;
        set
        {
            m_player = value;
            healthTMP.text = m_player.health.ToString();
        }
    }

    private const float growTime = .3f;
    private float growTimer = 0f;
    private bool isGrowing = false;

    private const float deadTime = growTime;
    private float deadTimer = 0f;
    private bool isDisappearing = false;

    private void Awake()
    {
        playerControllerScript = transform.GetComponentInParent<PlayerController>();
        damageControllerScript = GameObject.FindGameObjectWithTag("DamageController").GetComponent<DamageController>();
        audioScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();

        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        collider2d = transform.GetComponent<Collider2D>();
        healthTMP = transform.Find("Health").GetComponent<TMP_Text>();

        spriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isGrowing)
        {
            growTimer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * PlayerF.size, growTimer / growTime);

            if (growTimer < growTime) return;
            growTimer = 0f;
            isGrowing = false;
        }

        if (isDisappearing)
        {
            deadTimer += Time.deltaTime;

            SetAlphaForChildren(Mathf.Lerp(1f, 0f, deadTimer / deadTime));

            if (deadTimer < deadTime) return;
            deadTimer = 0f;
            isDisappearing = false;
            Recall();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform collisionTransform = collision.transform;
        if (collisionTransform.CompareTag("DeathZone"))
        {
            IsDead(true);
            playerControllerScript.Players = PlayerF;
            return;
        }
        if (!collisionTransform.CompareTag("Player")) return;

        audioScript.PlayAudio();

        int collisionDamage = collisionTransform.GetComponent<Player>().PlayerF.damage;
        PlayerF.health -= collisionDamage;
        healthTMP.text = PlayerF.health.ToString();

        damageControllerScript.ShowDamageGO(transform.position, collisionDamage);

        IsDead();
    }

    private void OnEnable()
    {
        rigidbody2d.linearVelocity = (new Vector2(Random.Range(-degree, degree), Random.Range(-degree, degree))).normalized * speed;
        isGrowing = true;
    }

    private void OnDestroy()
    {
        IsDead(true);
    }

    private void IsDead(bool isDead = false)
    {
        if (PlayerF.health > 0 && !isDead) return;
        if (!gameObject.activeSelf && !isDead) return;
        PlayerF.health = 0;

        rigidbody2d.linearVelocity = Vector2.zero;
        collider2d.isTrigger = true;

        isGrowing = false;
        isDisappearing = true;
    }

    private void Recall()
    {
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.zero;

        collider2d.isTrigger = false;
        SetAlphaForChildren(1f);

        playerControllerScript.PlayerDeadHandler(gameObject);
    }

    private void SetAlphaForChildren(float alpha)
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
    }
}
