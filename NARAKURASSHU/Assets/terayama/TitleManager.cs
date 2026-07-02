using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    public GameObject controlsPanel;
    public AudioSource audioSource;
    public Transform cursor;

    public bool isControlsOpen = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
       
        controlsPanel.SetActive(false);
    }

    void Update()
    {
        if (isControlsOpen)
        {
            if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                ShowControls();
                cursor.gameObject.SetActive(true);
            }
        }
    }

    public void StartGame()
    {
        if (isControlsOpen) return;

        SceneManager.LoadScene("GameStage");
    }

    public void ShowControls()
    {
        cursor.gameObject.SetActive(false);
        isControlsOpen = !isControlsOpen;
        controlsPanel.SetActive(isControlsOpen);
    }

    public void ExitGame()
    {
        if (isControlsOpen) return;

        Application.Quit();
    }
}