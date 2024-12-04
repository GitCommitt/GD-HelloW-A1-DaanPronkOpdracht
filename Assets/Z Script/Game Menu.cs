using UnityEngine;
using UnityEngine.UI; // Zorg ervoor dat je deze toevoegt voor UI-elementen

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel; // Het menu panel dat we gebruiken
    public GameObject[] otherUIElements; // Andere UI-elementen die we willen verbergen
    public GameObject hud; // UI-element voor HUD
    public GameObject radar; // UI-element voor Radar
    public GameObject fpsText; // UI-element voor FPS
    public GameObject dateText; // UI-element voor Datum/Tijd
    public CameraController cameraController; // Verwijzing naar het camera controller script

    public Button hudButton; // Koppeling naar de HUD toggle knop
    public Button radarButton; // Koppeling naar de Radar toggle knop
    public Button fpsButton; // Koppeling naar de FPS toggle knop
    public Button dateButton; // Koppeling naar de Datum/Tijd toggle knop

    private bool isGamePaused = false; // Houdt bij of het spel gepauzeerd is 

    private void Start()
    {
        // Zorg ervoor dat het menu niet zichtbaar is bij de start van het spel
        menuPanel.SetActive(false);
        SetUIElementsActive(true); // Zorg ervoor dat andere UI-elementen zichtbaar zijn

        // Maak de cursor onzichtbaar bij het starten
        Cursor.lockState = CursorLockMode.Locked; // Vergrendel de cursor
        Cursor.visible = false; // Maak de cursor onzichtbaar

        // Zet de datum/tijd in de UI
        UpdateDateTime();

        // Zet de knoppenkleur op basis van de zichtbaarheid van de UI-elementen
        UpdateButtonColors();
    }

    private void Update()
    {
        // Detecteer de F1-toets om het menu te toggelen
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleMenu();
        }

        // Update de FPS weergave
        UpdateFPS();
    }

    private void ToggleMenu()
    {
        // Wijzig de status van isGamePaused
        isGamePaused = !isGamePaused;

        // Stel het menu in op de huidige status
        menuPanel.SetActive(isGamePaused);

        // Pauze of hervat het spel op basis van de menu-status
        if (isGamePaused)
        {
            Time.timeScale = 0f; // Pauze de game
            SetUIElementsActive(false); // Zet andere UI-elementen inactief
            Cursor.lockState = CursorLockMode.None; // Laat de cursor vrij
            Cursor.visible = true; // Maak de cursor zichtbaar

            // Schakel de camera controller uit
            if (cameraController != null)
            {
                cameraController.enabled = false; // Deactiveer camera beweging
            }
        }
        else
        {
            Time.timeScale = 1f; // Hervat de game
            SetUIElementsActive(true); // Zet andere UI-elementen actief
            Cursor.lockState = CursorLockMode.Locked; // Vergrendel de cursor
            Cursor.visible = false; // Maak de cursor onzichtbaar

            // Schakel de camera controller in
            if (cameraController != null)
            {
                cameraController.enabled = true; // Heractiveer camera beweging
            }
        }
    }

    private void SetUIElementsActive(bool isActive)
    {
        foreach (GameObject uiElement in otherUIElements)
        {
            uiElement.SetActive(isActive);
        }
    }

    private void UpdateFPS()
    {
        if (fpsText != null)
        {
            float fps = 1.0f / Time.deltaTime; // Bereken de frames per seconde
            fpsText.GetComponent<Text>().text = "FPS: " + Mathf.Ceil(fps).ToString(); // Update de tekst
        }
    }

    private void UpdateDateTime()
    {
        if (dateText != null)
        {
            System.DateTime now = System.DateTime.Now;
            dateText.GetComponent<Text>().text = "Datum: " + now.ToString("dd-MM-yyyy HH:mm:ss"); // Update de tekst
        }
    }

    // Toggle de HUD zichtbaarheid
    public void ToggleHUDVisibility()
    {
        if (hud != null)
        {
            bool isVisible = !hud.activeSelf; // Bepaal de nieuwe zichtbaarheid
            hud.SetActive(isVisible); // Toggle de HUD
            UpdateButtonColor(hudButton, isVisible); // Update de knopkleur
            Debug.Log("HUD is now " + (isVisible ? "visible." : "hidden."));
        }
    }

    // Toggle de Radar zichtbaarheid
    public void ToggleRadarVisibility()
    {
        if (radar != null)
        {
            bool isVisible = !radar.activeSelf; // Bepaal de nieuwe zichtbaarheid
            radar.SetActive(isVisible); // Toggle de Radar
            UpdateButtonColor(radarButton, isVisible); // Update de knopkleur
            Debug.Log("Radar is now " + (isVisible ? "visible." : "hidden."));
        }
    }

    // Toggle de FPS zichtbaarheid
    public void ToggleFPSVisibility()
    {
        if (fpsText != null)
        {
            bool isVisible = !fpsText.activeSelf; // Bepaal de nieuwe zichtbaarheid
            fpsText.SetActive(isVisible); // Toggle de FPS
            UpdateButtonColor(fpsButton, isVisible); // Update de knopkleur
            Debug.Log("FPS is now " + (isVisible ? "visible." : "hidden."));
        }
    }

    // Toggle de Datum/Tijd zichtbaarheid
    public void ToggleDateVisibility()
    {
        if (dateText != null)
        {
            bool isVisible = !dateText.activeSelf; // Bepaal de nieuwe zichtbaarheid
            dateText.SetActive(isVisible); // Toggle de Datum/Tijd
            UpdateButtonColor(dateButton, isVisible); // Update de knopkleur
            Debug.Log("Datum/Tijd is now " + (isVisible ? "visible." : "hidden."));
        }
    }



    // Update de kleur van de knoppen op basis van zichtbaarheid
    private void UpdateButtonColor(Button button, bool isVisible)
    {
        ColorBlock colorBlock = button.colors; // Krijg de huidige kleuren van de knop
        if (isVisible)
        {
            colorBlock.normalColor = Color.green; // Zet de kleur op groen als zichtbaar
            colorBlock.highlightedColor = Color.green; // Zet de hover kleur op groen
            colorBlock.pressedColor = Color.green; // Zet de ingedrukte kleur op groen
            colorBlock.selectedColor = Color.green;
        }
        else
        {
            colorBlock.normalColor = Color.red; // Zet de kleur op rood als verborgen
            colorBlock.highlightedColor = Color.red; // Zet de hover kleur op rood
            colorBlock.pressedColor = Color.red; // Zet de ingedrukte kleur op rood
            colorBlock.selectedColor = Color.red;
        }
        button.colors = colorBlock; // Pas de kleuren toe
    }

    private void UpdateButtonColors()
    {
        // Update de kleuren van de knoppen bij de start
        UpdateButtonColor(hudButton, hud.activeSelf);
        UpdateButtonColor(radarButton, radar.activeSelf);
        UpdateButtonColor(fpsButton, fpsText.activeSelf);
        UpdateButtonColor(dateButton, dateText.activeSelf);
    }
}