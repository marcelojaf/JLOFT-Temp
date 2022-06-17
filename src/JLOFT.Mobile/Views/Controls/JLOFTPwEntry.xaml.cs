using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JLOFT.Mobile.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JLOFTPwEntry : FormControl
	{
        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create("EntryText", typeof(string), typeof(JLOFTEntry), defaultBindingMode: BindingMode.TwoWay, defaultValue: null);

        public static readonly BindableProperty HidePasswordProperty = BindableProperty.Create("HidePassword", typeof(bool), typeof(JLOFTEntry), defaultBindingMode: BindingMode.TwoWay, defaultValue: true);

        public static readonly BindableProperty EntryTextColorProperty = BindableProperty.Create("EntryTextColor", typeof(Color), typeof(JLOFTEntry), Color.Black);

        public static readonly BindableProperty ToggledOnImageProperty = BindableProperty.Create(nameof(ToggledOnImage), typeof(ImageSource), typeof(JLOFTEntry));

        public static readonly BindableProperty ToggledOffImageProperty = BindableProperty.Create(nameof(ToggledOffImage), typeof(ImageSource), typeof(JLOFTEntry));

        public string EntryText
        {
            get => (string)GetValue(EntryTextProperty);
            set => SetValue(EntryTextProperty, value);
        }

        public Color EntryTextColor
        {
            get => (Color)GetValue(EntryTextColorProperty);
            set => SetValue(EntryTextColorProperty, value);
        }

        public bool HidePassword
        {
            get => (bool)GetValue(HidePasswordProperty);
            set => SetValue(HidePasswordProperty, value);
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

        public JLOFTPwEntry()
        {
            InitializeComponent();
        }

        protected override View FormControlView => formControlView;
    }
}

