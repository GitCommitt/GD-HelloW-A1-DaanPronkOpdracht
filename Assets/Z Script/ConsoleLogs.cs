using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogPanelManager : MonoBehaviour
{
    public GameObject logPanel;
    public TextMeshProUGUI logText;
    public Button closeButton;
    public GameObject[] uiItemsToToggle;
    public CameraController cameraController;
    public CharacterController playerMovement;

    private void Start()
    {
        logPanel.SetActive(false);
        Application.logMessageReceived += Log;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            ToggleLogPanel();
        }
    }

    private void Log(string logString, string stackTrace, LogType type)
    {
        string logType = type.ToString();
        logText.text += $"{logType}: {logString}\n";

        if (logText.text.Split('\n').Length > 100)
        {
            string[] lines = logText.text.Split('\n');
            logText.text = string.Join("\n", lines, lines.Length - 99, 99);
        }

        Canvas.ForceUpdateCanvases();
        logText.GetComponent<RectTransform>().SetAsLastSibling();
    }

    private void ToggleLogPanel()
    {
        bool isActive = !logPanel.activeSelf;
        logPanel.SetActive(isActive);
        SetUIItemsActive(!isActive);

        if (isActive)
        {
            if (cameraController != null)
            {
                cameraController.enabled = false;
            }
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (cameraController != null)
            {
                cameraController.enabled = true;
            }
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void CloseLogPanel()
    {
        logPanel.SetActive(false);
        SetUIItemsActive(true);
        ToggleLogPanel();
    }

    private void SetUIItemsActive(bool isActive)
    {
        foreach (GameObject uiItem in uiItemsToToggle)
        {
            uiItem.SetActive(isActive);
        }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= Log;
    }
}
