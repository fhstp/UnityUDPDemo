using System;
using System.Threading;

namespace Ac.At.FhStp.UnityUDPDemo
{

    public static class MainThread
    {

        private static SynchronizationContext context;

        public static void Initialize() => 
            context = SynchronizationContext.Current;

        public static void Run(Action action) =>
            context.Post(_ => action(), null);

    }

}