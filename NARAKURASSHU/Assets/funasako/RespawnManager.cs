using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    Dictionary<Transform, Vector3> respawnPoints = new Dictionary<Transform, Vector3>();

    // オブジェクト位置を登録
    private Transform playerTransform;
    private Vector3 currentCheckpointPosition;
    private bool hasCheckpoint = false;
    public void Register(Transform obj)
    {
        Debug.Log("REBORN");
        if (!respawnPoints.ContainsKey(obj))
        {
            respawnPoints.Add(obj, obj.position);
        }
    }
    public void RegisterPlayer(Transform player)
    {
        playerTransform = player;

        // 最初（チェックポイントに触れる前）の復活位置は、ゲーム開始時の初期位置にする
        currentCheckpointPosition = player.position;

        // 一括管理のリスト（辞書）にもプレイヤーを登録しておく
        Register(player);
    }

    // 【新規】チェックポイントに触れた時に、復活位置を更新する関数
    // ステージ上のチェックポイントスクリプトから呼び出します
    public void UpdateCheckpoint(Vector3 newCheckpointPosition)
    {
        currentCheckpointPosition = newCheckpointPosition;
        hasCheckpoint = true;
        Debug.Log($"チェックポイント位置を更新しました: {newCheckpointPosition}");
    }

    public void RespawnALL()
    {
        foreach (var pair in respawnPoints)
        {
            if (pair.Key == playerTransform && hasCheckpoint)
            {
                // 最新のチェックポイント位置へワープ
                pair.Key.position = currentCheckpointPosition;

                // 落下速度や移動慣性を完全にリセットして変な挙動を防ぐ
                if (pair.Key.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
                pair.Key.position = pair.Value;
            }
            else
            {
                pair.Key.position = pair.Value;

                if (pair.Key.TryGetComponent<Rigidbody2D>(out var enemyRb))
                {
                    enemyRb.linearVelocity = Vector2.zero;
                    enemyRb.angularVelocity = 0f;
                }
            }
        }
    }
}
