using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Vector3 respawnPointPosition;
    [SerializeField] string respawnPointScene;
    internal void StartRespawn()
    {
        GameSceneManager.instance.Respawn(respawnPointPosition, respawnPointScene);
    }
}
