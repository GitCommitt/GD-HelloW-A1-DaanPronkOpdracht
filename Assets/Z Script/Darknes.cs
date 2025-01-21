using UnityEngine;
using UnityEngine.UI;

public class OpacityController : MonoBehaviour
{
    [Header("UI Components")]
    public Slider opacitySlider;
    public Text opacityText;
    public RawImage background;

    void Start()
    {
        opacitySlider.minValue = 0;
        opacitySlider.maxValue = 100;
        opacitySlider.value = 100;
        UpdateOpacity();

        opacitySlider.onValueChanged.AddListener(delegate { UpdateOpacity(); });
    }

    void UpdateOpacity()
    {
        float opacityValue = opacitySlider.value;
        opacityText.text = $"Helderheid: {opacityValue:F0}%";

        Color currentColor = background.color;
        currentColor.a = 1 - (opacityValue / 100f);
        background.color = currentColor;
    }
}
