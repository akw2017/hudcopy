

namespace AIC.Core
{
    using System.ComponentModel;

    /// <summary>
    /// Property changed event args that are used when a property has changed. The event arguments contains both
    /// the original sender as the current sender of the event.
    /// <para />
    /// Best used in combination with <see cref="IAdvancedNotifyPropertyChanged"/>.
    /// </summary>
    public class AdvancedPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedPropertyChangedEventArgs"/>"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyName">Name of the property.</param>
        public AdvancedPropertyChangedEventArgs(object sender, string propertyName)
            : this(sender, propertyName, null, null, false, false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedPropertyChangedEventArgs"/>"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="newValue">The new value.</param>
        public AdvancedPropertyChangedEventArgs(object sender, string propertyName, object newValue)
            : this(sender, propertyName, null, newValue, false, true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedPropertyChangedEventArgs"/>"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public AdvancedPropertyChangedEventArgs(object sender, string propertyName, object oldValue, object newValue)
            : this(sender, propertyName, oldValue, newValue, true, true) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedPropertyChangedEventArgs"/>"/> class.
        /// </summary>
        /// <param name="originalSender">The original sender.</param>
        /// <param name="latestSender">The latest sender.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="isOldValueMeaningful">if set to <c>true</c>, the <paramref name="oldValue"/> has a meaningful value.</param>
        /// <param name="isNewValueMeaningful">if set to <c>true</c>, the <paramref name="newValue"/> has a meaningful value.</param>
        private AdvancedPropertyChangedEventArgs(object sender, string propertyName, object oldValue, object newValue,
            bool isOldValueMeaningful, bool isNewValueMeaningful)
            : base(propertyName)
        {
            Sender = sender;
            OldValue = oldValue;
            NewValue = newValue;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Sender.
        /// </summary>
        /// <value>The Sender.</value>
        public object Sender { get; private set; }

        /// <summary>
        /// Gets the old value.
        /// </summary>
        /// <value>The old value.</value>
        public object OldValue { get; private set; }

        /// <summary>
        /// Gets the new value.
        /// </summary>
        /// <value>The new value.</value>
        public object NewValue { get; private set; }

        #endregion
    }
}
