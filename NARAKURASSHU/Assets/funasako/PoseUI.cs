using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;

public class PoseUI : MonoBehaviour
{
    public PlayerControl playerControl;
    public GameObject poseScreen;
    public TextMeshProUGUI cont;
    public TextMeshProUGUI title;
    public RectTransform cursor;
    public AudioClip poseSE;
    public AudioClip cursorSE;
    AudioSource audioSorce;

    private int selected = 0;

    private void Start()
    {
        poseScreen.SetActive(false);
        audioSorce = GetComponent<AudioSource>();
    }
    private void Update()
    {
        PoseBootEnd();
        PoseCont();
    }

    void PoseBootEnd()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame && playerControl.ispose == false)
        {
            Debug.Log("ポーズ起動");
            playerControl.ispose = true;
            Time.timeScale = 0;
            poseScreen.SetActive(true);
            audioSorce.PlayOneShot(poseSE);
        }
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame && playerControl.ispose == true)
        {
            Debug.Log("ポーズ終了");
            playerControl.ispose = false;
            Time.timeScale = 1;
            poseScreen.SetActive(false);
        }
        
    }
    
    void PoseCont()
    {
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            selected++;

            audioSorce.PlayOneShot(cursorSE);
            if (selected > 1)
                selected = 0;

            UpdateMenu();
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            selected--;

            audioSorce.PlayOneShot(cursorSE);
            if (selected < 0)
                selected = 1;

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
                cursor.position =
                    cont.rectTransform.position + new Vector3(-547f, -21f, 0f);
                break;

            case 1:
                cursor.position =
                    title.rectTransform.position + new Vector3(-555f, -21f, 0f);
                break;
        }
    }

    void SelectMenu()
    {
        SceneManager.LoadScene("Title");
        switch (selected)
        {
            case 0:
                playerControl.ispose = false;
                Time.timeScale = 1;
                poseScreen.SetActive(false);
                break;

            case 1:
                SceneManager.LoadScene("Title");
                break;
        }
    }
    
}
