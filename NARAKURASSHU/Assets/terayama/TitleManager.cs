using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject controlsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
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