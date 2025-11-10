using System.Collections;
using UnityEngine;

public class WepaonView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fire;

    private Color fireColor;

    private IPlayerViewModel _viewModel;
    public void Initialize(IPlayerViewModel playerViewModel)
    {
        _viewModel = playerViewModel;
        _viewModel.OnShoot += Shoot;
        _viewModel.OnDied += Disponse;
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
    private void Disponse()
    {
        _viewModel.OnShoot -= Shoot;
        _viewModel.OnDied -= Disponse;
    }
}
