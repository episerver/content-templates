using System;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Scheduler;

namespace MyAppNamespace
{
    [ScheduledPlugIn(
        DisplayName = "MyScheduledJob",
        Description = "",
        GUID = "3F7881B2-2451-4941-BB7C-5FAB9A6A799F",
        IntervalType = ScheduledIntervalType.Hours,
        IntervalLength = 12,
        DefaultEnabled = true,
        Restartable = true)]
    public class MyScheduledJob : ScheduledJobBase
    {
        private bool _stopSignaled;

        public MyScheduledJob()
        {
            IsStoppable = true;
        }

        /// <summary>
        /// Called when a scheduled job executes
        /// </summary>
        /// <returns>A status message to be stored in the database log and visible from admin mode</returns>
        public override string Execute()
        {
            //Call OnStatusChanged to periodically notify progress of job for manually started jobs
            OnStatusChanged($"Starting execution of {GetType()}");

            //Add implementation

            //For long running jobs periodically check if stop is signaled and if so stop execution
            if (_stopSignaled)
            {
                return "Stop of job was called";
            }

            return "Change to message that describes outcome of execution";
        }

        /// <summary>
        /// Called when a user clicks on Stop for a manually started job, or when ASP.NET shuts down.
        /// </summary>
        public override void Stop()
        {
            _stopSignaled = true;
        }
    }
}
