using UnityEngine;
using UnityEngine.InputSystem;

public class Boss : MonoBehaviour
{
    enum State
    {
        Idle,
        Charge,
        Dash
    }

    State state;

    public int hp = 40;
    public Transform player;
    public float dashCooldown = 3f;
    public float CooldownTimer;
    private float currentDashSpeed;
    private SpriteRenderer sr;
    // ボスの移動速度
    public float dashSpeed = 6f;
    private float dashTime = 01f;
    private float dashTimer;
    private float dashDir;
    private float chargeTime = 1f;
    private float chargeTimer;
    private Animator anim;

    // プレイヤー発見距離
    public float detectRange = 10f;

    void Start()
    {
        state = State.Idle;
       
        sr = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

        anim.Play("Boss_Idle");

        CooldownTimer = 0f;

        dashTimer = dashTime;

        currentDashSpeed = dashSpeed;



        GameObject p = GameObject.FindWithTag("Player");

        if (p != null)
        {
            player = p.transform;
            Debug.Log("found");
        }
        else
        {
            Debug.Log("Not Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CooldownTimer -= Time.deltaTime;

        // テスト用ダメージ
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            TakeDamage(10);
        }

        switch (state)
        {
            // 待機状態
            case State.Idle:

                LookPlayer();

                // プレイヤー発見
                if (player != null)
                {
                    float distance = Vector2.Distance(transform.position, player.position);

                    float distanceX = player.position.x - transform.position.x;

                    float dirToPlayer;

                    if (distanceX >= 0)
                    {
                        dirToPlayer = 1;
                    }
                    else
                    {
                        dirToPlayer = -1;
                    }
                    //   float dirToPlayer = Mathf.Sign(player.position.x - transform.position.x);

                    Debug.Log(distance);
                    Debug.Log(detectRange);
                    Debug.Log(CooldownTimer);

                    // 発見したら突進
                    if (CooldownTimer <= 0 && distance <= detectRange)
                    {
                        StartCharge(dirToPlayer);
                    }
                }

                break;

            // 予備動作
            case State.Charge:

                chargeTimer -= Time.deltaTime;

                // 点滅
                sr.color = Color.Lerp(
                    Color.white,
                    Color.red,
                    Mathf.PingPong(
                        Time.time * 8f,
                        1f));

                if (chargeTimer <= 0)
                {
                    StartDash(dashDir);
                }

                break;

            // ダッシュ状態
            case State.Dash:

                Dash();

                dashTimer -= Time.deltaTime;

                if (dashTimer <= 0)
                {
                    EndDash();
                }

                break;
        }
    }

    // プレイヤーの方向を見る
    void LookPlayer()
    {
        if (player == null) return;

        float distanceX = player.position.x - transform.position.x;

        // プレイヤーが右
        if (distanceX > 0.3f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // プレイヤーが左
        else if (distanceX < -0.3f)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
    }

    // ダッシュ開始
    void StartDash(float dir)
    {
        Debug.Log("Dash Start");

        state = State.Dash;

        dashDir = dir;

        currentDashSpeed = dashSpeed;

        Debug.Log(dashDir);

        dashTimer = dashTime;

        // 色戻す
        sr.color = Color.white;

        anim.SetTrigger("Dash");
    }

    // ダッシュ
    void Dash()
    {
        Debug.Log("Dashing");
        transform.Translate(Vector2.right * dashDir * currentDashSpeed * Time.deltaTime);

        // 徐々に減速
        currentDashSpeed -= 5f * Time.deltaTime;

        // 0以下防止
        if (currentDashSpeed < 0)
        {
            currentDashSpeed = 0;
        }
    }

    // ダッシュ終了
    void EndDash()
    {
        state = State.Idle;

        CooldownTimer = dashCooldown;

        anim.Play("Boss_Idle");
    }

    // ダメージ
    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log("Boss HP: " + hp);
        anim.Play("Boss_Damage");
        if (hp <= 0)
        {
            Die();
        }
    }

    // 死亡
    void Die()
    {
        Debug.Log("Boss dead");
        
        anim.Play("Boss_Die");

        // 1.5秒後に削除
        Destroy(gameObject, 1.5f);
    }

    //予備動作
    void StartCharge(float dir)
    {
        state = State.Charge;

        dashDir = dir;

        chargeTimer = chargeTime;

        // 赤くする
        sr.color = Color.red;
    }
}
