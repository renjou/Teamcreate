using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI exitText;
    public RectTransform cursor;

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
        switch (selected)
        {
            case 0:
                cursor.position =
                    startText.rectTransform.position + new Vector3(-50f, 0f, 0f);
                break;

            case 1:
                cursor.position =
                    controlsText.rectTransform.position + new Vector3(-50f, 0f, 0f);
                break;

            case 2:
                cursor.position =
                    exitText.rectTransform.position + new Vector3(-50f, 0f, 0f);
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