using UnityEngine;

public class test : MonoBehaviour
{
    Vector3 _velocity; // 速度
    Vector3 _dir; // 向いてる方向
    Quaternion _rot; // 回転度合い
    [SerializeField] float _speed = 0.1f; // 速さと
    [SerializeField] float _rotSpeed = 0.1f; // 回転速度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 正面ベクトル
        _dir = transform.forward;
    }

    // Update is called once per frame
    // PCの性能（ぶっちゃけディスプレイの性能）によって呼ばれる回数が変わる
    void Update()
    {
        // 移動入力
        _velocity = Vector3.zero; // 速度ベクトルの初期化
        _velocity += transform.forward * Input.GetAxis("Vertical"); // 前後方向の入力を正面ベクトルに加える
        _velocity += transform.right * Input.GetAxis("Horizontal"); // 左右方向の入力ベクトルに加える
        _velocity.Normalize(); // 正規化

        // 回転方向の入力
        if(Input.GetKey(KeyCode.Q)) { _dir += -transform.right * _rotSpeed; } // 左回転
        if (Input.GetKey(KeyCode.E)) { _dir += -transform.right * _rotSpeed; } // 右回転
        _dir.Normalize(); // 回転方向を正規化
    }

    // デフォルトだと、1秒間に50回呼ばれます
     private void FixedUpdate()
    {
        // 移動
        transform.position += _velocity * _speed;

        // 正面方向に向く
        _rot = Quaternion.LookRotation(_dir); // 正面方向を向くクォータニオンを求める
        transform.rotation = _rot; // クォータニオンを回転に適用
    }
}
