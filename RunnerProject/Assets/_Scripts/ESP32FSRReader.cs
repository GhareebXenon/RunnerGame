using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ESP32Receiver : MonoBehaviour
{
    [Header("Network Settings")]
    public string esp32IP = "192.168.1.24";
    public int receivePort = 8889;
    public int sendPort = 8888;

    [Header("Status")]
    public bool isConnected = false;
    public float currentSpeed = 0f;
    public int lastFoot = 0;  // 1 = Right, 2 = Left

    [Header("Events")]
    public UnityEngine.Events.UnityEvent<int> OnStep;
    public UnityEngine.Events.UnityEvent<float> OnSpeedUpdate;

    private UdpClient udpClient;
    private Thread receiveThread;
    private bool isRunning = false;

    // Thread-safe message queue
    private readonly object lockObject = new object();
    private string latestMessage = "";
    private bool hasNewMessage = false;

    void Start()
    {
        StartReceiving();

        // Send connect message to ESP32
        Invoke("SendConnectMessage", 1f);
    }

    void SendConnectMessage()
    {
        SendToESP32("CONNECT");
    }

    void StartReceiving()
    {
        try
        {
            udpClient = new UdpClient(receivePort);
            isRunning = true;

            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();

            Debug.Log($"UDP Receiver started on port {receivePort}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to start UDP receiver: {e.Message}");
        }
    }

    void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        while (isRunning)
        {
            try
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data);

                lock (lockObject)
                {
                    latestMessage = message;
                    hasNewMessage = true;
                }
            }
            catch (SocketException)
            {
                // Socket closed, exit thread
                break;
            }
            catch (Exception e)
            {
                Debug.LogError($"Receive error: {e.Message}");
            }
        }
    }

    void Update()
    {
        // Process messages on main thread
        lock (lockObject)
        {
            if (hasNewMessage)
            {
                ProcessMessage(latestMessage);
                hasNewMessage = false;
            }
        }
    }

    void ProcessMessage(string message)
    {
        if (string.IsNullOrEmpty(message)) return;

        // Parse Step message: "Step:1" or "Step:2"
        if (message.StartsWith("Step:"))
        {
            string footStr = message.Substring(5);
            if (int.TryParse(footStr, out int foot))
            {
                lastFoot = foot;
                OnStep?.Invoke(foot);
                Debug.Log($"Step detected - Foot: {(foot == 1 ? "Right" : "Left")}");
            }
        }
        // Parse Speed message: "Speed:2.50"
        else if (message.StartsWith("Speed:"))
        {
            string speedStr = message.Substring(6);
            if (float.TryParse(speedStr, out float speed))
            {
                currentSpeed = speed;
                OnSpeedUpdate?.Invoke(speed);
            }
        }
        // Connection confirmation
        else if (message == "CONNECTED")
        {
            isConnected = true;
            Debug.Log("Connected to ESP32!");
        }
    }

    public void SendToESP32(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            UdpClient sendClient = new UdpClient();
            sendClient.Send(data, data.Length, esp32IP, sendPort);
            sendClient.Close();
            Debug.Log($"Sent to ESP32: {message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Send error: {e.Message}");
        }
    }

    void OnDestroy()
    {
        isRunning = false;

        if (udpClient != null)
        {
            udpClient.Close();
        }

        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
    }

    void OnApplicationQuit()
    {
        OnDestroy();
    }
}