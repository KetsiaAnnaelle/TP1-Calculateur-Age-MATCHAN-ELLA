namespace CalculateurAge.Views;

public partial class ResultatPage : ContentPage, IQueryAttributable
{
	public string Nom { get; private set; } = string.Empty;
	public int Age { get; private set; }

	public ResultatPage()
	{
		InitializeComponent();
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		if (query.TryGetValue(nameof(Nom), out object? nom) && nom is string valeurNom)
			Nom = valeurNom;

		if (query.TryGetValue(nameof(Age), out object? age) && age is int valeurAge)
			Age = valeurAge;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		lblMessage.Text = $"{Nom}, vous avez {Age} ans";
	}

	private async void OnRetourClicked(object? sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}