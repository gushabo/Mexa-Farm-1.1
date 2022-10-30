using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    
    private void Awake() {
        instance = this;    
    }

    [SerializeField] ScreenTint screenTint;
    [SerializeField] CameraConfiner cameraConfiner;
    string currentScene;

    /**
    this 2 async vars is cause if we dont use this to load and unload the info 
    of the previous scene this explotes literaly    
    */
    AsyncOperation load;
    AsyncOperation unload;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name; 
    }

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StartCoroutine(Transition(to,targetPosition));
    }

    //this makes the transition tinting the scene
    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenTint.Tint();
        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);
        SwitchScene(to, targetPosition);

        //after all the process of checking if the scene if fully load the we change it
        while(load != null & unload != null)
        {
            if(load.isDone){load = null;}
            if(unload.isDone){unload = null;}
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        cameraConfiner.UpdateBounds();
        screenTint.UnTint();
    }

    /*
    this should be private but im gonna leave it public cause 
    is more simple if there is a mistake or if i wanna use it in another case
    */
    public void SwitchScene(string to, Vector3 targetPosition)
    {
        //loads the scene
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;
        //gets the characteristics of the player
        Transform playerTransform = GameManager.instance.player.transform;
        //gets the camera
        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
        //change the position camera
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targetPosition - playerTransform.position);

        playerTransform.position = new Vector3(targetPosition.x,targetPosition.y,playerTransform.position.z);
    }
}
