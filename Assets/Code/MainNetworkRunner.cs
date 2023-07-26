using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainNetworkRunner : MonoBehaviour
{
    private NetworkRunner _networkRunner;
    private NetworkSceneManagerDefault _sceneManagerDefault;

    private void Awake()
    {
        _networkRunner = GetComponent<NetworkRunner>();
        _sceneManagerDefault = GetComponent<NetworkSceneManagerDefault>();
    }

    private void Start()
    {
        var clientTask = InitializeNetworkRunner(_networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(),
            SceneManager.GetActiveScene().buildIndex, null);
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner networkRunner, GameMode gameMode, NetAddress netAddress, SceneRef sceneRef, Action<NetworkRunner> initialized)
    {
        networkRunner.ProvideInput = true;

        return networkRunner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = netAddress,
            Scene =  sceneRef,
            SessionName = "Session Name",
            Initialized = initialized,
            SceneManager = _sceneManagerDefault,
            
        });
    }
}
