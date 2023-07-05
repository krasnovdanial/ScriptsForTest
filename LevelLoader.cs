using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
	public GameObject panel;
	public GameObject loadingScreen;
	public Slider slider;
	public string gallerySceneName;
	public Text progressText;

	

	public void LoadGalleryScene()
	{
		StartCoroutine(LoadSceneAsync());
		loadingScreen.SetActive(true);
		panel.SetActive(false);
	}

	IEnumerator LoadSceneAsync()
	{
		yield return new WaitForSeconds(2f);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gallerySceneName);

		while (!asyncLoad.isDone)
		{
			float loadProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
			progressText.text = Mathf.RoundToInt(loadProgress * 100) + "%";
			slider.value = loadProgress;
			yield return null;
		}

		loadingScreen.SetActive(false);
	}
}
