using TMPro;
using UnityEngine;

namespace Ac.At.FhStp.UnityUDPDemo.Send
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SendInfoText : FadeText
    {

        protected override void Awake()
        {
            base.Awake();
            Clear();
        }

        public void OnSentBytes(int byteCount) =>
            Display($"OK! Sent {byteCount} bytes.");

    }

}