using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RiptideNetworking;

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
        // The message ID is set to zero because there is only one type of message here
        // that changes the text on the server
        Message message = Message.Create(MessageSendMode.reliable, 0);
        message.AddString(text);
        NetworkManager.Singleton.Client.Send(message);
    }
}
