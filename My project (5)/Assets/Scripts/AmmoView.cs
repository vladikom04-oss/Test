using TMPro;
using UnityEngine;

public class AmmoView : MonoBehaviour
{
    private TextMeshProUGUI ammo;

    private IPlayerViewModel _viewModel;

    public void Initialize(IPlayerViewModel playerViewModel)
    {
        _viewModel = playerViewModel;
        _viewModel.OnShoot += Shoot;
        _viewModel.OnDied += Disponse;
        ammo = GetComponent<TextMeshProUGUI>();
        ammo.text = (_viewModel.PlayerData.selectedWeaponAmmo).ToString();
    }

    private void Shoot()
    {
        ammo.text = (_viewModel.PlayerData.selectedWeaponAmmo).ToString();
    }

    private void Disponse()
    {
        _viewModel.OnShoot -= Shoot;
        _viewModel.OnDied -= Disponse;
    }
}
