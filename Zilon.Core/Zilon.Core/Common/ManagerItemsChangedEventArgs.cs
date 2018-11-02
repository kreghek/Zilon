using System;

using JetBrains.Annotations;

namespace Zilon.Core.Common
{
    public sealed class ManagerItemsChangedEventArgs<TItem> : EventArgs
    {
        public ManagerItemsChangedEventArgs(params TItem[] items)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        [PublicAPI]
        public TItem[] Items { get; }
    }
}
