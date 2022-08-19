using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lava
{
    class HostFakeDevice
    {
        static int port = 8005; // порт для приема входящих запросов

        public static EventHandler<BoxControllerArgs> FakeBoxControllerValuesUpdate;


        public static void StartThread()
        {
           HostFakeDevice host = new HostFakeDevice();
           Thread newThread = new Thread(host.StartHost);
           newThread.Start();
        }

        private void StartHost()
        {
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);

                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    string dataString = builder.ToString();
                    char[] arrayChar = dataString.ToArray();
                    byte[] myBytes = new byte[arrayChar.Length];

                    for (int i = 0; i < arrayChar.Length; i++)
                    {
                        myBytes[i] = byte.Parse(arrayChar[i].ToString());
                    }

                    //FakeBoxControllerValuesUpdate.Invoke(this, ConvertBoxValues(myBytes));

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /*private static BoxControllerArgs ConvertBoxValues(byte[] data)
        {
            List<byte[]> valuesBytesBoxController = new List<byte[]>();

            for (int i = 0; i < data.Length / 2 - 1; i++)
            {
                valuesBytesBoxController.Add(data.Skip(2 * i + 1).Take(2).ToArray());
            }

            valuesBytesBoxController.Add(data.Skip(data.Length - 2).Take(1).ToArray());
            valuesBytesBoxController.Add(data.Skip(data.Length - 1).Take(1).ToArray());

            return new BoxControllerArgs(valuesBytesBoxController.Take(7).ToList(), valuesBytesBoxController.Skip(7).Take(1).ToList(), valuesBytesBoxController.Skip(8).Take(1).ToList());
        }*/
    }
}
