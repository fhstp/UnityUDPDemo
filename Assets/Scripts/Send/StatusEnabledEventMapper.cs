namespace Ac.At.FhStp.UnityUDPDemo.Send
{

    public class StatusEnabledEventMapper : EventMapper<LocationTrackerStatus, bool>
    {

        protected override bool Map(LocationTrackerStatus value) =>
            value == LocationTrackerStatus.Active;

    }

}