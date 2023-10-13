using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Animator bgAnimator;
    public InputMachine inputMachine;
    public LevelChanger levelChanger;

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            bgAnimator.SetTrigger("ExitMenu");            
            inputMachine.enabled = true;
            levelChanger.enabled = true;
            enabled = false;
        }
    }
}
