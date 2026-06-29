using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameClearMenu : MonoBehaviour
{
    public RectTransform retryButton;
    public RectTransform titleButton;
    public RectTransform exitButton;
    public RectTransform cursor;
    public AudioSource audioSource;
    public AudioClip moveSE;
    public AudioClip decideSE;

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

            audioSource.PlayOneShot(moveSE);

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
                cursor.anchoredPosition =
                    retryButton.anchoredPosition + new Vector2(-30f, 0f);
                break;

            case 1:
                cursor.anchoredPosition =
                    titleButton.anchoredPosition + new Vector2(-30f, 0f);
                break;

            case 2:
                cursor.anchoredPosition =
                    exitButton.anchoredPosition + new Vector2(-30f, 0f);
                break;
        }
    }

    void SelectMenu()
    {
        audioSource.PlayOneShot(decideSE);

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
