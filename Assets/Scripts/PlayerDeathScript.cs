using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerDeathScript : MonoBehaviour
{
    [SerializeField]
    float DeathCountdownTime = 3;

    [SerializeField]
    RectTransform DeathScreenPanel;

    float TimeElapsed = 0;
    bool PlayerEnteredCCTV = false;

    float DeathScreenOpacity = 0;

    public void OnPlayerEnteredCCTV()
    {
        if(PlayerEnteredCCTV)
        {
            return;
        }
        PlayerEnteredCCTV = true;
        TimeElapsed = 0;
    }

    void Update()
    {
        if (PlayerEnteredCCTV)
        {
            TimeElapsed += Time.deltaTime;
            Debug.LogFormat("Time: {0}", TimeElapsed);

            DeathScreenOpacity = TimeElapsed / DeathCountdownTime;

            Image image = DeathScreenPanel.GetComponent<Image>();
            var tempColor = image.tintColor;
            tempColor.a = DeathScreenOpacity;
            image.tintColor = tempColor;

            if (TimeElapsed > DeathCountdownTime)
            {
                SceneManager.LoadScene("SCN_MainMenu");
            }
        }
    }
}
