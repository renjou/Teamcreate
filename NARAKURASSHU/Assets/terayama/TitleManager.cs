using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject controlsPanel;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameStage");
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}