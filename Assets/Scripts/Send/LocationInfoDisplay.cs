using TMPro;
using UnityEngine;

namespace Ac.At.FhStp.UnityUDPDemo.Send
{

    public class LocationInfoDisplay : MonoBehaviour
    {

        [SerializeField] private GameObject spinnerGameObject;
        [SerializeField] private TextMeshProUGUI statusTextView;


        private bool ShowSpinner
        {
            set => spinnerGameObject.SetActive(value);
        }

        private string StatusText
        {
            set => statusTextView.text = value;
        }


        public void OnStatusChanged(LocationTrackerStatus status)
        {
            ShowSpinner = status == LocationTrackerStatus.Initializing;
            StatusText =
                status switch
                {
                    LocationTrackerStatus.Initializing => "Location-tracker initializing...",
                    LocationTrackerStatus.Denied => "Location is required",
                    LocationTrackerStatus.CouldNotStart => "Could not start location-tracker",
                    _ => ""
                };
        }

        public void OnLocationChanged(LocationInfo location) =>
            StatusText = $"Lat: {location.latitude} - Lng: {location.longitude}";

    }

}