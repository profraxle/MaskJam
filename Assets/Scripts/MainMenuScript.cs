using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    public string SceneToOpen;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnBeginClicked()
    {
        SceneManager.LoadScene(SceneToOpen);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("SCN_MainMenu");
    }
}
