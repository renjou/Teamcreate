using UnityEngine;

public class cf : MonoBehaviour
{
    // 追いかける対象
    public Transform target;

    // カメラ位置のずれ
    public Vector3 offset;

    // 追従のなめらかさ
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // 目標位置
        Vector3 targetPosition = target.position + offset;

        // Z固定（2D用）
        targetPosition.z = -10f;

        // なめらか移動
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}