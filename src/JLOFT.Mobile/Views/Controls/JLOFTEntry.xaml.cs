using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JLOFT.Mobile.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JLOFTEntry : FormControl
    {
        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(nameof(EntryText), typeof(string), typeof(JLOFTEntry), defaultBindingMode: BindingMode.TwoWay, defaultValue: null);

        public static readonly BindableProperty EntryTextColorProperty = BindableProperty.Create(nameof(EntryTextColor), typeof(Color), typeof(JLOFTEntry), Color.Black);

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(JLOFTEntry));

        public static readonly BindableProperty EntryImageSourceProperty = BindableProperty.Create(nameof(EntryImageSource), typeof(ImageSource), typeof(JLOFTEntry));

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(JLOFTEntry), defaultValue: Keyboard.Default, coerceValue: (o, v) => (Keyboard)v ?? Keyboard.Default);

        public static readonly BindableProperty EntryImageCommandProperty = BindableProperty.Create(nameof(EntryImageCommand), typeof(ICommand), typeof(JLOFTEntry));
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
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        public ImageSource EntryImageSource
        {
            get => (ImageSource)GetValue(EntryImageSourceProperty);
            set => SetValue(EntryImageSourceProperty, value);
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public Command EntryImageCommand
        {
            get => (Command)GetValue(EntryImageCommandProperty);
            set => SetValue(EntryImageCommandProperty, value);
        }

        public JLOFTEntry()
        {
            InitializeComponent();
        }

        public IList<Behavior> EntryBehaviors
        {
            get => entry.Behaviors;
        }

        public Entry Entry => entry;

        protected override View FormControlView => formControlView;
    }
}

