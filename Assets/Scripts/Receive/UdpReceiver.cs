using System;
using System.Net;
using System.Net.Sockets;

namespace Ac.At.FhStp.UnityUDPDemo.Receive
{

    public class UdpReceiver : IDisposable
    {

        private readonly UdpClient client;


        public UdpReceiver(IPAddress remoteAddress, int port)
        {
            var endPoint = new IPEndPoint(remoteAddress, port);
            client = new UdpClient(endPoint);

            StartReceiving();
        }


        public void Dispose() =>
            client.Dispose();

        private void StartReceiving() =>
            client.BeginReceive(res =>
            {
                IPEndPoint receivedEndpoint = null;
                var packet = client.EndReceive(res, ref receivedEndpoint);
                OnPacketReceived?.Invoke(packet);
                StartReceiving();
            }, null);


        public event Action<byte[]> OnPacketReceived;

    }

}