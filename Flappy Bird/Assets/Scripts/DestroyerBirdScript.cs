using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBirdScript : MonoBehaviour
{
    void Start()
    {
		transform.position = new Vector2(transform.position.x, Camera.main.orthographicSize + 0.5f); 
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		BirdScript bird = collision.gameObject.GetComponent<BirdScript>();
		if (bird)
		{
			bird.Dead();
		}
	}
}
