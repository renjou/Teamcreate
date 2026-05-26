using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public BoxCollider2D NormalAttack;
    Rigidbody2D PlayerRigid;
    float jumpforce = 1000;
    float speed = 5.0f; // 移動速度
    public float playerDirection = 1; // 自機の向き　　1で右向き
    public int playerHP = 5;
    public float knockBackPower = 10f;
    bool isAttacking = false;
    bool isAttacking1 = false;
    bool isAttacking2 = false;
    bool isKnockBack = false;
    bool isDameging = false;
    public bool gameover = false;
    public Transform sprite;
    public Transform circle;
    public HPUI hpUI;
    // public GameObject attack1Prefab;
    public GameObject attack2Prefab;
    Animator animator;
    RespawnManager reborn;

    void Start()
    {
        Debug.Log(NormalAttack);
        hpUI.UpdateHP(playerHP);
        Application.targetFrameRate = 60;
        this.PlayerRigid = GetComponent<Rigidbody2D>();
        reborn = FindFirstObjectByType<RespawnManager>();
        animator = GetComponentInChildren<Animator>();
        reborn.Register(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision) // エネミーに衝突したらダメージ
    {
        if (isDameging) return;
        if (collision.CompareTag("enemy"))
        {
            PlayerDamage();
            knockBack(collision.transform.position);
        }
    }
    void Update()
    {
        playerAnime();
        if (playerHP == 0 && !gameover) // 死亡
        {
            Debug.Log("gameover");
            gameover = true;
            isDameging = false;
            isKnockBack = false;
            PlayerRigid.linearVelocity = Vector3.zero;
            Invoke(nameof(RespawanCall), 2f);
        }
        if (gameover) return;

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            PlayerDamage();
        }
        if (isKnockBack) return; // ノックバック中は操作無効
        if (isAttacking) return; // 攻撃中は別の攻撃は不可
        move();
        jump();
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
            sprite.localScale = new Vector3(1, 1, 1);
        }
        // 左に移動
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            move = -1;
            playerDirection = -1;
            sprite.localScale = new Vector3(-1, 1, 1);
        }
        //transform.Translate(this.speed * move * Time.deltaTime, 0, 0);
        PlayerRigid.linearVelocity = new Vector2(move * speed, PlayerRigid.linearVelocity.y);
        if (move == 0)
        {
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isRun", true);
        }
    }

    void attack1()
    {
        isAttacking = true;
        isAttacking1 = true;
        // 通常攻撃
        if (playerDirection == 1)
        {
            PlayerRigid.linearVelocityX = 0;
            Debug.Log("攻撃1右");
            NormalAttack.enabled = true;
        }
        else 
        {
            PlayerRigid.linearVelocityX = 0;
            Debug.Log("攻撃1左");
            NormalAttack.enabled = true;
        }
        Invoke(nameof(endAttack), 0.6f);
    }

    void attack2()
    {
        isAttacking = true;
        isAttacking2 = true;
        // 強攻撃
        if (playerDirection == 1)
        {
             PlayerRigid.linearVelocityX = 0;
             Debug.Log("攻撃2右");
             GameObject attack = Instantiate(attack2Prefab,
             circle.position,
             Quaternion.identity);
             attack.GetComponent<AttackObject1>().direction = playerDirection;
        }
        else 
        {
            PlayerRigid.linearVelocityX = 0;
            GameObject attack = Instantiate(attack2Prefab,
             circle.position,
             Quaternion.identity);
             Debug.Log("攻撃2左");
             attack.GetComponent<AttackObject1>().direction = playerDirection;
        }
        Invoke(nameof(endAttack), 0.6f);
        
    }

    void endAttack()
    {
        isAttacking = false;
        isAttacking1 = false;
        isAttacking2 = false;
        NormalAttack.enabled = false;
    }

    public void PlayerDamage()　// ダメージ処理
    {
        isDameging = true;
        playerHP--;
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
        Invoke(nameof(endInvincible), 1f);
    }

    void endKnockBack()
    {
        isKnockBack = false;
    }

    void RespawanCall()
    {
        playerHP = 5;
        reborn.RespawnALL();
        hpUI.UpdateHP(playerHP);
        gameover = false;
    }

    void endInvincible()
    {
        isDameging = false;
    }

    void playerAnime()
    {
        if (PlayerRigid.linearVelocityY > 0.1f) // ↓ジャンプor落下アニメ切り替え
        {
            animator.SetBool("isJump", true);
        }
        else if (PlayerRigid.linearVelocityY < -0.1f)
        {
            animator.SetBool("isFall", true);
        }
        else
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isFall", false);
        }

        if (isKnockBack)
        {
            animator.SetBool("isHit", true);
        }
        else
        {
            animator.SetBool("isHit", false);
        }

        if (gameover)
        {
            animator.SetBool("isDead", true);
        }
        else
        {
            animator.SetBool("isDead", false);
        }

        if (isAttacking1)
        {
            animator.SetBool("isattacking1", true);
        }
        else
        {
            animator.SetBool("isattacking1", false);
        }

        if (isAttacking2)
        {
            animator.SetBool("isattacking2", true);
        }
        else
        {
            animator.SetBool("isattacking2", false);

        }
    }
}
