using UnityEngine;

public class Checkpoint1 : MonoBehaviour
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
            PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                isActivated = true; // 位置保存が確実にできる場合のみ起動フラグを立てる

                // PlayerRespawnクラスのSetCheckpoint関数を呼び出す
                playerRespawn.SetCheckpoint(transform.position);

                // 見た目を変える
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = activatedColor;
                }

                Debug.Log("チェックポイント通過！位置を保存しました: " + transform.position);
            }
            else
            {
                Debug.LogWarning("プレイヤーに 'PlayerRespawn' スクリプトが見つからないため、位置を保存できませんでした。");
            }

        }
    }
}