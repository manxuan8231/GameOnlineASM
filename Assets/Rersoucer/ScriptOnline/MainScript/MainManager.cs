using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : NetworkBehaviour, INetworkRunnerCallbacks
{
    public NetworkPrefabRef _character1PlayerPrefab;
    public NetworkPrefabRef _character2PlayerPrefab;

    public NetworkRunner _runner;
    public NetworkSceneManagerDefault _sceneManager;

    //khởi tạo các biến
    void Awake()
    {
        if(_runner == null)
        {
            GameObject runnerObj = new GameObject("NetworkRunner");
            _runner = runnerObj.AddComponent<NetworkRunner>();
            _runner.AddCallbacks(this);
            _sceneManager = runnerObj.AddComponent<NetworkSceneManagerDefault>();
        }
        ConnectToFusion();
    }
    async void ConnectToFusion()
    {
        Debug.Log("Conneting to fusion network....");
        _runner.ProvideInput = true; //cho phép người chơi nhập input
        string sessionName = "MyGameSession";//tên phiên

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,//chế độ shared Mode
            SceneManager = _sceneManager,
            SessionName = sessionName,
            PlayerCount = 5,//số lượng người chơi tối đa
            IsVisible = true,//có hiển thị phiên hay không
            IsOpen = true,//có cho phép người chơi khác tham gia hay không
        };
        //kết nối mạng vào Fusion
        var result = await _runner.StartGame(startGameArgs);
        if (result.Ok) 
        {
            Debug.Log("Connected to fussion network successfully!");
        }
        else
        {
            Debug.Log($"Failed to connect: {result.ShutdownReason}");
        }
    }


    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    //hàm này sẽ gọi khi kết nối mạng thành công
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("..PLayer join:" + player);
        if (_runner.LocalPlayer != player) return;
        //thực hiện spawn nhân vật cho người chơi
        var playerClass = PlayerPrefs.GetString("PlayerClass");
        var playerName = PlayerPrefs.GetString("PlayerName");

        var prefab = playerClass.Equals("Character1") ? _character1PlayerPrefab : _character2PlayerPrefab;
        var position = Vector3.zero;
        _runner.Spawn(
            prefab,
            position,
            Quaternion.identity,
            player,
            (re, obj) =>
            {
                Debug.Log("Spawn player:"+obj);
            });
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }
}
