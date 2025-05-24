using Microsoft.AspNet.SignalR.Client;
using System;
using UnityEditor.VersionControl;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    private HubConnection connection;
    private IHubProxy chatHubProxy;

    private void Start()
    {
        ConnectToChat();
    }

    private void ConnectToChat()
    {
        try
        {
            // Создаем подключение
            connection = new HubConnection("http://localhost:5295/");
            chatHubProxy = connection.CreateHubProxy("ChatHub");

            // Подключаемся к серверу
            connection.Start().Wait();

            // Обрабатываем входящие сообщения
            chatHubProxy.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Debug.Log($"[{user}]: {message}");
                DisplayMessage(user, message);
            });

            Debug.Log("Connected to chat hub.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error connecting to chat: " + ex.Message);
        }
    }

    public void SendChatMessage(string user, string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            try
            {
                // Отправляем сообщение на сервер
                chatHubProxy.Invoke("SendMessage", user, message).Wait();
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to send message: " + ex.Message);
            }
        }
    }

    private void DisplayMessage(string user, string message)
    {
        // Здесь добавьте логику отображения сообщений в UI
        Debug.Log($"Displaying message: [{user}]: {message}");
    }
}