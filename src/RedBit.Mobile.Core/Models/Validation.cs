using System;
using Xamarin.Forms;

namespace RedBit.Mobile.Core.Models
{
	public class Validation : ObservableObject
	{
		public Validation()
		{
		}

        private string _Message = "";

        /// <summary>
        /// Sets and gets the ValidationErrorMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Message
        {
            get => _Message;
            set => SetProperty(ref _Message, value);
        }

        private bool _IsValid = false;

        /// <summary>
        /// Sets and gets the IsValid property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsValid
        {
            get => _IsValid;
            set => SetProperty(ref _IsValid, value);
        }

        private Color _Color = Color.Red;

        /// <summary>
        /// Sets and gets the Color property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Color Color
        {
            get => _Color;
            set => SetProperty(ref _Color, value);
        }
    }
}

