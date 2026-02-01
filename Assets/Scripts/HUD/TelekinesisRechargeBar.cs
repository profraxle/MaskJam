using UnityEngine;
using UnityEngine.UI;

public class TelekinesisRechargeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        slider.maxValue = playerMovement.teleportRechargeTime;
        slider.value = playerMovement.teleportRechargeTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.gameObject.GetComponent<TeleportMask>())
        {
            slider.gameObject.SetActive(true);
            slider.value = playerMovement.teleportRechargeTimer;
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }
}
