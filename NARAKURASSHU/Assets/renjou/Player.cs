using UnityEngine;
using UnityEngine.InputSystem; // 新しい入力システム用

public class Player : MonoBehaviour
{
<<<<<<< Updated upstream
    [Header("移動速度")]
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        // オブジェクトについている Rigidbody 2D を取得
        rb = GetComponent<Rigidbody2D>();
    }

=======
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
>>>>>>> Stashed changes
    void Update()
    {
        // キーボードの入力を受け取る処理（Update内で行う）
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveY = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveY = -1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveX = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveX = 1f;
        }

        // 入力方向をまとめてキープ（斜め移動で速くならないように正規化）
        movementInput = new Vector2(moveX, moveY).normalized;
    }

<<<<<<< Updated upstream
    // 物理演算による移動は Update ではなく FixedUpdate で行うのがUnityの鉄則です
    void FixedUpdate()
    {
        // Rigidbody の速度（velocity）を直接書き換えて移動させる
        rb.linearVelocity = movementInput * moveSpeed;
=======
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
>>>>>>> Stashed changes
    }
}