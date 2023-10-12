using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAgentController : MonoBehaviour
{
    [SerializeField] Collider2D columboTheCollider;

    public float gridSize = 0.9f;

    private Vector2 directionToMove;
    private int timeToMove;

    [SerializeField] private int[] directionHalf = new int[2];
    [SerializeField] private int[] timeHalf = new int[2];

    public void InterpretMorse(int[] controllerArray) {

        Debug.Log(controllerArray[0]);
        Debug.Log(controllerArray[1]);
        Debug.Log(controllerArray[2]);
        Debug.Log(controllerArray[3]);

        // this is hell. I apologise. I forgive

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
        for (int i = 1; i <= time; i++) {
            transform.position += new Vector3(direction.x, direction.y);
            if (gameObject.GetComponent<Collider2D>().IsTouchingLayers())
            {
                Debug.Log("Fuck you Tayler");
            }
        }
        Debug.Log(direction);
        Debug.Log(time);
    }
}
