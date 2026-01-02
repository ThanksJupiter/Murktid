using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class NetcodeTestManager : MonoBehaviour
{
    public Button hostButton;
    public Button clientButton;

    private VisualElement rootVisualElement;
    //Button serverButton;
    private Label statusLabel;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        rootVisualElement = uiDocument.rootVisualElement;

        hostButton = CreateButton("Host Button", "Host");
        clientButton = CreateButton("ClientButton", "Client");
        //serverButton = CreateButton("ServerButton", "Server");
        statusLabel = CreateLabel("StatusLabel", "Not Connected");

        rootVisualElement.Clear();
        rootVisualElement.Add(hostButton);
        rootVisualElement.Add(clientButton);
        //rootVisualElement.Add(serverButton);
        rootVisualElement.Add(statusLabel);

        //hostButton.clicked += OnHostButtonClicked;
        //clientButton.clicked += OnClientButtonClicked;
        //serverButton.clicked += OnServerButtonClicked;
    }

    private void OnDisable()
    {
        //hostButton.clicked -= OnHostButtonClicked;
        //clientButton.clicked -= OnClientButtonClicked;
        //serverButton.clicked -= OnServerButtonClicked;
    }

    void OnHostButtonClicked() => NetworkManager.Singleton.StartHost();
    void OnClientButtonClicked() => NetworkManager.Singleton.StartClient();
    //void OnServerButtonClicked() => NetworkManager.Singleton.StartServer();

    private void Update()
    {
        UpdateUI();
    }

    private Button CreateButton(string name, string text)
    {
        var button = new Button();
        button.name = name;
        button.text = text;
        button.style.width = 240;
        button.style.backgroundColor = Color.white;
        button.style.unityFontStyleAndWeight = FontStyle.Bold;
        return button;
    }

    private Label CreateLabel(string name, string content)
    {
        var label = new Label();
        label.name = name;
        label.text = content;
        label.style.color = Color.black;
        label.style.fontSize = 18;
        return label;
    }

    void UpdateUI()
    {
        if (NetworkManager.Singleton == null)
        {
            SetStartButtons(false);
            SetStatusText("NetworkManager not found");
            return;
        }

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            SetStartButtons(true);
            SetStatusText("Not connected");
        }
        else
        {
            SetStartButtons(false);
            UpdateStatusLabels();
        }
    }

    void SetStartButtons(bool state)
    {
        hostButton.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
        clientButton.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
        //serverButton.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
    }

    void SetStatusText(string text) => statusLabel.text = text;

    void UpdateStatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";
        string transport = "Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name;
        string modeText = "Mode: " + mode;
        SetStatusText($"{transport}\n{modeText}");
    }
}
