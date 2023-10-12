using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private int currentSceneIndex = 0;
    public string[] sceneNames;

    public InputMachine inputMachine;

    public GameObject playerAgent;

    // Start is called before the first frame update
    void Start() {
        SceneManager.LoadScene(sceneNames[currentSceneIndex], LoadSceneMode.Additive);
        FindPlayerAgent();
    }

    public void LoadNextLevel() {
        SceneManager.UnloadSceneAsync(sceneNames[currentSceneIndex]);
        
        currentSceneIndex++;
        SceneManager.LoadScene(sceneNames[currentSceneIndex], LoadSceneMode.Additive);
        // FindPlayerAgent();
    }

    void FindPlayerAgent() {
        playerAgent = GameObject.FindGameObjectWithTag("Player");
        playerAgent.GetComponent<PlayerAgentController>().FindLevelChangerScript();
        inputMachine.playerController = playerAgent.GetComponent<PlayerAgentController>();
    }

    // Update is called once per frame
    void Update() {
        FindPlayerAgent();
    }
}
