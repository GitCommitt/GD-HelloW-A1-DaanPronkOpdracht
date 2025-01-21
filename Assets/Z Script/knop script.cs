using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;
    public Button toggleButton;

    private bool isCanvas1Active = true;

    void Start()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
        toggleButton.onClick.AddListener(ToggleCanvas);
    }

    void ToggleCanvas()
    {
        isCanvas1Active = !isCanvas1Active;
        canvas1.SetActive(isCanvas1Active);
        canvas2.SetActive(!isCanvas1Active);

        if (isCanvas1Active)
        {
            toggleButton.GetComponentInChildren<Text>().text = "Open Canvas 2";
        }
        else
        {
            toggleButton.GetComponentInChildren<Text>().text = "Open Canvas 1";
        }
    }
}
