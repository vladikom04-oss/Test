using System.Collections;
using UnityEngine;

public class WepaonView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fire;

    private Color fireColor;

    public void Initialize(IPlayerViewModel playerViewModel)
    {
        playerViewModel.OnShoot += Shoot;
        fireColor = fire.color;
        fireColor.a = 0f;
        fire.color = fireColor;
    }

    private void Shoot()
    {
        fireColor.a = 1f;
        fire.color = fireColor;
        StartCoroutine(ResetFire());
    }

    private IEnumerator ResetFire()
    {
        yield return new WaitForSeconds(0.2f);
        fireColor.a = 0f;
        fire.color = fireColor;
    }
}
