using UnityEngine;

public class yokobane : MonoBehaviour
{
    [Header("弾き飛ばす力")]
    public float bounceForce = 15.0f;

    [Header("バネの向き（Trueなら右、Falseなら左）")]
    public bool pushRight = true;

    [Header("アニメーション（オプション）")]
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 接触したオブジェクトがプレイヤーかチェック
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // 1. 弾き飛ばす方向を決める
                Vector2 bounceDirection = pushRight ? Vector2.right : Vector2.left;

                // 2. プレイヤーの現在の横方向の速度をリセット（逆方向への慣性を消すため）
                playerRb.linearVelocity = new Vector2(0f, playerRb.linearVelocity.y);

                // 3. 横方向の力を加える（一瞬で強い力を加えるImpulseモード）
                playerRb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);

                // 4. バネのアニメーションを再生（Animatorがある場合のみ）
                if (animator != null)
                {
                    animator.SetTrigger("Bounce");
                }

                Debug.Log($"バネ発動！プレイヤーを {(pushRight ? "右" : "左")} 方向に弾き飛ばしました。");
            }
        }
    }
}