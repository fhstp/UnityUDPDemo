using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ComradeVanti.CSharpTools;
using UnityEngine;
using UnityEngine.Events;

namespace Ac.At.FhStp.UnityUDPDemo.Send
{

    public class LocationSender : MonoBehaviour
    {

        [SerializeField] private UnityEvent<bool> onCanSendChanged;
        [SerializeField] private UnityEvent<string> onInfo;
        [SerializeField] private int port;

        private Opt<LocationInfo> currentLocation = Opt.None<LocationInfo>();
        private Opt<IPAddress> targetIP = Opt.None<IPAddress>();
        private UdpClient client;


        private bool CanSend =>
            currentLocation.IsSome() && targetIP.IsSome();

        private Opt<string> LocationString =>
            currentLocation.Map(it => $"Lat: {it.latitude}, Lng: {it.longitude}");

        private Opt<byte[]> LocationBytes =>
            LocationString.Map(Encoding.ASCII.GetBytes);

        private Opt<IPEndPoint> EndPoint =>
            targetIP.Map(it => new IPEndPoint(it, port));


        private void OnEnable() =>
            client = new UdpClient(port);

        private void OnDisable()
        {
            client.Dispose();
            client = null;
        }

        public void OnLocationChanged(LocationInfo location)
        {
            currentLocation = Opt.Some(location);
            onCanSendChanged.Invoke(CanSend);
        }

        public void OnIpInputChanged(string input)
        {
            targetIP = IPAddress.TryParse(input, out var ip)
                ? Opt.Some(ip)
                : Opt.None<IPAddress>();
            onCanSendChanged.Invoke(CanSend);
        }

        public void OnSendButtonClicked() =>
            TrySendLocation();

        private void TrySendLocation()
        {
            var bytes = LocationBytes.Get();
            var endPoint = EndPoint.Get();

            SendBytesTo(bytes, endPoint);
        }

        private void SendBytesTo(byte[] bytes, IPEndPoint endPoint)
        {
            try
            {
                client.Send(bytes, bytes.Length, endPoint);
                onInfo.Invoke($"OK! Sent {bytes.Length} bytes.");
            }
            catch (Exception e)
            {
                onInfo.Invoke($"Uh-oh could not send: {e.Message}");
            }
        }

    }

}