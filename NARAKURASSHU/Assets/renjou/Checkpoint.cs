using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;

    [Header("活性化時の色（オプション）")]
    public Color activatedColor = Color.green;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーが触れた、かつ、まだ起動していない場合
        if (collision.CompareTag("Player") && !isActivated)
        {
            isActivated = true;

            // プレイヤーのスクリプトにこの場所を記憶させる
            Player playerRespawn = collision.GetComponent<Player>();
            if (playerRespawn != null)
            {
                playerRespawn.SetCheckpoint(transform.position);
            }

            // 見た目を変える（色を変更する例。アニメーションの再生でもOK）
            if (spriteRenderer != null)
            {
                spriteRenderer.color = activatedColor;
            }

            Debug.Log("チェックポイント通過！位置を保存しました: " + transform.position);
        }
    }
}