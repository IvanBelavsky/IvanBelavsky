using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Gun _gun;
    [SerializeField] private Gun _mediumGun;
    [SerializeField] private Gun _bigGun;
    void Start()
    {
        _gun.OnAmmoChange += DisplayAmmo;
        _mediumGun.OnAmmoChange += DisplayAmmo;
        _bigGun.OnAmmoChange += DisplayAmmo;
    }

    private void DisplayAmmo(float health, float maxHealth)
    {
        _bar.fillAmount = health / maxHealth;
    }
}
