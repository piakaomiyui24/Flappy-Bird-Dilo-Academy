using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	[SerializeField] private BirdScript bird;
	[SerializeField] private float speed = 1;

	Rigidbody2D rigidbody;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	void Start()
    {
		rigidbody.velocity = transform.right * speed;
	}

	private void Update()
	{
		if (bird.IsDead())
		{
			rigidbody.velocity = Vector2.zero;
		}
	}
}
