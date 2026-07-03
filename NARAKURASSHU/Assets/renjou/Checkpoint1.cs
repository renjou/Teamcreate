using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Checkpoint1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;
    private Color originalColor;

    [Header("活性化時の色（オプション）")]
    public Color activatedColor = Color.green;

    [SerializeField] private AudioClip soundToPlay;

    private AudioSource aoudioSource;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        aoudioSource = GetComponent<AudioSource>();
        if(aoudioSource != null)
        {
            aoudioSource=gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーが触れた、かつ、まだ起動していない場合
        if (collision.CompareTag("Player") && !isActivated)
        {
            // ヒエラルキーからマネージャー担当の「RespawnManager」を探す
            RespawnManager respawnManager = FindFirstObjectByType<RespawnManager>();

            if (respawnManager != null)
            {
                // ★他の二人のコードを汚さずに、リフレクション機能を使ってマネージャー内の辞書データを書き換える
                bool success = UpdateManagerDictionary(respawnManager, collision.transform, transform.position);

                if (success)
                {
                    isActivated = true;

                    // 見た目を変える
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.color = activatedColor;
                    }

                    Debug.Log("チェックポイント通過！マネージャーの復活位置を更新しました: " + transform.position);
                }
            }
            else
            {
                Debug.LogWarning("シーン内に 'RespawnManager' が見つからないため、位置を保存できませんでした。");
            }
            if(soundToPlay !=null && aoudioSource != null)
            {
                aoudioSource.PlayOneShot(soundToPlay);
            }
        }
    }
    

    /// <summary>
    /// RespawnManager内の private な辞書（respawnPoints）を安全に書き換える関数
    /// </summary>
    private bool UpdateManagerDictionary(RespawnManager manager, Transform playerTransform, Vector3 newPosition)
    {
        try
        {
            // RespawnManager から "respawnPoints" という名前の非公開変数（Dictionary）を取得
            FieldInfo field = typeof(RespawnManager).GetField("respawnPoints", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                // 実際の辞書オブジェクトを取り出す
                var respawnPoints = field.GetValue(manager) as Dictionary<Transform, Vector3>;

                if (respawnPoints != null && respawnPoints.ContainsKey(playerTransform))
                {
                    // マネージャーが持っているプレイヤーの復活位置を、このチェックポイントの位置に上書き！
                    respawnPoints[playerTransform] = newPosition;
                    return true;
                }
                else
                {
                    Debug.LogWarning("RespawnManagerにプレイヤーのTransformがまだ登録されていません。");
                }
            }
            else
            {
                Debug.LogWarning("RespawnManager 内に 'respawnPoints' という変数が見つかりません。名前が変更された可能性があります。");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"チェックポイント更新中にエラーが発生しました: {e.Message}");
        }
        return false;
    }

    public void ResetCheckpoint()
    {
        isActivated = false;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}