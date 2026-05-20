using UnityEngine;
// 新しい入力システムを使うためにこれを追加
using UnityEngine.InputSystem;

public class PlayerControl1 : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    void Update()
    {
        // 新しいInput Systemでのキーボード入力の取得方法
        Vector2 inputVector = Vector2.zero;

        if (Keyboard.current != null)
        {
            // WASD や 矢印キー の入力を自動で判別してくれます
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) inputVector.y = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) inputVector.y = -1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) inputVector.x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) inputVector.x = 1f;
        }

        // 移動する方向のベクトルを作成
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        // キャラクターを移動させる
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // 移動している方向にキャラクターを向かせる
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }
}