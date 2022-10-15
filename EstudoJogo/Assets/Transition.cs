using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionType
{
    Warp,
    Scene
}

public class Transition : MonoBehaviour
{
    [SerializeField] TransitionType transitionType;
    [SerializeField] string sceneNameToTransition;
    [SerializeField] Vector3 targetPosition;

    Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.GetChild(1).transform;
    }

    internal void InitiateTransition(Transform toTransition)
    {
        Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();        

        switch (transitionType)
        {
            case TransitionType.Warp:
                currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, destination.position - toTransition.position);

                toTransition.position = new Vector3(destination.position.x, destination.position.y, toTransition.position.z);
                break;
            case TransitionType.Scene:
                currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, targetPosition - toTransition.position);

                GameSceneManager.instance.SwitchScene(sceneNameToTransition, targetPosition);
                break;
        }        
    }
}
