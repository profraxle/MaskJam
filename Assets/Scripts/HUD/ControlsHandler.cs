using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControlsHandler : MonoBehaviour
{
    private GameObject player;

    private WearMaskManager maskHandler;
    [SerializeField]
    private Text displayText;
    private string defaultControlsText = "E: Pick Up Mask\nLeft Click: Punch";
	private int updateOnNext = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        maskHandler = player.GetComponent<WearMaskManager>();
        maskHandler.maskChanged.AddListener(DoUpdateNextFrame);
        displayText.text = defaultControlsText;
    }

    // Update is called once per frame
    void Update()
    {
        if (updateOnNext == 0) {
			updateOnNext = -1;
			UpdateControlsOnScreen();
		} else {
			updateOnNext--;
		}
    }

	void DoUpdateNextFrame() {
		updateOnNext = 2;
	}

    void UpdateControlsOnScreen()
    {
        Mask equippedMask = player.GetComponent<Mask>();
        if (equippedMask)
        {
            string currentTooltip = "E: Drop Mask\nLeft Click: Punch\n";
            if (equippedMask.GetType() == typeof(PunchMask))
            {
                currentTooltip = "E: Drop Mask\nLeft Click: Mega Punch";
            }
            else
            {
                currentTooltip += equippedMask.customControlTooltip;
            }
			displayText.text = currentTooltip;
        }
        else
        {
            displayText.text = defaultControlsText;
        }
    }
}
