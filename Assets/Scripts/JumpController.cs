using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class JumpController : MonoBehaviour
{
	[SerializeField] private LayerMask groundLayerMask;

	private Rigidbody2D	rb;
	private CapsuleCollider2D cc;

	public AudioLoudnessDetection detector;
	public float loudnessSensibility = 100;
	public float threshold = 10;

	public float minJump = 3;
	public float maxJump = 10;

	public float maxLoudness = 100;

	//plazer animation states

	Animator _animator;
	string _currentState;
	const string RUNNING = "running";
	const string JUMPINGSTART = "JumpingStart";
    const string JUMPINGTOP = "JumpingTop";
    const string JUMPINGEND = "JumpingEnd";

    void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		cc = GetComponent<CapsuleCollider2D>();
	}

    // Start is called before the first frame update
    void Start()
    {
		_animator = gameObject.GetComponent<Animator>();	
    }


    // Update is called once per frame
    void Update()
    {
		if (isGrounded())
		{
            if (isAnimationPlaying(_animator, JUMPINGTOP))
            {
                ChangeAnimationState(JUMPINGEND);
            }
            if (isAnimationPlaying(_animator, JUMPINGEND))
			{
				ChangeAnimationState(RUNNING);
			}
			else
                ChangeAnimationState(RUNNING);
            /* jump with microphone */

            float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
			if (loudness >= threshold)
			{
				//Debug.Log(loudness);
				rb.velocity = Vector2.up * (minJump + (maxJump - minJump) * loudness / maxLoudness);
				ChangeAnimationState(JUMPINGSTART);
                if (isAnimationPlaying(_animator, JUMPINGSTART))
                {
                    ChangeAnimationState(JUMPINGTOP);
                }
				else
                    ChangeAnimationState(JUMPINGTOP);
            }

			/* jump with mouse click: */

			float	jumpForce = 5;

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

	// change animation state

	private void ChangeAnimationState(string newState)
	{
		if (newState == _currentState)
			return;
		_animator.Play(newState);
		_currentState = newState;
	}

	// check if a specific animation is playing
	// Parameter named "0" is the animation layer

	bool isAnimationPlaying(Animator animator, string stateName)
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
			animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		{
			return true;
		}
		else
			return false;
    }

}
