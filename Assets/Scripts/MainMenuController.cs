using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Animator bgAnimator;
    public InputMachine inputMachine;

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            bgAnimator.SetTrigger("ExitMainMenu");
            Debug.Log("Closing main menu");
            
            inputMachine.enabled = true;
            enabled = false;
        }    
    }
}
