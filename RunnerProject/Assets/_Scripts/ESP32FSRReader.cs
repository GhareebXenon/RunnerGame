using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ESP32UdpReceiver : MonoBehaviour
{
    public int port = 3333;

    UdpClient udp;
    IPEndPoint remoteEP;

    int stepCount;

    void Start()
    {
        udp = new UdpClient(port);
        remoteEP = new IPEndPoint(IPAddress.Any, port);
        udp.BeginReceive(OnReceive, null);
    }

    void OnReceive(System.IAsyncResult ar)
    {
        byte[] data = udp.EndReceive(ar, ref remoteEP);
        string msg = Encoding.ASCII.GetString(data);

        if (msg.Contains("STEP"))
        {
            stepCount++;
        }

        udp.BeginReceive(OnReceive, null);
    }

    // Unity-side consumption (IMPORTANT)
    public bool ConsumeStep()
    {
        if (stepCount > 0)
        {
            stepCount--;
            return true;
        }
        return false;
    }

    void OnDestroy()
    {
        udp?.Close();
    }
}