using UnityEngine;
// 新しい入力システムを使うためにこれを追加
using UnityEngine.InputSystem;

public class idou : MonoBehaviour
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
           

            if ( Keyboard.current.leftArrowKey.isPressed) inputVector.x = -1f;
            if ( Keyboard.current.rightArrowKey.isPressed) inputVector.x = 1f;
        }

        // 移動する方向のベクトルを作成
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        // キャラクターを移動させる
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);



        // 【修正例】移動している方向にキャラクターを向かせる

   

        



       
    }
}