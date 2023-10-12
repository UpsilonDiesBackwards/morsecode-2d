using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgentController : MonoBehaviour
{
    // Start is called before the first frame update
    public void Move() {
        float dirIndex = Random.Range(0, 3);
        Vector2 directionToMove = new Vector2(0, 0);

        if (dirIndex == 0) { directionToMove = new Vector2(1, 0); }
        if (dirIndex == 1) { directionToMove = new Vector2(0, 1); }
        if (dirIndex == 2) { directionToMove = new Vector2(0, -1); }
        if (dirIndex == 3) { directionToMove = new Vector2(-1, 0); }

        transform.position += new Vector3(directionToMove.x, directionToMove.y);


        Debug.Log("Moving!");
    }
}
