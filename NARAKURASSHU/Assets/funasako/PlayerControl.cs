using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D PlayerRigid;
    float jumpforce = 300;
    float speed = 5.0f; // 移動速度
    public float playerDirection = 1; // 自機の向き　　1で右向き
    public int playerHP = 5;
    public float knockBackPower = 10f;
    bool isAttacking = false;
    bool isKnockBack = false;
    public bool gameover = false;
    public HPUI hpUI;
    public GameObject attack1Prefab;
    public GameObject attack2Prefab;

    void Start()
    {
        hpUI.UpdateHP(playerHP);
        Application.targetFrameRate = 60;
        this.PlayerRigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // エネミーに衝突したらダメージ
    {
        if (collision.CompareTag("enemy"))
        {
            PlayerDamage();
            knockBack(collision.transform.position);
        }
    }
    void Update()
    {
        if (playerHP == 0)
        {
            Debug.Log("gameover");
            gameover = true;
            PlayerRigid.linearVelocity = Vector3.zero;
        }
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            PlayerDamage();
        }
        if (isKnockBack) return;
        if (gameover) return;
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
        //transform.Translate(this.speed * move * Time.deltaTime, 0, 0);
        PlayerRigid.linearVelocity = new Vector2(move * speed, PlayerRigid.linearVelocity.y);
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

    public void PlayerDamage()　// ダメージ処理
    {
        playerHP -= 1;
        if (playerHP < 0)
        {
            playerHP = 0;
        }

        hpUI.UpdateHP(playerHP);
    }

    void knockBack(Vector3 enemyPos) // 被弾時ノックバック
    {
        Debug.Log("痛み");
        isKnockBack = true;
        Vector2 direction = (transform.position - enemyPos).normalized;
        direction.x *= 0.5f;
        direction.y = 0.5f;
        PlayerRigid.linearVelocity = Vector2.zero;
        PlayerRigid.AddForce(direction * knockBackPower, ForceMode2D.Impulse);
        Invoke(nameof(endKnockBack), 0.3f);
    }

    void endKnockBack()
    {
        isKnockBack = false;
    }

}
