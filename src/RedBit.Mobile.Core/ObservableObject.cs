using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RedBit.Mobile.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when property changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableObject()
        {
            PropertyChanged = null;
        }

        /// <summary>
        /// Sets the property
        /// </summary>
        /// <typeparam name="T">The 1st type parameter</typeparam>
        /// <param name="backingStore">Backing store</param>
        /// <param name="value">Value</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="onChanged">On changed</param>
        /// <param name="forcedPropertyChanged">Force property changed</param>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, bool forcedPropertyChanged = false)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value) && !forcedPropertyChanged)
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the property changed event
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

