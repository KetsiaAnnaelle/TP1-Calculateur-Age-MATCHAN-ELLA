namespace CalculateurAge;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    public object CounterBtn { get; private set; }

    private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}


	// Gestionnaire appele au clic du bouton Calculer.
	// sender = le controle clique e = donnees de 1 evenement.
	private void OnCalculerClicked(object sender, EventArgs e)
	{
		//validation : on retuse un nom vide.
		if (string. IsNullOrWhiteSpace(entryNom.Text))
		{
			DisplayAlert("Erreur","Entrez un nom", "OK") ;
			return; // on sort sans rien calculer
		}
		DateTime d = pickerDate.Date;
		int age = DateTime.Today.Year - d.Year;
		// Si 1 anniversaire n est pas encore passe cette annee,
		//on retire une annee.
		if (d.Date > DateTime.Today.AddYears(-age)) age--;

		// On ecrit DIRECTEMENT dans les controles : c est
		//precisement ce que le MVVM va supprimer.
		lblResultat.Text = $"{entryNom.Text}, vous avez {age} ans";
		lblResultat.IsVisible = true;

	}
}
