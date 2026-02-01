using UnityEngine;

public class HideTooltipsOnPause : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltips;
    [SerializeField]
    private PauseMenuScript pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tooltips.SetActive(!pauseMenu.Paused);
    }
}
