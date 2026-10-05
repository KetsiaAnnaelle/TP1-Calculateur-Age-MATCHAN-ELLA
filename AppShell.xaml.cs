namespace CalculateurAge;

using CalculateurAge.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(ResultatPage), typeof(ResultatPage));
	}
}
