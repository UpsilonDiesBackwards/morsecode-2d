using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputMachine : MonoBehaviour {
    [Header("Time and References")]
    float timePressed = 0f;
    [SerializeField] float timeComp = 0.3f;
    [SerializeField] Animator[] animators;
    [SerializeField] int pI = 0; // formally 'x', this is the index of the last prosign inputted.

    [Header("Automatic Resetting")]
    private Coroutine AutoReset;
    [SerializeField] float autoResetDelay = 4.0f;

    [Header("Array Information")]
    public int[] ControllerArray = new int[4];

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
        if (timePressed < timeComp)
        {
            animators[pI].SetTrigger("Dot");
            PlayDotSound();
            ControllerArray[pI] = 0;
        }
        if (timePressed > timeComp)
        {
            animators[pI].SetTrigger("Dash");
            PlayDashSound();
            ControllerArray[pI] = 1;
        }

        pI++;
        if ( pI > 3 )
        {
            Debug.Log("Prosign Index Reset");
            pI = 0;

            if (pI == 3)
            {
                Debug.Log("Check on final prowsign");
            }

            //run the entering command.
            MorseReset();
        }

        animators[pI].SetTrigger("Flash");
    }

    void MorseReset()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("ShouldFade");
            ControllerArray[pI] = 0;
            if (animator.gameObject.tag == "FirstProsign") {
                animator.SetTrigger("Flash");
            } else {
                animator.SetTrigger("Blank");
            }
        }
    }

    IEnumerator AutoResetAfterDelay() {
        // Debug.Log("Coroutine started!");
        yield return new WaitForSeconds(autoResetDelay);
       
        MorseReset();
        AutoReset = null;
        // Debug.Log("Prosigns reset!");
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
