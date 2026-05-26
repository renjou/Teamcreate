using UnityEngine;

public class test : MonoBehaviour
{
   [SerializeField] float _speed = 0.1f;
    Vector3 _playerInput;
    Quaternion _rot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    // PCの性能（ぶっちゃけディスプレイの性能）によって呼ばれる回数が変わる
    void Update()
    {
        // プレイヤーの入力
        _playerInput.x = Input.GetAxis("Horizontal"); // x方向
        _playerInput.z = Input.GetAxis("Vertical"); // z方向

        // 入力で作成した向きから、回転量(回転クォータニオン)を出す
        // (向きたい向き,対象の上ベクトル)
        Vector3 temp = _playerInput.normalized; // 方向ベクトルを正規化
        _rot = Quaternion.LookRotation(_playerInput,Vector3.up);
    }

    // デフォルトだと、1秒間に50回呼ばれます
     private void FixedUpdate()
    {
        // 入力値を使って移動
        transform.position += _playerInput * _speed;
        // 移動方向に回転
        transform.rotation = _rot;
    }
}
