using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Wow
{
    /// <summary>
    /// Provides data for ItemChanged event.
    /// </summary>
    public class ItemChangedEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// Index of item that was changed.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Initializes a new instance of the ItemChangedEventArgs class.
        /// </summary>
        /// <param name="index">Index of changed item.</param>
        /// <param name="propertyName">Name of changed property in item.</param>
        public ItemChangedEventArgs(int index, string propertyName)
            : base(propertyName)
        {
            Index = index;
        }
    }
}
