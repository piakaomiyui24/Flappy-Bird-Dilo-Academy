using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
	[SerializeField] private float upForce = 100f;
	[SerializeField] private float fireRate = 2f;
	[SerializeField] private float waitToFire = 0f;
	[SerializeField] private int score;
	[SerializeField] private bool isDead;
	[SerializeField] private UnityEvent OnJump, OnDead, OnAddPoint, OnShoot;
	[SerializeField] private Text scoreText;
	[SerializeField] private BulletScript bullet;
	[SerializeField] private Transform shootingPoint;

	private Rigidbody2D rigidbody;
	private Animator animator;

	void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
    void Update()
    {
		if (!isDead && Input.GetMouseButtonDown(0))
		{
			Jump();
		}

		if (!isDead && Input.GetMouseButtonDown(1) && Time.time > waitToFire)
		{
			waitToFire = Time.time + fireRate;
			Shoot();
		}
	}

	private void Shoot()
	{
		if (OnShoot != null)
		{
			OnShoot.Invoke();
		}

		BulletScript _bullet = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
		_bullet.gameObject.SetActive(true);
	}

	private void Jump()
	{
		if (rigidbody)
		{
			rigidbody.velocity = Vector2.zero;
			rigidbody.AddForce(new Vector2(0, upForce));
		}
		
		if (OnJump != null)
		{
			OnJump.Invoke();
		}
	}

	public bool IsDead()
	{
		return isDead;
	}

	public void Dead()
	{
		if (!isDead && OnDead != null)
		{
			OnDead.Invoke();
		}
		
		isDead = true;

	}

	public void AddScore(int value)
	{
		score += value;
		scoreText.text = score.ToString();

		if (OnAddPoint != null)
		{
			OnAddPoint.Invoke();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		animator.enabled = false;
	}
}
