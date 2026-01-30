using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathScript : MonoBehaviour
{
    public void OnPlayerEnteredCCTV()
    {
        SceneManager.LoadScene("SCN_MainMenu");
    }
}
