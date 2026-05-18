using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Image[] life;

    public Sprite FullLife;
    public Sprite emptyLife;

    public void UpdateHP(int playerHP)
    {
        for (int i = 0; i < life.Length; i++)
        {
            if (i < playerHP)
            {
                life[i].sprite = FullLife;
            }
            else
            {
                life[i].sprite = emptyLife;
            }
        }

    }
}
