namespace Deveknife.Blades.RecodeMule
{
    using System;

    internal interface INotifyWatcherQueueChanged
    {
        event EventHandler<Recoder.WatcherQueueChangedEventArgs> QueueChanged;
    }
}