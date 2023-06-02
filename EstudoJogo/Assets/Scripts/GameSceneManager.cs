using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] CameraConfiner cameraConfiner;

    [SerializeField] ScreenTint screenTint;
    string currentScene;

    AsyncOperation load;
    AsyncOperation unload;

    bool respawnTransition;

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StartCoroutine(Transition(to, targetPosition));
    }

    internal void Respawn(Vector3 respawnPointPosition, string respawnPointScene)
    {
        respawnTransition = true;

        if (currentScene != respawnPointScene)
        {
            InitSwitchScene(respawnPointScene, respawnPointPosition);
        }
        else
        {
            MoveCharacter(respawnPointPosition);
        }
    }

    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenTint.Tint();

        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);
        SwitchScene(to, targetPosition);

        while (load != null & unload != null)
        {
            if (load.isDone)
                unload = null;
            if (load.isDone)
                unload = null;
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        cameraConfiner.UpdateBounds();
        screenTint.UnTint();        
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void SwitchScene(string to, Vector3 targetPosition)
    {
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);

        currentScene = to;
        MoveCharacter(targetPosition);

    }

    private void MoveCharacter(Vector3 targetPosition)
    {
        Transform playerTransform = GameManager.instance.player.transform;

        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targetPosition - playerTransform.position);

        playerTransform.position = new Vector3(targetPosition.x, targetPosition.y, playerTransform.position.z);

        if (respawnTransition)
        {
            Character character = playerTransform.GetComponent<Character>();

            character.FullHeal();
            character.FullRest();
            character.isDead = false;
            character.isExhausted = false;
            playerTransform.GetComponent<DisableControl>().EnableControls();
            respawnTransition = false;
        }
    }
}
