using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;

public class MindWaveConector : MonoBehaviour {

    private TcpClient client;
    private Stream stream;
    private byte[] buffer;
    public UnityEngine.UI.Slider progressAtencao;
    public UnityEngine.UI.Text textAtencao;
    private Thread thread1;
    private Mutex mutex;
    private string atencao;
    private float mediaAtencao;
    private int tickAtencao;


    // Use this for initialization
    void Start () {
        if (!IsInvoking("parserData"))
        {
            client = new TcpClient("127.0.0.1", 13854);
            stream = client.GetStream();
            buffer = new byte[1024];
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(@"{""enableRawOutput"": true, ""format"": ""Json""}");
            stream.Write(myWriteBuffer, 0, myWriteBuffer.Length);

            mutex = new Mutex();
            thread1 = new Thread(executarParserData) { Name = "Thread 1" };
            thread1.Start();
        }
	}
	
	// Update is called once per frame
	void Update () {
        progressAtencao.value = float.Parse(atencao);
        textAtencao.text = atencao + "%".Replace("," ,"");
	}


    public string getMediaAtencao()
    {
        float atencaoString = mediaAtencao / tickAtencao;
        return atencaoString.ToString();
    }


    public void executarParserData()
    {

        Debug.Log("Chegou aqui no método");
        //InvokeRepeating("parserData", 0.1f, 0.02f);
        while (true)
        {
            parserData();
        }
    }

    void parserData()
    {
        if (stream.CanRead)
        {
            try
            {
                stream.Read(buffer, 0, buffer.Length);

                string packetString = System.Text.ASCIIEncoding.ASCII.GetString(buffer);

                string[] packets = packetString.Split(new char[] {'\r'});

                //Debug.Log("Dado: " + buffer);

                foreach (string packet in packets)
                {
                    if (packet.Length != 0)
                    {
                        if (packet.Contains("eSense"))
                        {

                            Debug.Log("Impressão do json: " + packet.Substring(23, 2));
                            atencao = packet.Substring(23, 2);
                            mediaAtencao += float.Parse(atencao);
                            tickAtencao += 1;
                        }
                    }

                }

            } catch(System.Exception e)
                {
                    Debug.Log("Ocorreu erro: " + e);
                }

            }

        }
}
