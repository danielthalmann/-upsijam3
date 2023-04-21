using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class JumpController : MonoBehaviour
{
	[SerializeField] private LayerMask groundLayerMask;
	public float jumpForce = 5f;
	private Rigidbody2D	rb;
	private CapsuleCollider2D cc;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		cc = GetComponent<CapsuleCollider2D>();
	}

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if (isGrounded())
		{
			if (Input.GetMouseButtonDown(0))
			{
				rb.velocity = Vector2.up * jumpForce * 1;
			}
			if (Input.GetMouseButtonDown(1))
			{
				rb.velocity = Vector2.up * jumpForce * 2;
			}
			if (Input.GetMouseButtonDown(2))
			{
				rb.velocity = Vector2.up * jumpForce * 0.5f;
			}
		}
    }

	bool isGrounded()
	{
		return Physics2D.Raycast(cc.bounds.center, Vector2.down, cc.bounds.extents.y + 0.1f, groundLayerMask);
	}

}
