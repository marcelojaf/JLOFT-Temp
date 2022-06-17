using RedBit.Mobile.Core.Models;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedBit.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialEntry : ContentView
    {
        public static void Init() { }
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialEntry), defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newval) =>
            {
                var matEntry = (MaterialEntry)bindable;
                var val = (string)newval;
                matEntry.SetValueFromPropertyChange();

            });

        private void SetValueFromPropertyChange()
        {
            EntryField.Text = Text;

            // if there is text that is being set and we are not focused, make sure the hidden label is visible
            if (!EntryField.IsFocused && !string.IsNullOrEmpty(Text))
            {
                if (EntryField.Height == -1 || EntryField.Y == 0)
                {
                    // the field has not been layed out yet so wait a bit then try again
                    Task.Factory.StartNew(async () =>
                    {
                        await Task.Delay(100);
                        Device.BeginInvokeOnMainThread(SetValueFromPropertyChange);
                    });
                }
                else
                {
                    EntryField.PlaceholderColor = HiddenLabel.TextColor = UnfocusedColor;
                    HiddenLabel.IsVisible = true;
                    HiddenLabel.TranslationY = (EntryField.Y - EntryField.Height) + HiddenLabelMargin;
                    EntryField.Placeholder = null;
                }
            }
        }
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialEntry), defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newval) =>
            {
                var matEntry = (MaterialEntry)bindable;
                matEntry.EntryField.Placeholder = (string)newval;
                matEntry.HiddenLabel.Text = (string)newval;
            });

        public static BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialEntry), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.IsPassword = (bool)newVal;
            if (matEntry.EntryField.IsFocused)
            {
                matEntry.EntryField.CursorPosition = matEntry.EntryField.Text.Length;
            }
        });
        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialEntry), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialEntry)bindable;
            matEntry.EntryField.Keyboard = (Keyboard)newVal;
        });
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialEntry), defaultValue: Color.Accent);

        public static BindableProperty ReturnTypeProperty = BindableProperty.Create(
            propertyName: "ReturnType",
            returnType: typeof(ReturnType),
            declaringType: typeof(MaterialEntry),
            defaultValue: default(ReturnType),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: HandleReturnTypePropertyChanged);
        private static void HandleReturnTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MaterialEntry targetView = (MaterialEntry)bindable;
            ReturnType val = (ReturnType)newValue;
            // Set your data here
            targetView.EntryField.ReturnType = val;
        }

        public static BindableProperty ReturnCommandProperty = BindableProperty.Create(
            propertyName: "ReturnCommand",
            returnType: typeof(Command),
            declaringType: typeof(MaterialEntry),
            defaultValue: default(Command),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: HandleReturnCommandPropertyChanged);

        private static void HandleReturnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MaterialEntry targetView = (MaterialEntry)bindable;
            Command val = (Command)newValue;
            targetView.EntryField.ReturnCommand = val;
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: "TextColor",
            returnType: typeof(Color),
            declaringType: typeof(MaterialEntry),
            defaultValue: default(Color),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: HandleTextColorPropertyChanged);
        private static void HandleTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MaterialEntry targetView = (MaterialEntry)bindable;
            Color val = (Color)newValue;
            targetView.EntryField.PlaceholderColor = targetView.EntryField.TextColor = val;
        }

        private double _scale = 1;

        public static BindableProperty IsValidProperty = BindableProperty.Create(
            propertyName: nameof(IsValid),
            returnType: typeof(bool),
            declaringType: typeof(MaterialEntry),
            defaultValue: true,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: HandleIsValidPropertyChanged);

        private static void HandleIsValidPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MaterialEntry targetView = (MaterialEntry)bindable;
            bool val = (bool)newValue;
            // TODO - need to set the validation text
        }

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        public MaterialEntry()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            EntryField.PlaceholderColor = BottomBorder.BackgroundColor = HiddenBottomBorder.BackgroundColor = UnfocusedColor;
            ListenForFocus();
            _scale = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
        }

        private void ListenForFocus()
        {
            EntryField.Focused += EntryFieldFocused;
            EntryField.Unfocused += EntryFieldUnfocused;
        }
        private void StopListenForFocus()
        {
            EntryField.Focused -= EntryFieldFocused;
            EntryField.Unfocused -= EntryFieldUnfocused;
        }

        private async void EntryFieldFocused(object sender = null, EventArgs e = null)
        {
            StopListenForFocus();
            HiddenBottomBorder.BackgroundColor = AccentColor;
            HiddenLabel.TextColor = AccentColor;
            HiddenLabel.IsVisible = true;
            if (string.IsNullOrEmpty(EntryField.Text))
            {

                // animate both at the same time
                await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(1, 60),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y - EntryField.Height + HiddenLabelMargin, 200, Easing.BounceIn)
                 );
                EntryField.Placeholder = null;
                EntryField.IsPassword = IsPassword;
            }
            else
            {
                await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200);
                EntryField.CursorPosition = EntryField.Text.Length;
            }
            FocusedCallBack?.Invoke();
            ListenForFocus();
        }

        private double _HiddenLabelMargin = 10;
        private double HiddenLabelMargin
        {
            get
            {
                if (_HiddenLabelMargin == -1)
                {
                    _HiddenLabelMargin = 4;
                    if (Device.RuntimePlatform == Device.iOS)
                        _HiddenLabelMargin = 0;// _scale <= 2 ? 6 : 0;
                }
                return _HiddenLabelMargin;
            }
        }

        private async void EntryFieldUnfocused(object sender = null, EventArgs e = null)
        {
            StopListenForFocus();
            HiddenLabel.TextColor = this.UnfocusedColor;
            if (string.IsNullOrEmpty(EntryField.Text))
            {
                // animate both at the same time
                await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(0, 180),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y, 200, Easing.BounceIn)
                 );
                EntryField.Placeholder = Placeholder;
                EntryField.IsPassword = false;
            }
            else
            {
                await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
            }

            UnfocusedCallBack?.Invoke();
            ListenForFocus();
        }

        public static BindableProperty ValidationProperty = BindableProperty.Create(
            propertyName: nameof(Validation),
            returnType: typeof(Validation),
            declaringType: typeof(MaterialEntry),
            defaultValue: default(Validation),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: HandleValidationPropertyChanged);

        private static void HandleValidationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

            MaterialEntry targetView = (MaterialEntry)bindable;
            Validation val = (Validation)newValue;
            if (newValue == null || val.IsValid)
                targetView.RemoveValidationLabel();
            else
                targetView.AddValidationLabel();
        }

        public Validation Validation
        {
            get { return (Validation)base.GetValue(ValidationProperty); }
            set { SetValue(ValidationProperty, value); }
        }

        public Color AccentColor
        {
            get
            {
                return (Color)GetValue(AccentColorProperty);
            }
            set
            {
                SetValue(AccentColorProperty, value);
            }
        }

        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)GetValue(KeyboardProperty);
            }
            set
            {
                SetValue(KeyboardProperty, value);
            }
        }

        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }
            set
            {
                SetValue(IsPasswordProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        public ReturnType ReturnType
        {
            get { return (ReturnType)base.GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public Command ReturnCommand
        {
            get { return (Command)base.GetValue(ReturnCommandProperty); }
            set { SetValue(ReturnCommandProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)base.GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static BindableProperty UnfocusedColorProperty = BindableProperty.Create(
            propertyName: nameof(UnfocusedColor),
            returnType: typeof(Color),
            declaringType: typeof(MaterialEntry),
            defaultValue: Color.Gray,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: HandleUnfocusedColorPropertyChanged);

        private static void HandleUnfocusedColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MaterialEntry targetView = (MaterialEntry)bindable;
            Color val = (Color)newValue;
            if (!targetView.EntryField.IsFocused)
            {
                targetView.EntryField.PlaceholderColor
                    = targetView.BottomBorder.BackgroundColor
                    = targetView.HiddenBottomBorder.BackgroundColor
                    = targetView.HiddenLabel.TextColor = val;
            }
        }

        public Color UnfocusedColor
        {
            get { return (Color)base.GetValue(UnfocusedColorProperty); }
            set { SetValue(UnfocusedColorProperty, value); }
        }

        /// <summary>
        /// Focus on the field
        /// </summary>
        public new void Focus() => EntryField.Focus();

        public Entry Entry { get => EntryField; }

        private Action FocusedCallBack;
        public void FocusedCommand(Action callback)
        {
            FocusedCallBack = callback;
        }
        private Action UnfocusedCallBack;
        public void UnfocusedCommand(Action callback)
        {
            UnfocusedCallBack = callback;
        }
        public void ClearFocusedCommands()
        {
            StopListenForFocus();
        }

        private Label _validationLabel = null;
        private void AddValidationLabel()
        {

            if (_validationLabel == null)
            {
                _validationLabel = new Label
                {
                    Text = Validation.Message,
                    TextColor = Validation.Color,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label))
                };
                Grid.SetRow(_validationLabel, 2);
                container.Children.Add(_validationLabel);
            }
            else
            {
                _validationLabel.Text = Validation.Message;
                BottomBorder.Color = Validation.Color;
            }
        }

        private void RemoveValidationLabel()
        {
            if (_validationLabel != null)
            {
                container.Children.Remove(_validationLabel);
                _validationLabel = null;
                BottomBorder.Color = UnfocusedColor;
            }
        }
    }
}