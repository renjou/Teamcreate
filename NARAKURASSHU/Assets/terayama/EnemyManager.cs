using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enemy1[] enemies1;
    public enemy2[] enemies2;
    public enemy3[] enemies3;

    public void RespawnEnemies()
    {
        for (int i = 0; i < enemies1.Length; i++)
        {
     //       enemies1[i].Respawn();
        }

        for (int i = 0; i < enemies2.Length; i++)
        {
     //       enemies2[i].Respawn();
        }

        for (int i = 0;i < enemies3.Length; i++)
        {
     //       enemies3[i].Respawn();
        }
    }
}
