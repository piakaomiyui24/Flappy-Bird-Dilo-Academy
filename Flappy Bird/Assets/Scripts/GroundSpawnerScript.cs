using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GroundSpawnerScript : MonoBehaviour
{
	[SerializeField]
	private GroundScript groundRef;

	private GroundScript prevGround;

	private void SpawnGround()
	{
		if (prevGround != null)
		{
			GroundScript newGround = Instantiate(groundRef);
			
			newGround.gameObject.SetActive(true);

			prevGround.SetNextGround(newGround.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		GroundScript ground = collision.GetComponent<GroundScript>();
		
		if (ground)
		{
			prevGround = ground;
			
			SpawnGround();
		}
	}

}
