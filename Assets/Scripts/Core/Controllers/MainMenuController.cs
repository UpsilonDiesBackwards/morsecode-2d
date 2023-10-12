using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Animator bgAnimator;
    public InputMachine inputMachine;

    void Start() {
        bgAnimator.SetTrigger("ShowMainMenu");
    }

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            bgAnimator.SetTrigger("HideMainMenu");            
            inputMachine.enabled = true;
            enabled = false;
        }
    }
}
