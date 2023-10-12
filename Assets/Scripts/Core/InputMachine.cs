using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputMachine : MonoBehaviour {
    [Header("Time and References")]
    float timePressed = 0f;
    [SerializeField] float timeComp = 0.3f;
    [SerializeField] Animator[] animators;
    [SerializeField] Animator bgAnimator;
    [SerializeField] PlayerAgentController playerController;
    [SerializeField] int pI = 0;

    public int[] inputArray = new int[4];

    [Header("Audio")]
    public AudioSource aSource;
    public AudioClip DotSound;
    public AudioClip DashSound;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

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
        timePressed += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) {
            timePressed = 0;
            bgAnimator.SetTrigger("on");
        }
        if (Input.GetKeyUp(KeyCode.Space)) { 
            MorseTyper();
            bgAnimator.SetTrigger("off");
        }
    }

    void MorseTyper()
    {
        if (timePressed < timeComp)
        {
            animators[pI].SetTrigger("Dot");
            PlayDotSound();
            inputArray[pI] = 0;
        }
        if (timePressed > timeComp)
        {
            animators[pI].SetTrigger("Dash");
            PlayDashSound();
            inputArray[pI] = 1;
        }

        pI++;
        if ( pI > 3 )
        {
            // Debug.Log("Prosign Index Reset");
            playerController.InterpretMorse(inputArray);

            pI = 0;

            //run the entering command.
            MorseReset();
        }
        else { animators[pI].SetTrigger("Flash"); }
    }

    void MorseReset()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("Blank");
        }
        animators[pI].SetTrigger("Flash");
    }

    void PlayDotSound()
    {
        aSource.pitch = (Random.Range(minPitch, maxPitch));
        aSource.PlayOneShot(DotSound, 0.5f);
    }

    void PlayDashSound()
    {
        aSource.pitch = (Random.Range(minPitch, maxPitch));
        aSource.PlayOneShot(DashSound, 0.5f);
    }
}
