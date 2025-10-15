using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    [Header("Настройки перехода")]
    public CanvasGroup fadeCanvas;   
    public float fadeDuration = 0.5f;

    public UnityEvent OnSceneLoaded; 

    private bool isLoading = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Загрузить сцену по имени.
    /// </summary>
    public void LoadScene(string sceneName)
    {
        if (!isLoading)
            StartCoroutine(LoadSceneRoutine(sceneName));
    }

    /// <summary>
    /// Загрузить сцену по индексу.
    /// </summary>
    public void LoadScene(int sceneIndex)
    {
        if (!isLoading)
            StartCoroutine(LoadSceneRoutine(sceneIndex));
    }

    public void ReloadCurrentScene()
    {
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        int nextIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
            LoadScene(nextIndex);
        else
            Debug.LogWarning("Следующая сцена отсутствует в Build Settings.");
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        isLoading = true;

        yield return StartCoroutine(Fade(1));

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        yield return StartCoroutine(Fade(0));

        isLoading = false;
        OnSceneLoaded?.Invoke();
    }

    private IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        isLoading = true;

        yield return StartCoroutine(Fade(1));

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        yield return StartCoroutine(Fade(0));

        isLoading = false;
        OnSceneLoaded?.Invoke();
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvas == null) yield break;

        float startAlpha = fadeCanvas.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = targetAlpha;
    }
}
