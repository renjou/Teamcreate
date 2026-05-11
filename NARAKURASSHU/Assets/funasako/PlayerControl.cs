using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D PlayerRigid;
    float jumpforce = 300;
    float speed = 5.0f; // 移動速度

    void Start()
    {
        Application.targetFrameRate = 60;
        this.PlayerRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = 0;

        // 右に移動
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            move = 1;
        }

        // 左に移動
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            move = -1;
        }


        transform.Translate(this.speed * move * Time.deltaTime, 0, 0);

       

    }

    void FixedUpdate()
    {
        // ジャンプ
        if (Keyboard.current.spaceKey.wasPressedThisFrame && this.PlayerRigid.linearVelocityY == 0)
        {
            this.PlayerRigid.AddForce(transform.up * this.jumpforce);
        }

        // 通常攻撃
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {

        }
    }
}
