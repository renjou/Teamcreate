using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI exitText;
    public Transform cursor;

    private int selected = 0;

    public TitleManager titleManager;

    void Start()
    {
        UpdateMenu();
    }

    void Update()
    {
        if (titleManager.isControlsOpen)
        {
            return;
        }

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
                cursor.position = startText.transform.position + new Vector3(-245f, 0f, 0f);
                break;

            case 1:
                cursor.position = controlsText.transform.position + new Vector3(-245f, 0f, 0f);
                break;

            case 2:
                cursor.position = exitText.transform.position + new Vector3(-245f, 0f, 0f);
                break;
        }
        Debug.Log("カーソル位置 selected = " + selected);
    }

    void SelectMenu()
    {
        Debug.Log("決定 selected = " + selected);
        switch (selected)
        {
            case 0:
                SceneManager.LoadScene("GameStage");
                break;

            case 1:
                titleManager.ShowControls();
                break;

            case 2:
                Application.Quit();
                break;
        }
    }
}