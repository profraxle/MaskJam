using UnityEngine;
using UnityEngine.Events;

public class Punchable : MonoBehaviour
{
    public UnityEvent onPunch = new UnityEvent();
	public bool requirePunchMask = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        onPunch.AddListener(PunchCallbackTest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Punch()
    {
        onPunch.Invoke();
    }

    void PunchCallbackTest()
    {
        print(gameObject.name + " was punched");
    }
    
}
