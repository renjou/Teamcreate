using UnityEngine;
using UnityEngine.InputSystem;

public class Boss : MonoBehaviour
{
    enum State
    {
        Move,
        Dash
    }

    State state;

    public int hp = 40;
    public Transform player;
    public float dashCooldown = 3f;
    private float CooldownTimer;


    // ボスの移動速度
    public float moveSpeed = 2f;
    public float dashSpeed = 5f;
    private float dashTime = 0.5f;
    private float dashTimer;
    private float dashDir;

    public float detectRange = 2.5f;

    private Vector2 moveDir = Vector2.right;

    void Start()
    {
        state = State.Move;
        dashTimer = dashTime;

        GameObject p = GameObject.FindWithTag("Player");

        if (p != null)
        {
            player = p.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CooldownTimer -= Time.deltaTime;

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            TakeDamage(4);
        }
        Debug.Log(state);
        switch (state)
        {
            case State.Move:
                Move();

                // プレイヤー発見
                if (player != null)
                {
                    float distance = Vector2.Distance(transform.position, player.position);

                    float dirToPlayer = Mathf.Sign(player.position.x - transform.position.x);

                    // 向いている方向にプレイヤーがいるか
                    bool facingPlayer = dirToPlayer == moveDir.x;

                    if (CooldownTimer <= 0f && distance <= detectRange && facingPlayer)
                    {
                        StartDash(dirToPlayer);
                    }
                }
                break;

            case State.Dash:

                Dash();

                dashTimer -= Time.deltaTime;

                if (dashTimer <= 0)
                {
                    EndDash();
                }

                // プレイヤーが一定距離以上離れたら移動状態に戻る
                if (player != null)
                {
                    float distance = Vector2.Distance(transform.position, player.position);
                }
                break;

        }
    }

    // ボス移動
    void Move()
    {
        if (player != null)
        {
            float distanceX =
                player.position.x - transform.position.x;

            // プレイヤーが右にいる
            if (distanceX > 0.3f)
            {
                moveDir = Vector2.right;

                transform.localScale =
                    new Vector3(1, 1, 1);
            }

            // プレイヤーが左にいる
            else if (distanceX < -0.3f)
            {
                moveDir = Vector2.left;

                transform.localScale =
                    new Vector3(-1, 1, 1);
            }
        }

        transform.Translate(
            moveDir * moveSpeed * Time.deltaTime);
    }
    void Dash()
    {
        transform.Translate(Vector2.right * dashDir * dashSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log("Boss HP: " + hp);

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss dead");
        // ボスが死んだときの処理
        Destroy(gameObject);
    }
    void StartDash(float dir)
    {
        state = State.Dash;
        dashDir = dir;
        dashTimer = dashTime;
    }

    void EndDash()
    {
        state = State.Move;
        CooldownTimer = dashCooldown;
    }

}
