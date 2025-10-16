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