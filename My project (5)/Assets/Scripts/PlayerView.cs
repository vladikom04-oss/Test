using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private HealthBarView healthBar;
    [SerializeField] private WepaonView wepaon;

    private Transform _hands;
    private Transform _head;
    private IPlayerViewModel _viewModel;
    private Vector2 _movement;

    private MonsterView monster;

    private bool isEnemyDetected;

    public void Initialize(IPlayerViewModel viewModel)
    {
        _viewModel = viewModel;
        wepaon.Initialize(viewModel);
        _viewModel.OnMove += HandleMove;
        _hands = transform.GetChild(3);
        _head = transform.GetChild(4);
        if (healthBar != null)
        {
            healthBar.initialize();
            healthBar.SetMaxHealth(_viewModel.PlayerData.maxHealth);
            healthBar.SetHealth(_viewModel.PlayerData.health);
        }
        _viewModel.OnShoot += Shoot;
    }

    private void FixedUpdate()
    {
        rb.velocity = _movement * moveSpeed;

        if (!isEnemyDetected)
        {
            if (_movement != Vector2.zero)
            {
                transform.rotation = ReversePlayer(_movement.x);
            }
            _hands.localRotation = Quaternion.Euler(Vector3.zero);
            _head.localRotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {

            Vector3 dir = (monster.transform.position - transform.position);

            transform.rotation = ReversePlayer(dir.x);
            float ang = Mathf.Atan2(dir.y, dir.x) / Mathf.PI * 180;
            Quaternion target;
            if (dir.x < 0f)
            {
                target = Quaternion.Euler(180, 0, -ang);
            }
            else
            {
                target = Quaternion.Euler(0, 0, ang);
            }

            _hands.rotation = Quaternion.Slerp(transform.rotation, target, 10f);
            _head.rotation = Quaternion.Slerp(transform.rotation, target, 10f);
        }
    }

    private void HandleMove(Vector2 direction)
    {
        _movement = direction;
    }

    public void TakeDamage(float damage)
    {
        _viewModel.PlayerData.health -= damage;
        if(healthBar != null)
        {
            healthBar.SetHealth(_viewModel.PlayerData.health);
        }

        if (_viewModel.PlayerData.health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enabled = false;
        _viewModel.Die();
        Destroy(gameObject);
    }

    private void Shoot()
    {
        if (monster != null)
        {
            monster.TakeDamage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            WorldItemView itemView = collision.gameObject.GetComponent<WorldItemView>();
            _viewModel.PickupItem(itemView.ItemId);
            itemView.Collect();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isEnemyDetected = true;
            monster = collision.GetComponent<MonsterView>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isEnemyDetected = false;
            monster = null;
        }
    }
    private Quaternion ReversePlayer(float x)
    {
        return (x > 0f)
        ? Quaternion.Euler(Vector3.zero) 
        : Quaternion.Euler(180f, 0f, 180f);
    }
}