using UnityEngine;
using UnityEngine.InputSystem; // 新しい入力システム用

public class PlayerControl1 : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        // オブジェクトについている Rigidbody 2D を取得
        rb = GetComponent<Rigidbody2D>();
    }

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

    // 物理演算による移動は Update ではなく FixedUpdate で行うのがUnityの鉄則です
    void FixedUpdate()
    {
        // Rigidbody の速度（velocity）を直接書き換えて移動させる
        rb.linearVelocity = movementInput * moveSpeed;
    }
}