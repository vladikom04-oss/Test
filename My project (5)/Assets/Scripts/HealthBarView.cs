using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    private Image healthBar;
    private Vector2 offset = new Vector2(0f, 2f);

    private Transform convas;
    private Transform target;

    private float maxValue;

    public void initialize()
    {
        target = transform.parent;
        Transform child = transform.GetChild(0);
        Transform childOfchild = child.GetChild(0);
        healthBar = childOfchild.GetComponent<Image>();
        convas = GameObject.FindGameObjectWithTag("UI").transform;
        transform.SetParent(convas);
    }
    private void Update()
    {
        if (target != null)
        {
             transform.position = new Vector2(target.position.x + offset.x, target.position.y + offset.y);
        }
    }

    public void SetMaxHealth(float health)
    {
        maxValue = health;
        healthBar.fillAmount = 1f;
    }

    public void SetHealth(float health)
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        healthBar.fillAmount = health / maxValue;
    }
}