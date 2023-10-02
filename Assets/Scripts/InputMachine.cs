using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMachine : MonoBehaviour
{
    float timePressed = 0f;
    [SerializeField] float timeComp = 0.5f;
    [SerializeField] Animator[] animators;
    int x = 0;

    void Start()
    {
        
    }

    void Update()
    {
        InputTimer();
        Debug.Log(timePressed);
    }

    void InputTimer()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { timePressed = 0; }
        if (Input.GetKey(KeyCode.Space)) { timePressed += Time.deltaTime; }
        if (Input.GetKeyUp(KeyCode.Space)) { MorseTyper(); }
    }

    void MorseTyper()
    {
        if (timePressed > timeComp) { animators[x].SetTrigger("asd"); }
        if (timePressed < timeComp) { animators[x].SetTrigger("asd");}
        x++;
        if ( x > 4 )
        {
            x = 0;
            //run the entering command.
            MorseReset();
        }
    }

    void MorseReset()
    {
        foreach (Animator animator in animators)
        { 
            //if (animator.)
        }
    }
}
