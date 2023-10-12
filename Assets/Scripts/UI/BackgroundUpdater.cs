using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundUpdater : MonoBehaviour
{
    [SerializeField] Animator bgAnimator;

    void OnTapperPress() {
        // bgAnimator.SetTrigger("Pressed");
    }

    void OnTapperUnpress() {
        // bgAnimator.SetTrigger("Unpressed");
    }
}
