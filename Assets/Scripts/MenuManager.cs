using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RiptideNetworking;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // https://www.youtube.com/watch?v=pcyiub1hz20    
    public void Play()
    {
        // Load the main scene and go to the game
        SceneManager.LoadScene("SampleScene");
    }
    public void B1()
    {
        Send("B1");
    }
    public void B2()
    {
        Send("B2");
    }
    public void B3()
    {
        Send("B3");
    }

    // Creates a message; using NetworkManager, it sends the message from client to server
    void Send(string text)
    {
        Message message = Message.Create(MessageSendMode.reliable, (ushort)NetworkManager.MessageType.ChangeText);
        message.AddString(text);
        NetworkManager.Singleton.Client.Send(message);
    }

    // Connect the server using the given IP Address and port
    public void Connect()
    {
        // https://gamedev.stackexchange.com/questions/132569/how-do-i-find-an-object-by-type-and-name-in-unity-using-c
        // Find the input object and read the text value
        var addr = GameObject.Find("AddressInput").GetComponent<TMP_InputField>().text;
        NetworkManager.Singleton.Client.Connect(addr);
    }
}
