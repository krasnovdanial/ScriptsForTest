using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageChooser : MonoBehaviour
{
	public string serverURL;
	public RawImage fullscreenImage;

	private int imageID;

	public void Load(int imageID)
	{
		this.imageID = imageID;
		StartCoroutine(LoadImage());
	}

	private IEnumerator LoadImage()
	{
		UnityWebRequest request = UnityWebRequestTexture.GetTexture(serverURL + "/get_image.php?id=" + imageID);
		yield return request.SendWebRequest();
		if (request.result != UnityWebRequest.Result.Success)
		{
			Debug.LogError("Failed to load image: " + request.error);
		}
		else
		{
			fullscreenImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
		}
	}
	public void OnImageClicked()
	{
		ImageChooser loadFullscreenImage = FindObjectOfType<ImageChooser>();
		if (loadFullscreenImage != null)
		{
			loadFullscreenImage.Load(imageID);
			SceneManager.LoadScene("FullscreenImageScene");
		}
	}
}
