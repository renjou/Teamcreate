using UnityEngine;
using UnityEngine.InputSystem;

public class Boss : MonoBehaviour
{
    enum State
    {
        Idle,
        Charge,
        Dash,
        Attack,
        Stun
    }

    State state;

    public int BossHp = 40;
    public Transform player;
    public float dashCooldown = 3f;
    public float CooldownTimer;
    private float currentDashSpeed;
    private SpriteRenderer sr;
    // ボスの移動速度
    public float dashSpeed = 6f;
    private float dashTime = 1f;
    private float dashTimer;
    private float dashDir;
    // ボスの予備動作時間
    private float chargeTime = 1f;
    private float chargeTimer;
    // アニメーション
    private Animator anim;
    // 元のサイズ
    private Vector3 baseScale;
    // 死亡フラグ
    bool isDead = false;
    // 攻撃範囲
    public float attackRange = 2f;
    // 攻撃時間
    private float attackTime = 0.7f;
    private float attackTimer;
    public float attackCooldown = 2f;
    private float attackCooldownTimer;
    // 無敵フラグ
    private bool isInvincible = false;
    // 無敵時間
    public float invincibleTime = 0.2f;
    // 攻撃エリア
    [SerializeField] private GameObject AttackArea;
    // スタン時間
    public float stunTime = 2f;
    // スタンタイマー
    private float stunTimer;

    // プレイヤー発見距離
    public float detectRange = 5f;

    // 突進開始位置
    private Vector3 chargePos;

    private Rigidbody2D rd;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();

        AttackArea.SetActive(false);

        // 初期状態
        state = State.Idle;

        // コンポーネント取得
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // 初期アニメ
        anim.Play("Boss_Idle");

        // タイマー初期化
        CooldownTimer = 0f;

        dashTimer = dashTime;

        currentDashSpeed = dashSpeed;

        // 元のサイズ保存
        baseScale = transform.localScale;

        // プレイヤーを探す
        GameObject p = GameObject.FindWithTag("Player");

        if (p != null)
        {
            player = p.transform;
            //    Debug.Log("found");
        }
        else
        {
            //   Debug.Log("Not Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        CooldownTimer -= Time.deltaTime;
        attackCooldownTimer -= Time.deltaTime;
        /*
        // テスト用ダメージ
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            TakeDamage(10);
        }
        */
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

                    //            Debug.Log(distance);
                    //          Debug.Log(CooldownTimer);

                    // 近距離なら攻撃
                    if (attackCooldownTimer <= 0 && distance <= attackRange)
                    {
                        StartAttack();
                    }
                    // 中距離なら突進
                    else if (CooldownTimer <= 0 && distance > attackRange && distance <= detectRange)
                    {
                        StartCharge(dirToPlayer);
                    }
                }

                break;

            // 予備動作
            case State.Charge:

                chargeTimer -= Time.deltaTime;

                // 小刻みに震える
                transform.position = chargePos + new Vector3( Mathf.Sin(Time.time * 50f) * 0.03f, 0, 0);

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

            // 攻撃状態
            case State.Attack:

                attackTimer -= Time.deltaTime;

                if (attackTimer <= 0)
                {
                    AttackArea.SetActive(false);
                    state = State.Idle;
                    anim.Play("Boss_Idle");
                }

                break;

            // スタン状態
            case State.Stun:

                sr.color = Color.Lerp(Color.white,Color.yellow,Mathf.PingPong(Time.time * 8f, 1f));

                stunTimer -= Time.deltaTime;

                if (stunTimer <= 0)
                {
                    sr.color = Color.white;

                    state = State.Idle;
                    anim.Play("Boss_Idle");
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
            transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
        }
        // プレイヤーが左
        else if (distanceX < -0.3f)
        {
            transform.localScale = new Vector3(baseScale.x, baseScale.y, baseScale.z);

        }
    }

    // ダッシュ開始
    void StartDash(float dir)
    {
        //  Debug.Log("Dash Start");

        transform.position = chargePos;

        state = State.Dash;

        dashDir = dir;

        currentDashSpeed = dashSpeed;

        //   Debug.Log(dashDir);

        dashTimer = dashTime;

        // 色戻す
        sr.color = Color.white;

        anim.SetTrigger("Dash");
    }

    // ダッシュ
    void Dash()
    {
        //   Debug.Log("Dashing");
        rd.linearVelocity = new Vector2( dashDir * currentDashSpeed, rd.linearVelocity.y);

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
        rd.linearVelocity = Vector2.zero;

        state = State.Idle;

        CooldownTimer = dashCooldown;
        
        // 1秒は通常攻撃禁止
        attackCooldownTimer = 1f; 

        anim.Play("Boss_Idle");
    }

    // ダメージ
    public void TakeDamage(int damage)
    {
        // 死亡してたら無効
        if (isDead) return;

        // 無敵中は無効
        if (isInvincible) return;

        // ダメージ受けたら少しの間無敵
        isInvincible = true;

        Invoke(nameof(EndInvincible), invincibleTime);

        // スタン中以外だけ停止
        if (state != State.Stun)
        {
            state = State.Idle;

            currentDashSpeed = 0f;
            dashTimer = 0f;
            chargeTimer = 0f;

            rd.linearVelocity = Vector2.zero;
        }

        // 色を戻す
        sr.color = Color.white;

        BossHp -= damage;

        //    Debug.Log("Boss HP: " + BossHp);
        anim.Play("Boss_Damage");
        if (BossHp <= 0)
        {
            Die();
        }
    }

    // 死亡
    void Die()
    {
        isDead = true;

        //    Debug.Log("Boss dead");

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

        // 現在位置保存
        chargePos = transform.position;

        // 赤くする
        sr.color = Color.red;
    }

    //近接
    void StartAttack()
    {
        state = State.Attack;

        sr.color = Color.white;

        // ダッシュ停止
        currentDashSpeed = 0;
        dashTimer = 0;

        attackTimer = attackTime;

        attackCooldownTimer = attackCooldown;

        AttackArea.SetActive(true);

        anim.Play("Boss_Attack");

        //    Debug.Log("Attack");
    }

    // 無敵終了
    void EndInvincible()
    {
        isInvincible = false;
    }

    // プレイヤーにぶつかった
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("ぶつかった: " + collision.gameObject.name);

        // ダッシュ中のみ判定
        if (state != State.Dash)
            return;

        // 壁に当たった
        if (collision.gameObject.CompareTag("Wall"))
        {
         //   Debug.Log("Wall Hit");

            StartStun();

            return;
        }

        // プレイヤーに当たった
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>()?.PlayerDamage();

       //     Debug.Log("Dash Hit");

            EndDash();
        }
    }

    // スタン開始
    void StartStun()
    {
        state = State.Stun;

        stunTimer = stunTime;

        currentDashSpeed = 0;

        rd.linearVelocity = Vector2.zero;

        anim.Play("Boss_Damage");
    }
}
