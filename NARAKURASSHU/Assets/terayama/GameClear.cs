using UnityEngine;

public class GameClear : MonoBehaviour
{
    public GameObject clearUI;

    public void ShowClear()
    {
        clearUI.SetActive(true);
        Time.timeScale = 0f;
    }
}