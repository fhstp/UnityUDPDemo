using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Ac.At.FhStp.UnityUDPDemo.Receive
{

    public class LocationReceiver : MonoBehaviour
    {

        [SerializeField] private int port;
        [SerializeField] private UnityEvent<string> onPacketReceived;

        private UdpReceiver receiver;


        private void OnEnable()
        {
            receiver = new UdpReceiver(IPAddress.Any, port);
            receiver.OnPacketReceived += OnPacketReceived;
        }

        private void OnDisable()
        {
            receiver.Dispose();
            receiver = null;
        }
        
        private void OnPacketReceived(byte[] packet)
        {
            var stringContent = Encoding.ASCII.GetString(packet);
            onPacketReceived.Invoke(stringContent);
        }

    }

}