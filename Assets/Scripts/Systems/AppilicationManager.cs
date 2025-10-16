using UnityEngine;

/// <summary>
/// Менеджер приложения
/// </summary>
public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private float currentTime = 1;

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentTime = Time.timeScale;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentTime = Time.timeScale;
    }
}