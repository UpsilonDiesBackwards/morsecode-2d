using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAgentController : MonoBehaviour
{
    public float gridSize = 0.9f;

    private Vector2 directionToMove;
    private int timeToMove;

    [SerializeField] private int[] directionHalf = new int[2];
    [SerializeField] private int[] timeHalf = new int[2];

    public void InterpretMorse(int[] controllerArray) {
        Array.Copy(controllerArray, 0, directionHalf, 0 , 2); // Split the controller array into two arrays
        Array.Copy(controllerArray, 2, timeHalf, 0 , 2);

        Debug.Log("dir half: " + directionHalf[0] + directionHalf[1]);
                Debug.Log("time half: " + timeHalf[0] + timeHalf[1]);


        // this is hell. I apologise

        if (controllerArray[0] == 0 && controllerArray[1] == 0) { directionToMove = new Vector2(1, 0); }
        if (controllerArray[0] == 0 && controllerArray[1] == 1) { directionToMove = new Vector2(0, 1); }
        if (controllerArray[0] == 1 && controllerArray[1] == 0) { directionToMove = new Vector2(0, -1); }
        if (controllerArray[0] == 1 && controllerArray[1] == 1) { directionToMove = new Vector2(-1, 0); }

        if (controllerArray[2] == 0 && controllerArray[3] == 0) { timeToMove = 1; }
        if (controllerArray[2] == 0 && controllerArray[3] == 1) { timeToMove = 2; }
        if (controllerArray[2] == 1 && controllerArray[3] == 0) { timeToMove = 3; }
        if (controllerArray[2] == 1 && controllerArray[3] == 1) { timeToMove = 4; }

        Move(directionToMove, timeToMove);
    }

    void Move(Vector2 direction, int time) {
        /*for (int i = 0; i <= time; i++) {
            transform.position += new Vector3(direction.x * time, direction.y * time);
        }*/
        Debug.Log(direction);
        Debug.Log(time);
        transform.position += new Vector3(direction.x * time, direction.y * time);
    }
}
