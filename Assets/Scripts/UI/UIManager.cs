using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;

    private GameObject currentScreen;

    private void Start()
    {
        if (screens.Length > 0)
        {
            ShowScreen(0);
        }
    }

    public void ShowScreen(int screenIndex)
    {
        if (screenIndex < 0 || screenIndex >= screens.Length) return;

        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
        }

        currentScreen = screens[screenIndex];
        currentScreen.SetActive(true);
    }

    public void ShowScreen(string screenName)
    {
        foreach (GameObject screen in screens)
        {
            if (screen.name == screenName)
            {
                if (currentScreen != null)
                {
                    currentScreen.SetActive(false);
                }

                currentScreen = screen;
                currentScreen.SetActive(true);
                return;
            }
        }
    }

    public void HideCurrentScreen()
    {
        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
            currentScreen = null;
        }
    }

    public void ToggleScreen(int screenIndex)
    {
        if (currentScreen == screens[screenIndex])
        {
            HideCurrentScreen();
        }
        else
        {
            ShowScreen(screenIndex);
        }
    }
}