using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
	[SerializeField] private GameObject particle;
	[SerializeField] private BirdScript bird;
	[SerializeField] private float speed = 1;
	
    void Update()
    {
		if (!bird.IsDead())
		{
			transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		BirdScript bird = collision.gameObject.GetComponent<BirdScript>();
		if (bird)
		{
			Collider2D collider = GetComponent<Collider2D>();
			if (collider)
			{
				collider.enabled = false;
			}
			
			bird.Dead();
		}

		BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
		if (bullet)
		{
			GameObject destroyParticle = Instantiate(particle, transform.position, Quaternion.identity);
			Destroy(destroyParticle, 1.5f);
			Destroy(this.gameObject);
			Destroy(bullet.gameObject);
		}
	}
}
