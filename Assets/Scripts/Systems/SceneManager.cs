using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �������� ����
/// </summary>
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

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

        OnSceneLoaded?.Invoke();
    }

    private void OnDestroy()
    {
        OnSceneLoaded = null;
    }

    /// <summary>
    /// �������� ����� �� �����.
    /// </summary>
    public void LoadScene(string sceneName)
    {
        if (!isLoading)
            StartCoroutine(LoadSceneRoutine(sceneName));
    }

    /// <summary>
    /// �������� ����� �� �������.
    /// </summary>
    public void LoadScene(int sceneIndex)
    {
        if (!isLoading)
            StartCoroutine(LoadSceneRoutine(sceneIndex));
    }

    /// <summary>
    /// ������������ ������� �����
    /// </summary>
    public void ReloadCurrentScene()
    {
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// �������� ��������� �����
    /// </summary>
    public void LoadNextScene()
    {
        int nextIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
            LoadScene(nextIndex);
        else
            Debug.LogWarning("��������� ����� ����������� � Build Settings.");
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        isLoading = true;

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

        isLoading = false;
        OnSceneLoaded?.Invoke();
    }

    private IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        isLoading = true;

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

        isLoading = false;
        OnSceneLoaded?.Invoke();
    }
}