using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    private bool isDead = false;
    private Vector3 respawnPosition;
    private Checkpoint1 currentCheckpoint;
    private Rigidbody2D rb;
    //public NormalAttack normalAttack;
    
    void Start()
    {
        // 初期位置を最初のチェックポイントとして登録
        respawnPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // チェックポイントの位置を更新する関数（Checkpointスクリプトから呼ばれる）
    internal void SetCheckpoint(Checkpoint1 newCheckpoint)
    {
        if (currentCheckpoint != null && currentCheckpoint != newCheckpoint)
        {
            currentCheckpoint.ResetCheckpoint();
        }

        // 2. 新しいチェックポイントを現在のチェックポイントとして記憶
        currentCheckpoint = newCheckpoint;

        // 3. 復活位置（座標）を更新
        respawnPosition = newCheckpoint.transform.position;
    }
    // プレイヤーをチェックポイントに戻す関数
    
    private void Die()
    {
        isDead = true;
        Debug.Log("プレイヤーが死亡しました。死亡演出を開始します...");

        // （参考）もし死亡アニメーションがある場合
        // if (anim != null) anim.SetTrigger("Die"); 

        // 物理挙動を止めて、死亡中に動けないようにする
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // 1秒間のウエイト（間）を置いてからリスポーンさせる
        StartCoroutine(RespawnDelay(1.0f));
    }
    public void TriggerDeath()
    {
        if (isDead) return; // 既に死亡処理中なら何もしない

        isDead = true;
        Debug.Log("プレイヤーが死亡しました。死亡演出を開始します...");

        // 物理挙動を止めて、死亡中に動けないようにする
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // 1秒間のウエイト（間）を置いてからリスポーンさせる
        StartCoroutine(RespawnDelay(1.0f));
    }
    private IEnumerator RespawnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 位置をチェックポイントへ移動
        transform.position =respawnPosition;

        // 物理挙動（落下速度など）をリセットして、変な挙動を防ぐ
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Unity2022以前なら rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
       
       
        Debug.Log("プレイヤーがリスポーンしました");
    }

   
}