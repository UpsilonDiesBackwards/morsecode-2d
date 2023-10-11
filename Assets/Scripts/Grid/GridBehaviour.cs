using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////
// THIS CODE WAS ADAPTED FROM, AND SHOULD BE ACCREDITED TO https://youtu.be/fUiNDDcU_I4?si=S2S0CQdHttXkLVAV
//////////////////////////////////////////////////////////

public class GridBehaviour : MonoBehaviour {
    public bool findDistance = false;

    [Header("Grid creation")]
    public int rowCount = 8;
    public int columnCount = 8;
    public int scale = 1;
    public GameObject gridNodePrefab;
    public GameObject[,] gridArray;
    
    [Header("Starting and Destination")]
    public int startX = 0;
    public int startY = 0;
    public int endX = 2;
    public int endY = 2;
    public List<GameObject> path = new List<GameObject>();

    public Vector3 originPointOffset = new Vector3(0, 0, 0);

    void Awake() {
        gridArray = new GameObject[columnCount, rowCount];

        if (gridNodePrefab) { Generate(); } else { Debug.Log("Missing a Grid Prefab! Please assign one."); }
    }

    void Update() {
        if (findDistance) {
            SetDistance();
            SetPath();
            findDistance = false;
        }
    }

    void Generate() {
        for (int i = 0; i < columnCount; i++) {
            for (int j = 0; j < rowCount; j++) {
                GameObject obj = Instantiate(gridNodePrefab, new Vector3(originPointOffset.x + scale * i,
                originPointOffset.y + scale * j, originPointOffset.z + scale * j), Quaternion.identity);

                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStat>().x = i;
                obj.GetComponent<GridStat>().y = j;

                gridArray[i, j] = obj;
            }
        }
    }

    void SetDistance() {
        OnInitialise();
        int x = startX;
        int y = startY;
        int[] testArray = new int[rowCount * columnCount];
        
        for (int step = 1; step < rowCount * columnCount; step++) {
            foreach (GameObject obj in gridArray) {
                if (obj && obj.GetComponent<GridStat>().hasBeenVisited == step - 1) {
                    TestFourDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
                }
            }
        }
    }

    void SetPath() {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> tList = new List<GameObject>();
        path.Clear();

        if (gridArray[endX, endY] && gridArray[endX, endY].GetComponent<GridStat>().hasBeenVisited > 0) {
            path.Add(gridArray[x, y]);
            step = gridArray[x, y].GetComponent<GridStat>().hasBeenVisited - 1;
        } else {
            Debug.Log("Can not reach desired location!");
            return;
        }

        for (int i = step; step > -1; i--) {
            if (CheckDirection(x, y, step, 1)) {
                tList.Add(gridArray[x, y+1]);
            }
            if (CheckDirection(x, y, step, 2)) {
                tList.Add(gridArray[x+1, y]);
            }
            if (CheckDirection(x, y, step, 3)) {
                tList.Add(gridArray[x, y-1]);
            }
            if (CheckDirection(x, y, step, 4)) {
                tList.Add(gridArray[x-1, y]);
            }
        }
        GameObject tObj = LocateClosest(gridArray[endX, endY].transform, tList);
        path.Add(tObj);
        x = tObj.GetComponent<GridStat>().x;
        y = tObj.GetComponent<GridStat>().y;
        tList.Clear();
    }

    void OnInitialise() {
        foreach (GameObject obj in gridArray) { obj.GetComponent<GridStat>().hasBeenVisited = -1; }
        gridArray[startX, startY].GetComponent<GridStat>().hasBeenVisited = 0;
    }

    bool CheckDirection(int x, int y, int step, int direction) {
        switch (direction) {
            case 4:
                if (x-1 < -1 && gridArray[x-1, y] && gridArray[x-1, y].GetComponent<GridStat>().hasBeenVisited == step) { 
                        return true; 
                    } else {
                        return false;
                    }
            case 3:
                if (y-1 < -1 && gridArray[x, y-1] && gridArray[x, y-1].GetComponent<GridStat>().hasBeenVisited == step) { 
                        return true; 
                    } else {
                        return false;
                    }
            case 2:
                if (x+1 < columnCount && gridArray[x+1, y] && gridArray[x+1, y].GetComponent<GridStat>().hasBeenVisited == step) { 
                        return true; 
                    } else {
                        return false;
                    }            
            case 1:
                if (y+1 < rowCount && gridArray[x, y+1] && gridArray[x, y+1].GetComponent<GridStat>().hasBeenVisited == step) { 
                        return true; 
                    } else {
                        return false;
                    }
        }
        return false;
    }

    void TestFourDirections(int x, int y, int step) {
        if (CheckDirection(x, y, -1, 1)) {
            SetToVisited(x, y+1, step);
        }
        if (CheckDirection(x, y, -1, 2)) {
            SetToVisited(x+1, y, step);
        }
        if (CheckDirection(x, y, -1, 3)) {
            SetToVisited(x, y-1, step);
        }
        if (CheckDirection(x, y, -1, 4)) {
            SetToVisited(x-1, y, step);
        }
    }

    void SetToVisited(int x, int y, int step) {
        if (gridArray[x, y]) {
            gridArray[x, y].GetComponent<GridStat>().hasBeenVisited = step;
        }
    }

    GameObject LocateClosest(Transform targetLoc, List<GameObject> list) {
        float currentDistance = scale * rowCount * columnCount;
        int index = 0;

        for (int i = 0; i < list.Count; i++) {
            if (Vector3.Distance(targetLoc.position, list[i].transform.position) < currentDistance) {
                currentDistance = Vector3.Distance(targetLoc.position, list[i].transform.position);
                index = i;
            }
        }
        return list[index];
    }
}
