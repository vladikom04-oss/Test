using System.Collections;
using UnityEngine;

public class MonsterView : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private HealthBarView healthBar;
    [SerializeField] private string lootItemId;

    private MonsterData _data;
    private Transform _player;
    private Rigidbody2D _rb;
    private bool _isAttacking;

    public void Initialize(MonsterData data)
    {
        _data = data;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        lootItemId = data.lootItemId;

        transform.position = _data.position;
    }
    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.initialize();
            healthBar.SetMaxHealth(_data.maxHealth);
            healthBar.SetHealth(_data.health);
        }
    }

    private void FixedUpdate()
    {
        if (_player == null || _isAttacking) return;

        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction = (_player.position - transform.position).normalized;
            transform.rotation = ReversePlayer(direction.x);
            _rb.velocity = direction * moveSpeed;

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void AttackPlayer()
    {
        _isAttacking = true;
        _rb.velocity = Vector2.zero;

        var player = _player.GetComponent<PlayerView>();
        if (player != null)
        {
            player.TakeDamage(10f);
        }

        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
        _isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        _data.health -= damage;
        healthBar.SetHealth(_data.health);

        if (_data.health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!string.IsNullOrEmpty(lootItemId))
        {
            var lootManager = FindObjectOfType<LootManager>();
            lootManager.SpawnLoot(lootItemId, transform.position);
        }

        Destroy(gameObject);
    }
    private Quaternion ReversePlayer(float x)
    {
        return (x > 0f)
        ? Quaternion.Euler(Vector3.zero)
        : Quaternion.Euler(180f, 0f, 180f);
    }
}