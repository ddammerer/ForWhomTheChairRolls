using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class ArduinoSerialDebug : MonoBehaviour
{
    private SerialPort serial;
    private Thread readThread;
    private volatile bool running = false;
    private string latestLine = null;
    private readonly object dataLock = new object();

    void Start()
    {
        serial = new SerialPort("COM3", 9600);
        serial.ReadTimeout = 100;
        serial.NewLine = "\n";
        serial.DtrEnable = true;
        serial.RtsEnable = true;
        serial.Open();

        running = true;
        readThread = new Thread(SerialReadLoop);
        readThread.IsBackground = true;
        readThread.Start();
    }

    void SerialReadLoop()
    {
        while (running && serial != null && serial.IsOpen)
        {
            try
            {
                string line = serial.ReadLine(); 
                lock (dataLock)
                {
                    latestLine = line;
                }
            }
            catch
            {
                
            }
        }
    }

    void Update()
    {
        if (latestLine != null)
        {
            string lineCopy;
            lock (dataLock)
            {
                lineCopy = latestLine;
                latestLine = null;
            }

            Debug.Log("[ESP32] " + lineCopy);
            // process rotary data here
        }
    }

    void OnApplicationQuit()
    {
        Shutdown();
    }

    void OnDestroy()
    {
        Shutdown();
    }

    void Shutdown()
    {
        running = false;

        if (readThread != null && readThread.IsAlive)
            readThread.Join(200);
        if (serial != null && serial.IsOpen)
            serial.Close();
    }
}
