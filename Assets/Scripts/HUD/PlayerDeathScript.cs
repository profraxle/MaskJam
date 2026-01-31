using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerDeathScript : MonoBehaviour
{
    [SerializeField]
    float DeathCountdownTime = 3;
    [SerializeField]
    float FadeOutTime = 0.2f;

    [SerializeField]
    RectTransform DeathScreenPanel;
    CanvasGroup DeathImage;

    float DeathTimeElapsed = 0;
    float FadeOutTimeElapsed = 0;
    float FadeOutStartOpacity = 0;
    int CamerasViewingPlayer = 0;
    private void Start()
    {
        DeathImage = DeathScreenPanel.GetComponent<CanvasGroup>();
        DeathImage.alpha = 0;
    }

    public void OnPlayerEnteredCCTV()
    {
        if(CamerasViewingPlayer == 0)
        {
            DeathTimeElapsed = 0;
        }
        CamerasViewingPlayer++;
    }

    public void OnPlayerLeftCCTV()
    {
        CamerasViewingPlayer--;
        if(CamerasViewingPlayer == 0)
        {
            FadeOutTimeElapsed = FadeOutTime;
            FadeOutStartOpacity = DeathImage.alpha;
        }
    }

    void Update()
    {
        if (CamerasViewingPlayer > 0)
        {
            DeathTimeElapsed += Time.deltaTime;
            DeathImage.alpha = DeathTimeElapsed / DeathCountdownTime;

            if (DeathTimeElapsed > DeathCountdownTime)
            {
                SceneManager.LoadScene("SCN_MainMenu");
            }
        }
        else if (FadeOutTimeElapsed > 0)
        {
            FadeOutTimeElapsed -= Time.deltaTime;
            DeathImage.alpha = Mathf.Lerp(FadeOutStartOpacity, 0, 1 - (FadeOutTimeElapsed / FadeOutTime));
        }
    }
}
