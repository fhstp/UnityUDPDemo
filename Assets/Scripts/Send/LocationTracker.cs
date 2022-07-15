using System;
using System.Collections;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif
using UnityEngine.Events;

namespace Ac.At.FhStp.UnityUDPDemo.Send
{

    public class LocationTracker : MonoBehaviour
    {

        [SerializeField] private UnityEvent<LocationTrackerStatus> onStatusChanged;
        [SerializeField] private UnityEvent<LocationInfo> onLocationChanged;

        private LocationInfo lastLocation;
        private LocationTrackerStatus status = LocationTrackerStatus.Disabled;


        public LocationTrackerStatus Status
        {
            get => status;
            private set
            {
                if (value != status)
                {
                    status = value;
                    onStatusChanged.Invoke(status);
                }
            }
        }

        private static bool HasPermission
        {
            get
            {
#if UNITY_EDITOR
                return true;
#elif UNITY_ANDROID
                return UnityEngine.Input.location.isEnabledByUser;
#else
                return UnityEngine.Input.location.isEnabledByUser;
#endif
            }
        }

        private static LocationServiceStatus ServiceStatus => Input.location.status;

        private static bool ServiceIsRunning => ServiceStatus == LocationServiceStatus.Running;


        private void Update()
        {
            if (ServiceIsRunning)
                PublishLocationIfChanged();
        }

        private void OnEnable() =>
            ActivateTracking();

        private void OnDisable() =>
            DeactivateTracking();

        private void PublishLocationIfChanged()
        {
            var location = Input.location.lastData;

            if (!location.Equals(lastLocation))
                onLocationChanged.Invoke(location);
        }

        private void RequestPermission()
        {
#if UNITY_ANDROID

            var callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += _ => ActivateTracking();
            callbacks.PermissionDenied += _ => { Status = LocationTrackerStatus.Denied; };
            
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                Permission.RequestUserPermission(Permission.FineLocation, callbacks);
#endif
        }

        private void ActivateTracking()
        {
            IEnumerator Routine()
            {
                Status = LocationTrackerStatus.Initializing;

                if (!HasPermission)
                {
                    RequestPermission();
                    yield break;
                }

                Input.location.Start(1, 1);
                yield return this.WaitUntil(() => ServiceIsRunning, TimeSpan.FromSeconds(10));

                if (!ServiceIsRunning)
                {
                    Status = LocationTrackerStatus.CouldNotStart;
                    yield break;
                }

                Status = LocationTrackerStatus.Active;
                PublishLocationIfChanged();
            }

            StartCoroutine(Routine());
        }

        private void DeactivateTracking()
        {
            Input.location.Stop();
            Status = LocationTrackerStatus.Disabled;
        }

    }

    public enum LocationTrackerStatus
    {

        Initializing,
        Active,
        Denied,
        CouldNotStart,
        Disabled

    }

}