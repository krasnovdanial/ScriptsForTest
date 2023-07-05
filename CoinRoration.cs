using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRoration : MonoBehaviour
{
	public float rotationSpeed = 50f;
	private bool touched = false;
	private Material material;

	void Start()
	{
		material = GetComponent<Renderer>().material;
	}

	void Update()
	{
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}

	void OnMouseDown()
	{
		touched = !touched;
		if (touched)
		{
			material.color = Color.red;
			touched = true;
		}
		else
		{
			material.color = Color.white;
			touched = false;
		}
	}
}
