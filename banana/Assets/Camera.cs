using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    GameObject _player; // Player所得用
    Vector3 _playerDir; // Playerの正面ベクトル
    Vector3 _playerToCamera; // Playerからカメラへのベクトル
    Vector3 _playerToCameraInit; // Playerからカメラの初期ベクトル
    Vector3 _playerToCameraXZInit; // PlayerからカメラのXZ空間での初期ベクトル
    float _delPlayerToCamera; // Playerからラメラへの距離
    float _delXZ; // XZ空間での距離
    float _delY;
    Quaternion _rot;

    void Start()
    {
        // Playerオブジェクトの取得
        _player = GameObject.FindGameObjectWithTag("Player");
        // Playerからカメラへの初期ベクトル
        _playerToCameraInit = transform.position -_player.transform.localPosition;
        // XYZ空間の距離
        _delPlayerToCamera = _playerToCameraInit.magnitude;
        // PlayerからカメラへのXZ空間での初期ベクトル
        _playerToCameraXZInit = new Vector3(_playerToCameraInit.x,0, _playerToCameraInit.z);
        // XZ空間での距離
        _delXZ = _playerToCameraXZInit.magnitude;
        // playerとカメラのY方向の距離
        _delY = _playerToCameraInit.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Playerの正面ベクトル取得
        _playerDir = _player.transform.forward;
        _playerDir = _playerDir.normalized; // 正規化して大きさ1にする。

        // 方向をかける、距離で、Playerとカメラのベクトルを作成
        _playerToCamera = _playerDir * _delXZ;

        // それにY成分を足してXYZ空間のベクトルに戻す
        _CameraToPlayer = _playerToCameraXZ - _delY * Vector3.up;

        // XZ空間でのカメラの位置を計算する
        transform.position = _player.transform.position - _CameraToPlayer;

        // カメラをプレイヤーの方向に回転させる
        _rot = Quaternion.LookRotation();
        transform.position = _rot;



    }

}

