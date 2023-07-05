using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GalleryController : MonoBehaviour
{
	public GameObject imagePrefab;
	public Transform content;
	public List<string> imagesUrls;
	public ScrollRect scrollRect;

	private int currentIndex = 0;
	private int loadCount = 0;
	private int displayCount = 0;

	void Start()
	{
		LoadImages();
		scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
	}

	void LoadImages()
	{
		while (loadCount < imagesUrls.Count && displayCount < 66)
		{
			StartCoroutine(LoadImage(imagesUrls[currentIndex]));
			currentIndex++;
			loadCount++;
		}
	}

	IEnumerator LoadImage(string url)
	{
		GameObject imageObject = Instantiate(imagePrefab, content);
		ImageLoader imageLoader = imageObject.GetComponent<ImageLoader>();
		imageLoader.url = url;
		yield return null;

		displayCount++;
	}

	private void OnScrollValueChanged(Vector2 value)
	{
		if (value.y <= 0 && loadCount < imagesUrls.Count)
		{
			LoadImages();
		}
	}
}
