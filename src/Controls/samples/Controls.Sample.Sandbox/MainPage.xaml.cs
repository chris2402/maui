namespace Maui.Controls.Sample;

public partial class MainPage : ContentPage
{
	private const int DEFUALT_VALUE = 0;
	private const int DEFAULT_MAX_VALUE = int.MaxValue;

	public static readonly BindableProperty ValueProperty =
		BindableProperty.Create(nameof(Value), typeof(int), typeof(MainPage), DEFUALT_VALUE, 
			coerceValue: CoerceValueHandler);

	public static readonly BindableProperty MaxValueProperty =
		BindableProperty.Create(nameof(MaxValue), typeof(int), typeof(MainPage), DEFAULT_MAX_VALUE,
			propertyChanged: MaxValuePropertyChangedHandler);

	private static object CoerceValueHandler(BindableObject sender, object value)
	{
		MainPage page = (MainPage)sender;
		return Math.Min((int)value, page.MaxValue);
	}

	private static void MaxValuePropertyChangedHandler(BindableObject sender, object oldValue, object newValue)
	{
		MainPage page = (MainPage)sender;
		page.CoerceValue(ValueProperty);
	}


	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;

		Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(5), () => {
			MaxValue = int.MinValue; // Value property should be coerced to int.MinValue
		});
	}

	public int Value
	{
		get => (int)GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	public int MaxValue
	{
		get => (int)GetValue(MaxValueProperty);
		set => SetValue(MaxValueProperty, value);
	}
}
