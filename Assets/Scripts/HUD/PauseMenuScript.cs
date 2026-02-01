using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    string SceneToRestart;

    [SerializeField]
    InputActionReference PauseAction;

    public bool Paused = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PauseAction.action.Enable();
        GetComponent<CanvasGroup>().alpha = 0;
        Time.timeScale = 1f;
    }
    void TogglePause()
    {
        Paused = !Paused;
        if (Paused)
        {
            GetComponent<CanvasGroup>().alpha = 1;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
    }

    private void Update()
    {
        if (PauseAction.action.triggered)
        {
            TogglePause();
        }
    }

    public void OnContinueClicked()
    {
        TogglePause();
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneToRestart);
        GetComponent<CanvasGroup>().alpha = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("SCN_MainMenu");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
