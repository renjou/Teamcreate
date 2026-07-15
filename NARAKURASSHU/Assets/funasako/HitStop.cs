using UnityEngine;

public class HitStop : MonoBehaviour
{
    public bool isHitStop = false;
    public int time = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void HitStopBoot(int attack)
    {
        
        isHitStop = true;

        switch (attack)
        {
            case 0:
                break;
            case 1:
                time = 3;
                break;
            case 2:
                time = 6;
                break;
            case 3:
                time = 9;
                break;
        }
    }
}
