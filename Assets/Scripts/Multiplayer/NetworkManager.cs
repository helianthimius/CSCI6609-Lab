using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;
using RiptideNetworking.Utils;

// Written inspired by Riptide Tutorial (Assignment Reference)
// https://www.youtube.com/watch?v=6kWNZOFcFQw
public class NetworkManager : MonoBehaviour
{
    // https://en.wikipedia.org/wiki/Singleton_pattern
    // Make this class singleton to guarantee there is only one instance which is
    // accessible by other classes
    private static NetworkManager _singleton;
    public static NetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.Log("NetworkManager already exists");
                Destroy(value);
            }
        }

    }
    public Client Client { get; private set; }

    [SerializeField] private ushort port;
    [SerializeField] private string ip;

    private void Awake()
    {
        Singleton = this;
    }
    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Client = new Client();
        Client.Connect($"{ip}:{port}");
    }

    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
    // This function is called by fix intervals, so arriving messages can be handled here
    private void FixedUpdate()
    {
        Client.Tick();
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }
}
