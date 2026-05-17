using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D PlayerRigid;
    float jumpforce = 300;
    float speed = 5.0f; // 移動速度
    public float playerDirection = 1; // 自機の向き　　1で右向き
    bool isAttacking = false;
    public GameObject attack1Prefab;
    public GameObject attack2Prefab;

    void Start()
    {
        Application.targetFrameRate = 60;
        this.PlayerRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move();
        jump();
        if (isAttacking) return;
        if (Keyboard.current.rKey.wasPressedThisFrame) attack1();
        else if (Keyboard.current.eKey.wasPressedThisFrame) attack2();
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
        isAttacking = true;
        // 通常攻撃
        if (playerDirection == 1)
        {
            Debug.Log("攻撃1右");
            GameObject attack = Instantiate(attack1Prefab,
            transform.position + Vector3.right,
            Quaternion.identity);
            attack.GetComponent<AttackObject1>().direction = playerDirection;
        }
        else 
        {
            Debug.Log("攻撃1左");
            GameObject attack = Instantiate(attack1Prefab,
            transform.position + Vector3.left,
            Quaternion.identity);
            attack.GetComponent <AttackObject1>().direction = playerDirection;
        }
        Invoke(nameof(endAttack), 0.6f);
    }

    void attack2()
    {
        isAttacking = true;
        // 強攻撃
        if (playerDirection == 1)
        {
             Debug.Log("攻撃2右");
             GameObject attack = Instantiate(attack2Prefab,
             transform.position + Vector3.right,
             Quaternion.identity);
             attack.GetComponent<AttackObject1>().direction = playerDirection;
        }
        else 
        {
             GameObject attack = Instantiate(attack2Prefab,
             transform.position + Vector3.left,
             Quaternion.identity);
             Debug.Log("攻撃2左");
             attack.GetComponent<AttackObject1>().direction = playerDirection;
        }
        Invoke(nameof(endAttack), 0.6f);
        
    }

    void endAttack()
    {
        isAttacking = false;
    }
    
}
