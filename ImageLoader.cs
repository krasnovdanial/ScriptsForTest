using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
	public string url;
	private Image image;

	void Start()
	{
		image = GetComponent<Image>();
		StartCoroutine(LoadImage());
	}

	IEnumerator LoadImage()
	{
		WWW www = new WWW(url);
		yield return www;

		if (www.error == null)
		{
			image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
		}
	}
}
