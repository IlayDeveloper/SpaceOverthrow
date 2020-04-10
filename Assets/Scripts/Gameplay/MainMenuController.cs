using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void ToGameScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
