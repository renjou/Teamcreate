using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI exitText;

    private int selected = 0;

    void Start()
    {
        UpdateMenu();
    }

    void Update()
    {
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            selected++;

            if (selected > 2)
                selected = 0;

            UpdateMenu();
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            selected--;

            if (selected < 0)
                selected = 2;

            UpdateMenu();
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SelectMenu();
        }
    }

    void UpdateMenu()
    {
        startText.text = "START";
        controlsText.text = "CONTROLS";
        exitText.text = "EXIT";

        switch (selected)
        {
            case 0:
                startText.text = "▶ START";
                break;

            case 1:
                controlsText.text = "▶ CONTROLS";
                break;

            case 2:
                exitText.text = "▶ EXIT";
                break;
        }
    }

    void SelectMenu()
    {
        switch (selected)
        {
            case 0:
                SceneManager.LoadScene("Game");
                break;

            case 1:
                Debug.Log("Controls");
                break;

            case 2:
                Application.Quit();
                break;
        }
    }
}