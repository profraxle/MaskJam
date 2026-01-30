using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OnBeginClicked()
    {
        SceneManager.LoadScene("SCN_Main");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
