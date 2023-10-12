using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgentController : MonoBehaviour
{
    public int[] directionHalf = new int[2];
    public int[] timeHalf = new int[2];

    public void InterpretMorse(int[] controllerArray) {
        Array.Copy(controllerArray, 0, directionHalf, 0 , 2); // Split the controller array into two arrays
        Array.Copy(controllerArray, 2, timeHalf, 0 , 2);

        // this is hell. I apologise

        if (directionHalf[0] == 0 && directionHalf[1] == 0) {
            Debug.Log("Move Right");
        }

        if (directionHalf[0] == 0 && directionHalf[1] == 1) {
            Debug.Log("Move Upwards");
        }

        if (directionHalf[0] == 1 && directionHalf[1] == 0) {
            Debug.Log("Move Downwards");
        }

        if (directionHalf[0] == 1 && directionHalf[1] == 1) {
            Debug.Log("Move Left");
        }

        if (timeHalf[0] == 0 && timeHalf[1] == 0) {
            Debug.Log("Move for 1 tile");
        }

        if (timeHalf[0] == 0 && timeHalf[1] == 1) {
            Debug.Log("Move for 2 tiles");
        }

        if (timeHalf[0] == 1 && timeHalf[1] == 0) {
            Debug.Log("Move for 3 tiles");
        }

        if (timeHalf[0] == 1 && timeHalf[1] == 1) {
            Debug.Log("Move for 4 tiles");
        }
    }
}
