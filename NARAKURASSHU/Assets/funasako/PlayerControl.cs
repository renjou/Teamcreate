using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D PlayerRigid;
    float jumpforce = 300;
    float speed = 5.0f; // 移動速度
    float playerDirection = 1; // 自機の向き　　1で右向き
    public GameObject attack1Prefab;

    void Start()
    {
        Application.targetFrameRate = 60;
        this.PlayerRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move();
        jump();
        attack1();
        attack2();
    }



    void jump() // ジャンプ
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && this.PlayerRigid.linearVelocityY == 0)
        {
            this.PlayerRigid.AddForce(transform.up * this.jumpforce);
        }
    }
    void move() // 左右移動
    {
        float move = 0;
        // 右に移動
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            move = 1;
            playerDirection = 1;
        }
        // 左に移動
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            move = -1;
            playerDirection = -1;
        }
        transform.Translate(this.speed * move * Time.deltaTime, 0, 0);
    }

    void attack1()
    {
        // 通常攻撃
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (playerDirection == 1)
            {
                Debug.Log("攻撃1右");
                Instantiate(attack1Prefab,
                transform.position + Vector3.right,
                Quaternion.identity);
            }
            else Debug.Log("攻撃1左");
        }
    }

    void attack2()
    {
        // 強攻撃
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (playerDirection == 1) Debug.Log("攻撃2右");
            else Debug.Log("攻撃2左");
        }
    }
}
