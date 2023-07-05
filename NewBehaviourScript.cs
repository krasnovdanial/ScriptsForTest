using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
	public string serverURL;
	public GameObject imagePrefab;
	public GridLayoutGroup gridLayoutGroup;

	private int columnCount;
	private float cellSize;
	private float spacing;
	private float padding;

	private IEnumerator Start()
	{
		yield return StartCoroutine(GetImageCount());
		CreateImages();
		gridLayoutGroup.constraintCount = columnCount;
		StartCoroutine(LoadImages());
	}

	private IEnumerator GetImageCount()
	{
		UnityWebRequest request = UnityWebRequest.Get(serverURL + "/get_image_count.php");
		yield return request.SendWebRequest();
		if (request.result != UnityWebRequest.Result.Success)
		{
			Debug.LogError("Failed to get image count: " + request.error);
			yield break;
		}
		int imageCount = int.Parse(request.downloadHandler.text);
		columnCount = 2;
		cellSize = (gridLayoutGroup.transform as RectTransform).rect.width / columnCount - gridLayoutGroup.spacing.x * (columnCount - 1) / columnCount;
		spacing = gridLayoutGroup.spacing.x;
		padding = gridLayoutGroup.padding.left + gridLayoutGroup.padding.right;
		gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
		int rowCount = Mathf.CeilToInt((float)imageCount / columnCount);
		gridLayoutGroup.padding.top = Mathf.RoundToInt((gridLayoutGroup.transform as RectTransform).rect.height / 2 - rowCount * cellSize / 2 - (rowCount - 1) * spacing / 2);
	}

	private void CreateImages()
	{
		for (int i = 1; i <= columnCount; i++)
		{
			for (int j = 1; j <= gridLayoutGroup.constraintCount; j++)
			{
				GameObject imageObject = Instantiate(imagePrefab);
				imageObject.transform.SetParent(gridLayoutGroup.transform);
				imageObject.transform.localScale = Vector3.one;
				imageObject.SetActive(false);
			}
		}
	}

	private IEnumerator LoadImages()
	{
		for (int i = 1; i <= columnCount; i++)
		{
			for (int j = 1; j <= gridLayoutGroup.constraintCount; j++)
			{
				int index = (j - 1) * columnCount + i;
				UnityWebRequest request = UnityWebRequestTexture.GetTexture(serverURL + "/get_image.php?id=" + index);
				yield return request.SendWebRequest();
				if (request.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError("Failed to load image: " + request.error);
				}
				else
				{
					GameObject imageObject = gridLayoutGroup.transform.GetChild((j - 1) * columnCount + i - 1).gameObject;
					imageObject.SetActive(true);
					RawImage rawImage = imageObject.GetComponent<RawImage>();
					rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
				}
			}
		}
		gridLayoutGroup.transform.parent.GetComponent<ScrollRect>().content = gridLayoutGroup.GetComponent<RectTransform>();
		gridLayoutGroup.transform.parent.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
	}
}
