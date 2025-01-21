using UnityEngine;

public class EscapeMenuManager : MonoBehaviour
{
    public GameObject escapeMenuCanvas; // The Canvas that will serve as the escape menu

    private void Start()
    {
        // Ensure the escape menu is hidden at the start of the game
        if (escapeMenuCanvas != null)
        {
            escapeMenuCanvas.SetActive(false);
            Debug.Log("Escape menu initialized as hidden.");
        }
        else
        {
            Debug.LogWarning("escapeMenuCanvas is not assigned in the Inspector!");
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key detected.");
            ToggleEscapeMenu();
        }
    }

    private void ToggleEscapeMenu()
    {
        if (escapeMenuCanvas == null)
        {
            Debug.LogWarning("Escape menu canvas is not assigned!");
            return;
        }

        bool isActive = !escapeMenuCanvas.activeSelf;
        escapeMenuCanvas.SetActive(isActive);

        Debug.Log("Toggling escape menu. New state is " + (isActive ? "active" : "inactive"));

        if (isActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Debug.Log("Escape menu is now active. Game paused.");
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            Debug.Log("Escape menu is now inactive. Game resumed.");
        }
    }
}
