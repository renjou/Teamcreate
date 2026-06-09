using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameClearMenu : MonoBehaviour
{
    public TextMeshProUGUI retryText;
    public TextMeshProUGUI titleText;
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
        retryText.text = "RETRY";
        titleText.text = "TITLE";
        exitText.text = "EXIT";

        switch (selected)
        {
            case 0:
                retryText.text = "▶ RETRY";
                break;

            case 1:
                titleText.text = "▶ TITLE";
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
                Time.timeScale = 1f;
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().buildIndex);
                break;

            case 1:
                Time.timeScale = 1f;
                SceneManager.LoadScene("Title");
                break;

            case 2:
                Application.Quit();
                break;
        }
    }
}
