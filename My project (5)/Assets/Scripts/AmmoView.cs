using TMPro;
using UnityEngine;

public class AmmoView : MonoBehaviour
{
    private TextMeshProUGUI ammo;

    private IPlayerViewModel viewModel;

    public void Initialize(IPlayerViewModel playerViewModel)
    {
        viewModel = playerViewModel;
        playerViewModel.OnShoot += Shoot;
        ammo = GetComponent<TextMeshProUGUI>();
        ammo.text = (viewModel.PlayerData.selectedWeaponAmmo).ToString();
    }

    private void Shoot()
    {
        ammo.text = (viewModel.PlayerData.selectedWeaponAmmo).ToString();
    }
}
