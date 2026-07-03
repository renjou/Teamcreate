using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

public class PlayerControl : MonoBehaviour
{
    //public NormalAttack normalAttack;
    Rigidbody2D playerRigid;
    Collider2D playerCollider;
    SpriteRenderer playerSp;
    SpecialUI specialUI;
    public float jumpforce = 1000;
    public float speed = 10.0f; // 移動速度
    public float speGauge = 0;
    public float playerDirection = 1; // 自機の向き　　1で右向き
    public int playerHP = 5;
    public float knockBackPower = 10f;
    bool isAttacking = false;
    bool isAttacking1 = false;
    bool isAttacking2 = false;
    bool isKnockBack = false;
    bool isDameging = false;
    public bool gameover = false;
    public bool ispose = false;
    public Collider2D normalAttack;
    public SpriteRenderer circleSp;
    public Transform sprite;
    public Transform Idle_0;
    public Transform circle;
    public HPUI hpUI;
    public SpecialGaugeUI spUI;
    public PoseUI poseUI;
    // public GameObject attack1Prefab;
    public GameObject attack2Prefab;
    public GameObject specialPrefab;
    Animator animator;
    public AudioClip jumpSE;
    public AudioClip landingSE;
    public AudioClip knokBackSE;
    public AudioClip dethSE;
    public AudioClip attack1SE;
    public AudioClip attack2SE;
    public AudioClip specialSE;
    AudioSource audioSource;
    RespawnManager reborn;

    void Start()
    {
        Debug.Log("Player Start");
        Debug.Log(poseUI);
        hpUI.UpdateHP(playerHP);
        Application.targetFrameRate = 60;
        //normalAttack = GetComponent<NormalAttack>();
        this.playerRigid = GetComponent<Rigidbody2D>();
        this.playerCollider = GetComponent<Collider2D>();
        reborn = FindFirstObjectByType<RespawnManager>();
        animator = GetComponentInChildren<Animator>();
        reborn.Register(transform);
    //  specialUI = FindFirstObjectByType<SpecialUI>();
        audioSource = GetComponent<AudioSource>();
        playerSp = Idle_0.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // エネミーに衝突したらダメージ
    {
        if (isDameging) return;
        if (normalAttack.enabled && isAttacking1) return; 
        if (collision.CompareTag("enemy") ||
            collision.CompareTag("boss"))
        {
            PlayerDamage();
            KnockBack(collision.transform.position);
        }
    }
    void Update()
    {
        //Debug.Log("Player Update");
        if (ispose) return;
        PlayerAnime();
     // Debug.Log(poseUI.ispose);
        if (playerHP == 0 && !gameover) // 死亡
        {
            Debug.Log("gameover");
            gameover = true;
            audioSource.PlayOneShot(dethSE);
            isDameging = false;
            isKnockBack = false;
            playerRigid.linearVelocity = Vector3.zero;
            Invoke(nameof(RespawanCall), 2f);
            StartCoroutine(RespawnFlash());

        }
        if (gameover) return;

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            PlayerDamage();
        }
        if (Keyboard.current.aKey.wasPressedThisFrame) speGauge++;
        if (isKnockBack) return; // ノックバック中は操作無効
        if (isAttacking) return; // 攻撃中は別の攻撃は不可
        move();
        jump();
        if (Keyboard.current.jKey.wasPressedThisFrame) attack1();
        else if (Keyboard.current.kKey.wasPressedThisFrame) attack2();
        else if (Keyboard.current.shiftKey.wasPressedThisFrame && speGauge >= 5)
        {
            SpecialAttack();
        }

    }


    void jump() // ジャンプ
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && this.playerRigid.linearVelocityY == 0)
        {
            this.playerRigid.AddForce(transform.up * this.jumpforce);
            audioSource.PlayOneShot(jumpSE);
            Debug.Log("ジャンプ");
        }
    }
    void move() // 左右移動
    {
        float move = 0;
        // 右に移動
        if (Keyboard.current.dKey.isPressed)
        {
            move = 1;
            playerDirection = 1;
            sprite.localScale = new Vector3(1, 1, 1);
        }
        // 左に移動
        if (Keyboard.current.aKey.isPressed)
        {
            move = -1;
            playerDirection = -1;
            sprite.localScale = new Vector3(-1, 1, 1);
        }
        //transform.Translate(this.speed * move * Time.deltaTime, 0, 0);
        playerRigid.linearVelocity = new Vector2(move * speed, playerRigid.linearVelocity.y);
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
        audioSource.PlayOneShot(attack1SE);
        
        // 通常攻撃
        if (playerDirection == 1)
        {
            playerRigid.linearVelocityX = 0;
            Debug.Log("攻撃1右");
            Invoke(nameof(Delay), 0.3f);
        }
        else
        {
            playerRigid.linearVelocityX = 0;
            Debug.Log("攻撃1左");
            //normalAttack.AttackOn();
            Invoke(nameof(Delay), 0.3f);
        }
        Invoke(nameof(endAttack), 0.6f);
    }


    void attack2() // 強攻撃
    {
        isAttacking = true;
        isAttacking2 = true;
        audioSource.PlayOneShot(attack2SE);
        playerRigid.linearVelocityX = 0;
    //  playerRigid.linearVelocityY = 0;
        Debug.Log("攻撃2右");
        StartCoroutine(attack2Instantlate());
        Invoke(nameof(endAttack), 0.6f);
    }

    IEnumerator attack2Instantlate()
    {
        yield return new WaitForSeconds(0.2f);
        circleSp.enabled = true;
        yield return new WaitForSeconds(0.3f);
        GameObject attack = Instantiate(attack2Prefab,
            circle.position,
            Quaternion.identity);
        attack.GetComponent<AttackObject1>().direction = playerDirection;
        Vector3 scale = attack.transform.localScale;
        if (playerDirection == -1)
        {
            scale.x *= -1;
        }
        attack.transform.localScale = scale;
        yield return new WaitForSeconds(0.1f);
        circleSp.enabled = false;
    }

    IEnumerator specialAttackInstantlate()
    {
        yield return new WaitForSeconds(0.2f);
        circleSp.enabled = true;
        yield return new WaitForSeconds(0.3f);
        GameObject attack = Instantiate(specialPrefab,
            circle.position,
            Quaternion.identity);
        attack.GetComponent<AttackObject1>().direction = playerDirection;
        yield return new WaitForSeconds(0.1f);
        circleSp.enabled = false;
    }

    void SpecialAttack()
    {
        circleSp.enabled = true;
        audioSource.PlayOneShot(specialSE);
        GameObject attack = Instantiate(specialPrefab,
            circle.position,
            Quaternion.identity);
        attack.GetComponent<SpecialAttack>().direction = playerDirection;
        Vector3 scale = attack.transform.localScale;
        if (playerDirection == -1)
        {
            scale.x *= -1;
        }
        attack.transform.localScale = scale;
        speGauge = 0;
        //  specialUI.ressetSpeGauge();
        spUI.SpeGaugeUpdate(speGauge);
        circleSp.enabled = false;
    }


    void endAttack()
    {
        isAttacking = false;
        isAttacking1 = false;
        isAttacking2 = false;
        normalAttack.enabled = false;
        //normalAttack.AttackOff();
    }

    public void PlayerDamage()　// ダメージ処理
    {
        isDameging = true;
        Debug.Log("攻撃を受けた");
        speGauge++;
        playerHP--;
        if (playerHP < 0)
        {
            playerHP = 0;
        }
        //  specialUI.increaseSpeGauge();
        spUI.SpeGaugeUpdate(speGauge);
        hpUI.UpdateHP(playerHP);
    }

    public void KnockBack(Vector3 enemyPos) // 被弾時ノックバック
    {
        Debug.Log("ノックバック発生");
        isKnockBack = true;
        audioSource.PlayOneShot(knokBackSE);
        StartCoroutine(DamgagedFlash());
        Vector2 direction = (transform.position - enemyPos).normalized;
        direction.x *= 0.5f;
        direction.y = 0.5f;
        playerRigid.linearVelocity = Vector2.zero;
        playerRigid.AddForce(direction * knockBackPower, ForceMode2D.Impulse);
        Invoke(nameof(EndKnockBack), 0.5f);
        Invoke(nameof(EndInvincible), 2f);
    }

    IEnumerator DamgagedFlash()
    {
        playerSp.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        playerSp.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        playerSp.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        playerSp.color = Color.white;
    }

    void EndKnockBack()
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

    IEnumerator RespawnFlash()
    {
        yield return new WaitForSeconds(2f);
        playerSp.enabled = false;
        yield return new WaitForSeconds(0.1f);
        playerSp.enabled = true;
        yield return new WaitForSeconds(0.1f);
        playerSp.enabled = false;
        yield return new WaitForSeconds(0.1f);
        playerSp.enabled = true;
        yield return new WaitForSeconds(0.1f);
        playerSp.enabled = false;
        yield return new WaitForSeconds(0.1f);
        playerSp.enabled = true;
        yield return new WaitForSeconds(0.1f);
        playerSp.enabled = false;
        yield return new WaitForSeconds(0.1f);
        playerSp.enabled = true;
    }

    void EndInvincible()
    {
        isDameging = false;
    }

    public void SpeGaugeIncrease()
    {
        Debug.Log("ゲージ増加");
        speGauge++;
        //  specialUI.increaseSpeGauge();
        spUI.SpeGaugeUpdate(speGauge);
    }

    void Delay()
    {
        normalAttack.enabled = true;
    }

    void PlayerAnime()
    {
        if (playerRigid.linearVelocityY > 0.1f) // ↓ジャンプor落下アニメ切り替え
        {
            animator.SetBool("isJump", true);
        }
        else if (playerRigid.linearVelocityY < -0.1f)
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
