using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject[] otherUIElements;
    public GameObject hud;
    public GameObject radar;
    public GameObject fpsText;
    public GameObject dateText;
    public CameraController cameraController;

    public Button hudButton;
    public Button radarButton;
    public Button fpsButton;
    public Button dateButton;

    private bool isGamePaused = false;

    private void Start()
    {
        menuPanel.SetActive(false);
        SetUIElementsActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UpdateDateTime();
        UpdateButtonColors();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleMenu();
        }
        UpdateFPS();
    }

    private void ToggleMenu()
    {
        isGamePaused = !isGamePaused;
        menuPanel.SetActive(isGamePaused);

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            SetUIElementsActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (cameraController != null)
            {
                cameraController.enabled = false;
            }
        }
        else
        {
            Time.timeScale = 1f;
            SetUIElementsActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (cameraController != null)
            {
                cameraController.enabled = true;
            }
        }
    }

    private void SetUIElementsActive(bool isActive)
    {
        foreach (GameObject uiElement in otherUIElements)
        {
            if (uiElement.activeSelf != isActive)
            {
                uiElement.SetActive(isActive);
            }
        }
    }

    private void UpdateFPS()
    {
        if (fpsText != null)
        {
            float fps = 1.0f / Time.deltaTime;
            fpsText.GetComponent<Text>().text = "FPS: " + Mathf.Ceil(fps).ToString();
        }
    }

    private void UpdateDateTime()
    {
        if (dateText != null)
        {
            System.DateTime now = System.DateTime.Now;
            dateText.GetComponent<Text>().text = "Datum: " + now.ToString("dd-MM-yyyy HH:mm:ss");
        }
    }

    public void ToggleHUDVisibility()
    {
        if (hud != null)
        {
            bool isVisible = !hud.activeSelf;
            hud.SetActive(isVisible);
            UpdateButtonColor(hudButton, isVisible);
            Debug.Log("HUD is now " + (isVisible ? "visible." : "hidden."));
        }
    }

    public void ToggleRadarVisibility()
    {
        if (radar != null)
        {
            bool isVisible = !radar.activeSelf;
            radar.SetActive(isVisible);
            UpdateButtonColor(radarButton, isVisible);
            Debug.Log("Radar is now " + (isVisible ? "visible." : "hidden."));
        }
    }

    public void ToggleFPSVisibility()
    {
        if (fpsText != null)
        {
            bool isVisible = !fpsText.activeSelf;
            fpsText.SetActive(isVisible);
            UpdateButtonColor(fpsButton, isVisible);
            Debug.Log("FPS is now " + (isVisible ? "visible." : "hidden."));
        }
    }

    public void ToggleDateVisibility()
    {
        if (dateText != null)
        {
            bool isVisible = !dateText.activeSelf;
            dateText.SetActive(isVisible);
            UpdateButtonColor(dateButton, isVisible);
            Debug.Log("Datum/Tijd is now " + (isVisible ? "visible." : "hidden."));
        }
    }

    private void UpdateButtonColor(Button button, bool isVisible)
    {
        ColorBlock colorBlock = button.colors;
        if (isVisible)
        {
            colorBlock.normalColor = Color.green;
            colorBlock.highlightedColor = Color.green;
            colorBlock.pressedColor = Color.green;
            colorBlock.selectedColor = Color.green;
        }
        else
        {
            colorBlock.normalColor = Color.red;
            colorBlock.highlightedColor = Color.red;
            colorBlock.pressedColor = Color.red;
            colorBlock.selectedColor = Color.red;
        }
        button.colors = colorBlock;
    }

    private void UpdateButtonColors()
    {
        UpdateButtonColor(hudButton, hud.activeSelf);
        UpdateButtonColor(radarButton, radar.activeSelf);
        UpdateButtonColor(fpsButton, fpsText.activeSelf);
        UpdateButtonColor(dateButton, dateText.activeSelf);
    }
}
