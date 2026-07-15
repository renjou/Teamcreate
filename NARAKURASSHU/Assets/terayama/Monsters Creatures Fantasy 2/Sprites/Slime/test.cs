using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    public EnemyManager enemyManager;

    void Update()
    {
        if (Keyboard.current.qKey.wasReleasedThisFrame)
        {
            enemyManager.RespawnEnemies();
        }
    }
}