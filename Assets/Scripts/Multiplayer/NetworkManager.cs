using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;
using RiptideNetworking.Utils;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _singleton;
    public static NetworkManager singleton 
    {   
        get 
        { 
            return _singleton;
        } 
        private set
        {
            if(_singleton == null)
                _singleton = value;
            else if(_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)}: instance already exists");
                Destroy(value);
            }
        }
    }

    public Server server { get; private set; }

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        server = new Server();
        server.Start(port, maxClientCount);
    }

    private void FixedUpdate()
    {
        server.Tick();
    }

    private void OnApplicationQuit()
    {
        server.Stop();
    }
}
