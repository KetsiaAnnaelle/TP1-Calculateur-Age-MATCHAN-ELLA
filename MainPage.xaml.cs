namespace CalculateurAge;

using CalculateurAge.ViewModels;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new CalculateurViewModel();
	}
}
