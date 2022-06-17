using System;
using System.Collections.Generic;
using JLOFT.Mobile.ViewModels;
using Xamarin.Forms;

namespace JLOFT.Mobile.Views
{	
	public partial class LoginView : LoginViewXaml
	{	
		public LoginView ()
		{
			InitializeComponent ();
		}
	}

	public class LoginViewXaml : BaseContentPage<LoginViewModel> { }
}

