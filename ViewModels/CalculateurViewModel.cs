namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
	private string _nom = string.Empty;
	private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
	private string _resultat = string.Empty;
	private string _messageMajorite = string.Empty;
	private string _messageAnniversaire = string.Empty;
	private bool _resultatVisible;

	public RelayCommand CalculerCommand { get; }
	public RelayCommand EffacerCommand { get; }
	public DateTime DateMaximale => DateTime.Today;
	public bool DateInvalide => DateNaissance.Date > DateTime.Today;
	public string MessageErreurDate => DateInvalide ? "La date de naissance ne peut pas être dans le futur." : string.Empty;

	public CalculateurViewModel()
	{
		CalculerCommand = new RelayCommand(Calculer, () =>
			!string.IsNullOrWhiteSpace(Nom) && !DateInvalide);
		EffacerCommand = new RelayCommand(Effacer);
	}

	public string Nom
	{
		get => _nom;
		set
		{
			if (SetField(ref _nom, value ?? string.Empty))
			{
				CalculerCommand.Rafraichir();
				EffacerResultats();
			}
		}
	}

	public DateTime DateNaissance
	{
		get => _dateNaissance;
		set
		{
			if (!SetField(ref _dateNaissance, value))
				return;

			OnPropertyChanged(nameof(DateInvalide));
			OnPropertyChanged(nameof(MessageErreurDate));
			CalculerCommand.Rafraichir();
			EffacerResultats();
		}
	}

	public string Resultat
	{
		get => _resultat;
		set => SetField(ref _resultat, value);
	}

	public string MessageMajorite
	{
		get => _messageMajorite;
		set => SetField(ref _messageMajorite, value);
	}

	public string MessageAnniversaire
	{
		get => _messageAnniversaire;
		set => SetField(ref _messageAnniversaire, value);
	}

	public bool ResultatVisible
	{
		get => _resultatVisible;
		set => SetField(ref _resultatVisible, value);
	}

	private void Calculer()
	{
		int age = DateTime.Today.Year - DateNaissance.Year;
		if (DateNaissance.Date > DateTime.Today.AddYears(-age))
			age--;

		Resultat = $"{Nom.Trim()}, vous avez {age} ans";
		MessageMajorite = age >= 18 ? "Statut : majeur" : "Statut : mineur";
		MessageAnniversaire = CreerMessageAnniversaire(DateNaissance, DateTime.Today);
		ResultatVisible = true;
	}

	private void Effacer()
	{
		Nom = string.Empty;
		DateNaissance = DateTime.Today.AddYears(-20);
		EffacerResultats();
	}

	private void EffacerResultats()
	{
		Resultat = string.Empty;
		MessageMajorite = string.Empty;
		MessageAnniversaire = string.Empty;
		ResultatVisible = false;
	}

	private static string CreerMessageAnniversaire(DateTime dateNaissance, DateTime aujourdHui)
	{
		DateTime prochainAnniversaire = CreerAnniversaire(dateNaissance, aujourdHui.Year);
		if (prochainAnniversaire.Date < aujourdHui.Date)
			prochainAnniversaire = CreerAnniversaire(dateNaissance, aujourdHui.Year + 1);

		int joursRestants = (prochainAnniversaire.Date - aujourdHui.Date).Days;
		return joursRestants == 0
			? "Joyeux anniversaire !"
			: $"Prochain anniversaire dans {joursRestants} jour(s).";
	}

	private static DateTime CreerAnniversaire(DateTime dateNaissance, int annee)
	{
		int jour = Math.Min(dateNaissance.Day, DateTime.DaysInMonth(annee, dateNaissance.Month));
		return new DateTime(annee, dateNaissance.Month, jour);
	}
}