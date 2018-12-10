using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{

    public float value;
    public float minValue;
    public float maxValue;

    [SerializeField]
    [Range(0, 1)]
    private float fillAmount;

    [SerializeField]
    private Image fill;

    void Update()
    {

        UpdateValue();

    }

    void UpdateValue()
    {

        fill.fillAmount = MapValue(value, minValue, maxValue);

    }

    float MapValue(float value, float minValue, float maxValue)
    {

        return (value - minValue) / (maxValue - minValue);

    }

}
