using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Ac.At.FhStp.UnityUDPDemo.Receive
{

    public class LocationReceiver : MonoBehaviour
    {

        [SerializeField] private int port;
        [SerializeField] private UnityEvent<string> onPacketReceived;
        

        private void OnEnable() =>
            StartListening();

        private void StartListening()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, port);
            var client = new UdpClient(endPoint);

            client.BeginReceive(res =>
            {
                var bytes = client.EndReceive(res, ref endPoint);
                var packet = Encoding.ASCII.GetString(bytes);
                OnPacketReceived(packet);
            }, null);
        }

        private void OnPacketReceived(string packet)
        {
            onPacketReceived.Invoke(packet);
        }

    }

}