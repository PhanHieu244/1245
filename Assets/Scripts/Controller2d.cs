using System;
using UnityEngine;

public class Controller2d : MonoBehaviour
{
	[SerializeField]
	private LayerMask layerMask;

	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private float jumpForce = 15f;

	private Rigidbody2D rb;

	private Transform groundCheck;

	private float defaultGravity;

	private Animator animator;

	private float _radius = 0.2f;

	private bool hit;

	private void Start()
	{
		this.animator = base.gameObject.GetComponent<Animator>();
		this.rb = base.gameObject.GetComponent<Rigidbody2D>();
		this.groundCheck = GameObject.Find("GroundCheck").transform;
	}

	private void FixedUpdate()
	{
		float horizontal = this.getHorizontal();
		Vector2 velocity = default(Vector2);
		this.hit = Physics2D.OverlapCircle(this.groundCheck.position, this._radius, this.layerMask);
		velocity = new Vector2(horizontal * this.moveSpeed, this.rb.velocity.y);
		if (this.IsJump() && this.hit)
		{
			velocity.y = this.jumpForce;
		}
		this.rb.velocity = velocity;
	}

	public float getHorizontal()
	{
		bool flag = false;
		bool flag2 = false;
		Touch[] touches = Input.touches;
		for (int i = 0; i < touches.Length; i++)
		{
			Touch touch = touches[i];
			if (touch.position.x < (float)(Screen.width / 4) && touch.position.y < (float)(Screen.height / 2))
			{
				flag = true;
			}
			else if (touch.position.x > (float)(Screen.width / 4) && touch.position.x < (float)(Screen.width * 2 / 4) && touch.position.y < (float)(Screen.height / 2))
			{
				flag2 = true;
			}
		}
		return UnityEngine.Input.GetAxis("Horizontal") + ((!flag) ? 0f : -1f) + ((!flag2) ? 0f : 1f);
	}

	public bool IsJump()
	{
		return UnityEngine.Input.GetKey(KeyCode.Space);
	}
}
