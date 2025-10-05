using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private Slider _healthSlider;

    private void Start()
    {
        _healthSystem.OnHealtChanged.AddListener(UpdateHealthBar);

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthSlider.value = _healthSystem.GetHealthNormalized();
    }
}
