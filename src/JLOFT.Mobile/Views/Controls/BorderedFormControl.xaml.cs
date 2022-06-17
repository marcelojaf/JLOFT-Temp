using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JLOFT.Mobile.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BorderedFormControl : ContentView
    {
        public static readonly BindableProperty InnerContentProperty = BindableProperty.Create(nameof(InnerContent), typeof(View), typeof(BorderedFormControl), propertyChanged: OnInnerContentPropertyChanged);

        public static readonly BindableProperty UseUniformHeightProperty = BindableProperty.Create(nameof(UseUniformHeight), typeof(bool), typeof(BorderedFormControl), propertyChanged: OnUseUniformHeightPropertyChanged);

        private static void OnUseUniformHeightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BorderedFormControl targetView = (BorderedFormControl)bindable;
            targetView.HandleUniformHeight();
        }

        private static void OnInnerContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BorderedFormControl targetView = (BorderedFormControl)bindable;
            View val = (View)newValue;
            val.VerticalOptions = LayoutOptions.Center;
            targetView.BorderFrame.Content = val;
        }

        public View InnerContent
        {
            get => (View)GetValue(InnerContentProperty);
            set => SetValue(InnerContentProperty, value);
        }

        public bool UseUniformHeight
        {
            get => (bool)GetValue(UseUniformHeightProperty);
            set => SetValue(UseUniformHeightProperty, value);
        }

        public BorderedFormControl()
        {
            InitializeComponent();
        }

        private void HandleUniformHeight()
        {
            if (UseUniformHeight)
            {
                BorderFrame.Padding = new Thickness(10, 0);
                BorderFrame.HeightRequest = 60;
            }
        }
    }
}

