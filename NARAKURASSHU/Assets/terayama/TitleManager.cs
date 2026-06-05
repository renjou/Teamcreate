using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject controlsText;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowControls()
    {
        controlsText.SetActive(
            !controlsText.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}