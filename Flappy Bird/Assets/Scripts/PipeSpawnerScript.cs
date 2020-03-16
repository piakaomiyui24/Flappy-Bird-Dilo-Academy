using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
	[SerializeField] private BirdScript bird;
	[SerializeField] private PipeScript pipeUp, pipeDown;
	[SerializeField] private PointScript point;
	[SerializeField] private float spawnInterval = 1f;
	[SerializeField] private float maxHoleSize = 3f;
	[SerializeField] private float minHoleSize = 1f;
	[SerializeField] private float maxMinOffset = 1f;

	float holeSize;

	private Coroutine CR_Spawn;

	void Start()
    {
		StartSpawn();
	}

	void StartSpawn()
	{
		if (CR_Spawn == null)
		{
			CR_Spawn = StartCoroutine(IeSpawn());
		}
	}

	void StopSpawn()
	{
		if (CR_Spawn != null)
		{
			StopCoroutine(CR_Spawn);
		}
	}

	void SpawnPipe()
	{
		float y = maxMinOffset * Mathf.Sin(Time.time);
		holeSize = Random.Range(minHoleSize, maxHoleSize);

		PipeScript newPipeUp = Instantiate(pipeUp, transform.position, Quaternion.Euler(0, 0, 180));
		newPipeUp.transform.position += Vector3.up * (holeSize / 2);
		newPipeUp.transform.position += Vector3.up * y;
		newPipeUp.gameObject.SetActive(true);
		
		PipeScript newPipeDown = Instantiate(pipeDown, transform.position, Quaternion.identity);
		newPipeDown.transform.position += Vector3.down * (holeSize / 2);
		newPipeDown.transform.position += Vector3.up * y;
		newPipeDown.gameObject.SetActive(true);

		PointScript newPoint = Instantiate(point, transform.position, Quaternion.identity);
		newPoint.gameObject.SetActive(true);
		newPoint.SetSize(Camera.main.orthographicSize * 2);
		newPoint.transform.position += Vector3.up * y;
	}

	IEnumerator IeSpawn()
	{
		while (true)
		{
			if (bird.IsDead())
			{
				StopSpawn();
			}
			
			SpawnPipe();
			
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
