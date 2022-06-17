using RedBit.Mobile.Core.Models;
using Xamarin.Forms;

namespace JLOFT.Mobile.Views.Controls
{
    public class FormControl : ContentView
    {
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(FormControl), Color.Black);

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create("LabelText", typeof(string), typeof(FormControl), null);

        public static readonly BindableProperty LabelTextColorProperty = BindableProperty.Create("LabelTextColor", typeof(Color), typeof(FormControl), Color.Black);

        public static readonly BindableProperty ValidationProperty = BindableProperty.Create("Validation", typeof(Validation), typeof(FormControl), null, propertyChanged: OnValidationPropertyChanged);

        public static readonly BindableProperty AssistantLabelTextProperty = BindableProperty.Create("AssistantLabelText", typeof(string), typeof(FormControl), " ");

        public static readonly BindableProperty AssistantLabelTextColorProperty = BindableProperty.Create("AssistantLabelTextColor", typeof(Color), typeof(FormControl), App.Current.Resources["GreyTextColor"]);

        public static readonly BindableProperty LabelStyleProperty = BindableProperty.Create(nameof(LabelStyle), typeof(Style), typeof(FormControl), App.Current.Resources["CaptionLabelStyle"]);

        private static void OnValidationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FormControl targetView = (FormControl)bindable;
            Validation val = (Validation)newValue;
            targetView.ApplyValidation();
        }

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public Color LabelTextColor
        {
            get => (Color)GetValue(LabelTextColorProperty);
            set => SetValue(LabelTextColorProperty, value);
        }

        public Style LabelStyle
        {
            get => (Style)GetValue(LabelStyleProperty);
            set => SetValue(LabelStyleProperty, value);
        }

        public Validation Validation
        {
            get => (Validation)GetValue(ValidationProperty);
            set => SetValue(ValidationProperty, value);
        }

        public string AssistantLabelText
        {
            get => (string)GetValue(AssistantLabelTextProperty);
            set => SetValue(AssistantLabelTextProperty, value);
        }
        public Color AssistantLabelTextColor
        {
            get => (Color)GetValue(AssistantLabelTextColorProperty);
            set => SetValue(AssistantLabelTextColorProperty, value);
        }
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        protected virtual View FormControlView { get; }

        protected virtual void ApplyValidation()
        {
            HandleState();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsEnabled))
            {
                HandleState();
            }
        }

        private void HandleState()
        {
            if (FormControlView == null) return;

            if (IsEnabled)
            {
                if (Validation == null || Validation.IsValid)
                {
                    VisualStateManager.GoToState(FormControlView, "Normal");
                }
                else
                {
                    VisualStateManager.GoToState(FormControlView, "Invalid");
                }
            }
            else
            {
                VisualStateManager.GoToState(FormControlView, "Disabled");
            }
        }
    }
}


