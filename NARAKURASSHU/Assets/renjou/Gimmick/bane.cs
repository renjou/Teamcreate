using UnityEngine;

public class bane : MonoBehaviour
{
    [Header("ばねの跳ね返す力")]
    public float bounceForce = 15f;

    // 2D用のトリガー判定
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 2D用のRigidbody2Dを取得する
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // 2Dでの速度リセット（Unity 2025以降は linearVelocity、古いバージョンは velocity）
                Vector2 currentVelocity = playerRb.linearVelocity;
                currentVelocity.y = 0;
                playerRb.linearVelocity = currentVelocity;

                // 2Dの上方向に向かって力を加える
                playerRb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);

                Debug.Log("2Dプレイヤーが跳ねました！");
            }
        }
    }
}