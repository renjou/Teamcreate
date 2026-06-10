using System;
using UnityEngine;
// 新しい入力システムを使うためにこれを追加
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public float moveSpeed = 5.0f;
    public float jumpForce = 8.0f;
    void Start()
    {
        // 自分のオブジェクトからSpriteRendererを取得
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // 新しいInput Systemでのキーボード入力の取得方法
        Vector2 inputVector = Vector2.zero;

        if (Keyboard.current != null)
        {
            // WASD や 矢印キー の入力を自動で判別してくれます
            
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) inputVector.x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) inputVector.x = 1f;
        }

        // 移動する方向のベクトルを作成
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        // キャラクターを移動させる
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        


        // 【修正例】移動している方向にキャラクターを向かせる

        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f); // 元の向き
        }
        else if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f); // 左右反転
        }

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // 上方向に向けて一瞬だけ力を加える（Impulse）
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (Keyboard.current != null && Keyboard.current.wKey.wasPressedThisFrame)
        {
            // 上方向に向けて一瞬だけ力を加える（Impulse）
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (Keyboard.current != null && Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            // 上方向に向けて一瞬だけ力を加える（Impulse）
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }



        if (moveDirection.y > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f); // 上向き

        }
        
    }

    internal void SetCheckpoint(Vector3 position)
    {
        throw new NotImplementedException();
    }
}