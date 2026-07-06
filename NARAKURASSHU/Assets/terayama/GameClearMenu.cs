using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameClearMenu : MonoBehaviour
{
    public RectTransform retryButton;
    public RectTransform titleButton;
    public RectTransform exitButton;
    public Transform cursor;

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
                cursor.position = retryButton.transform.position + new Vector3(-155f, 0f, 0f);
                break;

            case 1:
                cursor.position = titleButton.transform.position + new Vector3(-155f, 0f, 0f);
                break;

            case 2:
                cursor.position = exitButton.transform.position + new Vector3(-155f, 0f, 0f);
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
