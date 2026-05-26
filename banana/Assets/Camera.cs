using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    GameObject _plyaer;
    Vector3 _delPlayer;
    Vector3 _delPlayerNomal;
    Vector3 _setDel;
    [SerializeField] float distance = 4.0f;
    Quaternion _rot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // プレイヤーのオブジェクトを取得する
        _plyaer = GameObject.Find("Player");

        // 初期位置を正しい位置にする
        // プレイヤーとの距離を計算する
        // 目標の位置 - 現在の自分の位置
        // 自分からプレイヤーまで伸ばしたベクトル
        _delPlayer = _plyaer.transform.position - transform.position;

        // 正規化する
        // 自分からプレイヤー方向の単位ベクトルを作る
        _delPlayerNomal = _delPlayer.normalized;

        // プレイヤー方向の単位ベクトルに距離を掛ける
        // プレイヤーからカメラまでの相対ベクトルを作る
        _setDel = _delPlayer.normalized * distance;
        transform.position = _plyaer.transform.position - _setDel;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの位置から相対ベクトルを引いて、
        // カメラの現在地を出す
        transform.position = _plyaer.transform.position - _setDel;

        // カメラの正面をプレイヤーとの相対ベクトルにする
        _rot = Quaternion.LookRotation(_delPlayerNomal, Vector3.up);

        // 移動方向に回転
        transform.rotation = _rot;

    }

}

