namespace CalculateurAge;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	// Gestionnaire appele au clic du bouton Calculer.
	// sender = le controle clique e = donnees de 1 evenement.
	private async void OnCalculerClicked(object? sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(entryNom.Text))
		{
			await DisplayAlertAsync("Erreur", "Entrez un nom", "OK");
			return;
		}

		DateTime dateNaissance = pickerDate.Date ?? DateTime.Today;
		int age = DateTime.Today.Year - dateNaissance.Year;
		if (dateNaissance.Date > DateTime.Today.AddYears(-age))
			age--;

		await Shell.Current.GoToAsync(nameof(Views.ResultatPage), new Dictionary<string, object>
		{
			[nameof(Views.ResultatPage.Nom)] = entryNom.Text.Trim(),
			[nameof(Views.ResultatPage.Age)] = age
		});

	}
}
