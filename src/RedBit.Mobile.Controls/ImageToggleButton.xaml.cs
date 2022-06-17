using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedBit.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageToggleButton : ContentView
    {
        readonly TapGestureRecognizer _tap;
        public static readonly BindableProperty ToggledOnImageProperty = BindableProperty.Create(nameof(ToggledOnImage), typeof(ImageSource), typeof(ImageToggleButton), propertyChanged: ToggledOnImagePropertyChanged, defaultValue: null);
        public static readonly BindableProperty ToggledOffImageProperty = BindableProperty.Create(nameof(ToggledOffImage), typeof(ImageSource), typeof(ImageToggleButton), propertyChanged: ToggledOffImagePropertyChanged, defaultValue: null);
        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(ImageToggleButton), propertyChanged: IsToggledPropertyChanged, defaultValue: false, defaultBindingMode: BindingMode.TwoWay);

        private static void ToggledOnImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ImageToggleButton)bindable).UpdateView();
        }
        private static void ToggledOffImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ImageToggleButton)bindable).UpdateView();
        }
        private static void IsToggledPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ImageToggleButton)bindable).UpdateView();
        }

        public ImageToggleButton()
        {
            InitializeComponent();
            _tap = new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    IsToggled = !IsToggled;
                })
            };
            this.GestureRecognizers.Add(_tap);
            UpdateView();
        }

        public ImageSource ToggledOnImage
        {
            get => (ImageSource)GetValue(ToggledOnImageProperty);
            set => SetValue(ToggledOnImageProperty, value);
        }

        public ImageSource ToggledOffImage
        {
            get => (ImageSource)GetValue(ToggledOffImageProperty);
            set => SetValue(ToggledOffImageProperty, value);
        }

        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        public void UpdateView()
        {
            imgMain.Source = IsToggled ? ToggledOnImage : ToggledOffImage;
        }
    }
}

