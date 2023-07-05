using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotation : MonoBehaviour
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
		if (!touched)
		{
			material.color = Color.red;
			touched = true;
		}
		else if (touched)
		{
			material.color = Color.yellow;
			touched = false;
		}
	}
}
