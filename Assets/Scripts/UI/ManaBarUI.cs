using UnityEngine;
using UnityEngine.UI;

public class ManaBarUI : MonoBehaviour
{
    [SerializeField] private ManaSystem _manaSystem;
    [SerializeField] private Slider _manaSlider;

    private void Start()
    {
        _manaSystem.OnManaChanged.AddListener(UpdateManaBar);

        UpdateManaBar();
    }

    private void UpdateManaBar()
    {
        _manaSlider.value = _manaSystem.GetManaNormalized();
    }
}
