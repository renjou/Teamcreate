using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	Animator animator;
	public float speed = 1f;
	public Rigidbody2D rb;

    void Start () {
		animator = GetComponent<Animator> ();
		rb= GetComponent<Rigidbody2D> ();

	}
	
	void Update () 
	{
		animator.speed = speed;
        

    }

	public void run() 
	{
        if (Input.GetKey(KeyCode.RightArrow))
        {// 右方向の移動入力
            Vector2 pos = transform.position;
            pos.x += 0.05f;
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {// 左方向の移動入力
            Vector2 pos = transform.position;
            pos.x -= 0.05f;
            transform.position = pos;
        }
        animator.SetTrigger ("run");
	}

	public void jump() {
		animator.SetTrigger ("jump");
	}

	public void attack1() {
		animator.SetTrigger ("attack1");
	}

	public void attack2() {
		animator.SetTrigger ("attack2");
	}

	public void attack3() {
		animator.SetTrigger ("attack3");
	}

	public void skill() {
		animator.SetTrigger ("skill");
	}

}
