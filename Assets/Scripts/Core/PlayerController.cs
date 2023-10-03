using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Settings to control the timings
    [SerializeField] private float holdTime = 0.0f; // How long the player has held down the input key for.
    [SerializeField] private float DotToDashThreshold = 0.3f; // The time that differentiates between a dash and a dot. (1 = 1sec)
    
    // Inputted Morse Code Array
    [SerializeField] private ProsignType[] morseInput = new ProsignType[2];
    [SerializeField] private int currentInputIndex = 0;
    
    // Coroutine
    private Coroutine noInputClear;
    [SerializeField] private float AutomaticClearDelay = 2.0f;

    // UI
    [SerializeField] Image directionIndex1;
    [SerializeField] Image directionIndex2;
    [SerializeField] Image timeIndex1;
    [SerializeField] Image timeIndex2;

    [SerializeField] Sprite dotProsign;
    [SerializeField] Sprite dashProsign;


    private enum ProsignType {
        Dot,
        Dash,
        Pause // This is a end case. This, theoretically should never be called.
    }

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            float hTime = GetHoldDownTime();

            // Debug.Log(GetProsign(hTime));
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            ProsignType pSign = GetProsign(holdTime);
            morseInput[currentInputIndex] = pSign;
            currentInputIndex = (currentInputIndex + 1) % 2;

            holdTime = 0.0f;
        
            if (noInputClear != null) {
                StopCoroutine(noInputClear);
            }
            noInputClear = StartCoroutine(AutomaticClearAfterDelay());
        }

        UpdateUI();

        // // DEBUG!! REMOVE ON BUILD! 
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     ClearMorseInput();
        // }
    }

    float GetHoldDownTime() {
        holdTime += Time.deltaTime;
        return holdTime;
    }

    void ClearMorseInput() {
        morseInput[0] = ProsignType.Pause;
        morseInput[1] = ProsignType.Pause;
    }

    ProsignType GetProsign(float holdTime) {
        if (holdTime <= DotToDashThreshold) {
            return ProsignType.Dot;
        } else {
            return ProsignType.Dash;
        }
        return ProsignType.Pause;
    }

    IEnumerator AutomaticClearAfterDelay() { // If the user hasn't inputted in X seconds, clear the first entry so they can try again
        yield return new WaitForSeconds(AutomaticClearDelay);

        ClearMorseInput();
        noInputClear = null;

        Debug.Log("No input! Automatically reset input array.");
    }

    void UpdateUI() {
        /* Lord, please have mercy on my soul for what I am about to do. I do not take pride in it,
           It is 2:37 AM, I need sleep. */
        
        if (morseInput[0] == ProsignType.Dot) {
            directionIndex1.sprite = dotProsign;
        } else if (morseInput[0] == ProsignType.Dash) {
            directionIndex1.sprite = dashProsign;
        }

        if (morseInput[1] == ProsignType.Dot) {
            directionIndex1.sprite = dotProsign;
        } else if (morseInput[1] == ProsignType.Dash) {
            directionIndex1.sprite = dashProsign;
        }
    }
}