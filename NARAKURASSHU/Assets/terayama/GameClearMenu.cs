using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameClearMenu : MonoBehaviour
{
    public RectTransform retryButton;
    public RectTransform titleButton;
    public RectTransform exitButton;
    public RectTransform cursor;

    private int selected = 0;

    void OnEnable()
    {
        selected = 0;
        StartCoroutine(UpdateCursor());
    }

    IEnumerator UpdateCursor()
    {
        yield return null;   // 1フレーム待つ
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
                cursor.position = retryButton.position + new Vector3(-275f, 0f, 0f);
                break;

            case 1:
                cursor.position = titleButton.position + new Vector3(-275f, 0f, 0f);
                break;

            case 2:
                cursor.position = exitButton.position + new Vector3(-275f, 0f, 0f);
                break;
        }
    }

    void SelectMenu()
    {
        switch (selected)
        {
            case 0:
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
