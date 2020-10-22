using UnityEngine;
using UnityEngine.UI;

public class ValueSlider : MonoBehaviour
{
    public FloatVariable variable;
    public Slider slider;

    private void OnEnable()
    {
        slider.value = variable.value;
    }

    public void ChangeValue()
    {
        variable.value = slider.value;
    }
}
