using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PoseUI : MonoBehaviour
{
    public PlayerControl playerControl;
    bool poseCoolTime = false;

    void Update()
    {
        PoseBoot();
        PoseEnd();
    }
    void PoseBoot()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame && playerControl.pose == false)
        {
            playerControl.pose = true;
            Debug.Log("ポーズ中");
        }
    }

    void PoseEnd()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame && playerControl.pose == true)
        {
            playerControl.pose = false;
            Debug.Log("ポーズ終了");
        }
    }


}
