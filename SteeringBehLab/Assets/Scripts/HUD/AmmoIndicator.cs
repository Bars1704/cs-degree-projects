using TMPro;
using UnityEngine;

public class AmmoIndicator : MonoBehaviour
{
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private TextMeshProUGUI _ammoLabel;

    private void Start()
    {
        _playerShooting.OnAmmoChanged += (ammo) => { _ammoLabel.text = ammo.ToString(); };
    }
}