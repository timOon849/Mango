using Microsoft.AspNet.SignalR.Client;
using System;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    private HubConnection connection;
    private IHubProxy chatHubProxy;
    [SerializeField] private InputField inputField;
    [SerializeField] private Transform chatContentArea; // Content из ScrollView
    [SerializeField] private GameObject myMessagePrefab;  // Префаб своего сообщения
    [SerializeField] private GameObject otherMessagePrefab; // Префаб чужого сообщения
    [SerializeField] private ScrollRect scrollRect; // ScrollRect из ScrollView
    private string username = "Player"; // Можно заменить на ввод с клавиатуры
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
            chatHubProxy = connection.CreateHubProxy("/chatHub");

            // Подключаемся к серверу
            connection.Start().Wait();

            // Обрабатываем входящие сообщения
            chatHubProxy.On<string, string>("ReceiveMessage", (message, senderId) =>
            {
                Debug.Log($"[{senderId}]: {message}");
                DisplayMessage( senderId, message);
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
    public void OnSendButtonClicked()
    {
        string message = inputField.text.Trim();
        if (!string.IsNullOrEmpty(message))
        {
            SendChatMessage(username, message);
            inputField.text = "";
            DisplayMessage(username, message, true); // Отображаем сразу
        }
    }

    private void DisplayMessage(string user, string message, bool isMine = false)
    {
        GameObject prefab = isMine ? myMessagePrefab : otherMessagePrefab;
        GameObject messageGO = Instantiate(prefab, chatContentArea);

        TextMeshProUGUI textComponent = messageGO.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = $"<b>{user}</b>: {message}";
        }

        // Авто-прокрутка вниз
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}