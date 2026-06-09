using UnityEngine;
using UnityEngine.SceneManagement;
public class GameClearUI : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Title()
    {
        Time.timeScale = 1f; SceneManager.LoadScene("Title");
    }
    public void Exit()
    {
        Time.timeScale = 1f; Application.Quit();
        Debug.Log("ゲーム終了");
    }
}