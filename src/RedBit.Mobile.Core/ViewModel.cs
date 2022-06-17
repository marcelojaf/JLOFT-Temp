using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RedBit.Mobile.Core
{
	/// <summary>
    /// Base ViewModel
    /// </summary>
	public class ViewModel : ObservableObject
	{
		private string _title = string.Empty;
		private string _subtitle = string.Empty;
		private string _icon = string.Empty;
		private string _header = string.Empty;
		private string _footer = string.Empty;
		private bool _isBusy;
		private bool _isNotBusy;
		private bool _canLoadMore = true;
		private string _loadingMessage = "";

		/// <summary>
		/// Gets or sets the Title
		/// </summary>
		public string Title
        {
			get => _title;
			set => SetProperty(ref _title, value);
        }

		/// <summary>
        /// Gets or sets the Subtitle
        /// </summary>
		public string Subtitle
		{
			get => _subtitle;
			set => SetProperty(ref _subtitle, value);
		}

		/// <summary>
		/// Gets or sets the Icon
		/// </summary>
		public string Icon
        {
			get => _icon;
			set => SetProperty(ref _icon, value);
		}

		/// <summary>
		/// Gets or sets the Header
		/// </summary>
		public string Header
		{
			get => _header;
			set => SetProperty(ref _header, value);
		}

		/// <summary>
		/// Gets or sets the Footer
		/// </summary>
		public string Footer
		{
			get => _footer;
			set => SetProperty(ref _footer, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is busy
		/// </summary>
		/// <value><c>true</c> if this instance is busy; otherwise <c>false</c></value>
		public bool IsBusy
        {
			get => _isBusy;
            set
            {
				if (SetProperty(ref _isBusy, value))
					IsNotBusy = !_isBusy;
            }
        }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is not busy
		/// </summary>
		/// <value><c>true</c> if this instance is busy; otherwise <c>false</c></value>
		public bool IsNotBusy
		{
			get => _isNotBusy;
			set
			{
				if (SetProperty(ref _isNotBusy, value))
					IsBusy = !_isNotBusy;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance can load more
		/// </summary>
		/// <value><c>true</c> if this instance is busy; otherwise <c>false</c></value>
		public bool CanLoadMore
        {
			get => _canLoadMore;
			set => SetProperty(ref _canLoadMore, value);
        }

		/// <summary>
		/// Gets or sets a Loading Message
		/// </summary>
		public string LoadingMessage
		{
			get => _loadingMessage;
			set => SetProperty(ref _loadingMessage, value);
		}

		public virtual Task DisplayAlert(string title, string message)
        {
			return DisplayAlert(title, message, "OK");
        }

		public virtual Task DisplayAlert(string title, string message, string okText)
		{
			return Application.Current?.MainPage?.DisplayAlert(title, message, okText);
		}

		public virtual Task<bool> DisplayYesNo(string title, string message)
        {
			return DisplayYesNo(title, message, "Yes", "No");
        }

		public virtual Task<bool> DisplayYesNo(string title, string message, string yes, string no)
		{
			return Application.Current?.MainPage?.DisplayAlert(title, message, yes, no);
		}

		/// <summary>
        /// Gets the current navigation, but be careful it may be null
        /// </summary>
		public virtual INavigation Navigation
        {
            get
            {
				return Application.Current?.MainPage?.Navigation;
            }
        }

		/// <summary>
        /// To be used in the CTOR of a view to determine if navigation back button should be shown
        /// </summary>
		public bool ShowNavigationBackButton { get; set; } = true;

		/// <summary>
        /// To be used in the CTOR of a view to determine if navigation bar should be shown
        /// </summary>
		public bool ShowNavigationBar { get; set; } = true;

		/// <summary>
        /// To be used in the CTOR to determine if we should allow user to use back button (will close app on main page if allowed)
        /// </summary>
		public bool DisableBackButtonPressed { get; set; } = false;

		
    }
}

