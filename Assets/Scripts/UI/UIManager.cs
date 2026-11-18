using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject mainMenuScreen;

    void Start()
    {
        ShowTitle();
    }

    public void ShowTitle()
    {
        mainMenuScreen.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void ShowMainMenu()
    {
        titleScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
}
