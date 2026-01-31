using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    public string SceneToOpen;

    public void OnBeginClicked()
    {
        SceneManager.LoadScene(SceneToOpen);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
