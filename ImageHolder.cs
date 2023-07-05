using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHolder : MonoBehaviour
{
	public Image image;
	public string url;

	void Start()
	{
		LoadImage();
	}

	void LoadImage()
	{
		StartCoroutine(LoadImageCoroutine());
	}

	IEnumerator LoadImageCoroutine()
	{
		WWW www = new WWW(url);
		yield return www;

		if (www.error == null)
		{
			image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);

			
			float aspectRatio = (float)www.texture.width / (float)www.texture.height;
			float desiredHeight = Mathf.Min(Screen.height, image.rectTransform.sizeDelta.y);
			float desiredWidth = desiredHeight * aspectRatio;

			image.rectTransform.sizeDelta = new Vector2(desiredWidth, desiredHeight);
		}
	}
}
