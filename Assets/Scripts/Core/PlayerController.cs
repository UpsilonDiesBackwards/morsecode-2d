using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float holdTime = 0.0f; // How long the player has held down the input key for.
    [SerializeField] private float DotToDashThreshold = 0.3f; // The time that differentiates between a dash and a dot.

    private enum ProsignType {
        Dot,
        Dash,
        Pause // This is a end case. This, theoretically should never be called.
    }

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            float hTime = GetHoldDownTime();
            Debug.Log(GetProsign(hTime));
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdTime = 0.0f;
        }
    }

    float GetHoldDownTime() {
        holdTime += Time.deltaTime;
        return holdTime;
    }

    ProsignType GetProsign(float holdTime) {
        if (holdTime <= DotToDashThreshold) {
            return ProsignType.Dot;
        } else {
            return ProsignType.Dash;
        }
        return ProsignType.Pause;
    }
}