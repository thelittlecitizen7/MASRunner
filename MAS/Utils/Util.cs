using MAS.Auctions;
using System;
using System.Linq;

namespace MAS.Utils
{
    public static class Util
    {
        public static void RunAllEventInParallel(Delegate delegates, IAuction auction)
        {
            object obj = auction;
            delegates.GetInvocationList().AsParallel().ForAll(a => a?.DynamicInvoke(obj));
        }
    }
}
