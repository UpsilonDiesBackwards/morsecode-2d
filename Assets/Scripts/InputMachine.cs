using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMachine : MonoBehaviour
{
    float timePressed = 0f;
    [SerializeField] float timeComp = 0.3f;
    [SerializeField] Animator[] animators;
    [SerializeField] int pI = 0; // formally 'x', this is the index of the last prosign inputted.

    private Coroutine AutoReset;
    [SerializeField] float autoResetDelay = 4.0f;

    void Start()
    {
        animators[pI].SetTrigger("Flash");
    }

    void Update()
    {
        InputTimer();
        // Debug.Log(timePressed);
    }

    void InputTimer()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { timePressed = 0; }
        if (Input.GetKey(KeyCode.Space)) { timePressed += Time.deltaTime; }
        if (Input.GetKeyUp(KeyCode.Space)) { 
            MorseTyper(); 

            if (AutoReset != null) { // Start the countdown immediately (will change)
                StopCoroutine(AutoResetAfterDelay());
            }
            AutoReset = StartCoroutine(AutoResetAfterDelay());    
        }
    }

    void MorseTyper()
    {   
        if (timePressed > timeComp) { animators[pI].SetTrigger("Dash"); }
        if (timePressed < timeComp) { animators[pI].SetTrigger("Dot");}
        pI++;
        animators[pI].SetTrigger("Flash");
        if ( pI > 3 )
        {
            Debug.Log("Prosign Index Reset");
            pI = 0;
            //run the entering command.
            MorseReset();
        }
    }

    void MorseReset()
    {
        foreach (Animator animator in animators)
        { 
        }
        animators[pI].SetTrigger("Flash");
    }

    IEnumerator AutoResetAfterDelay() {
        Debug.Log("Coroutine started!");
        yield return new WaitForSeconds(autoResetDelay);
        MorseReset();

        AutoReset = null;
        Debug.Log("Prosigns reset!");
    }
}
