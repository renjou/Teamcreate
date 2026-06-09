using UnityEngine;

public class bane : MonoBehaviour
{
    [Header("ばねの設定")]
    public Vector3 targetPosition;  // 目標地点（ばねの根元）
    public float stiffness = 100f;  // ばねの硬さ（戻る力）
    public float damping = 10f;     // 減衰（ブレーキの強さ）

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // 初期位置を目標地点に設定
        targetPosition = transform.position;
    }

    void FixedUpdate()
    {
        // 1. 現在の位置から目標地点への変位（ズレ）を計算
        Vector3 displacement = targetPosition - transform.position;

        // 2. フックの法則（F = kx）に基づき、復元力を計算
        Vector3 springForce = displacement * stiffness;

        // 3. 速度に対する減衰力（摩擦）を計算
        Vector3 dampingForce = velocity * damping;

        // 4. 合計の加速度を計算（簡易的に質量 m = 1 とする）
        Vector3 acceleration = springForce - dampingForce;

        // 5. 速度と位置を更新
        velocity += acceleration * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }
}