using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentCheckpoint;
    private Rigidbody2D rb;

    void Start()
    {
        // 初期位置を最初のチェックポイントとして登録
        currentCheckpoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // チェックポイントの位置を更新する関数（Checkpointスクリプトから呼ばれる）
    internal  void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    // プレイヤーをチェックポイントに戻す関数
    public void Respawn()
    {
        // 位置をチェックポイントへ移動
        transform.position = currentCheckpoint;

        // 物理挙動（落下速度など）をリセットして、変な挙動を防ぐ
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Unity2022以前なら rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Debug.Log("プレイヤーがリスポーンしました");
    }

   
}