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

    // 🔹 Accessible from other scripts
    public static int encoder1Value;
    public static int encoder2Value;

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
            catch { }
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

            ParseEncoders(lineCopy);
        }
    }

    void ParseEncoders(string line)
    {
        // Expected format:
        // Enc1 Dir: cw Rotation: 10
        // Enc2 Dir: ccw Rotation: 5

        if (line.StartsWith("Enc1"))
        {
            int value = ExtractRotation(line);
            encoder1Value = value;
        }
        else if (line.StartsWith("Enc2"))
        {
            int value = ExtractRotation(line);
            encoder2Value = value;
        }
    }

    int ExtractRotation(string line)
    {
        int index = line.LastIndexOf("Rotation:");
        if (index >= 0)
        {
            string number = line.Substring(index + 9).Trim();
            int.TryParse(number, out int result);
            return result;
        }
        return 0;
    }

    void OnApplicationQuit() => Shutdown();
    void OnDestroy() => Shutdown();

    void Shutdown()
    {
        running = false;

        if (readThread != null && readThread.IsAlive)
            readThread.Join(200);

        if (serial != null && serial.IsOpen)
            serial.Close();
    }
}