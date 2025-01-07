using PeopleJordyAguilar.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PeopleJordyAguilar
{
	internal class MainPageViewModel : INotifyPropertyChanged
	{
		private string personName;

		public string PersonName
		{
			get => personName;
			set
			{
				personName = value;
				OnPropertyChanged();
			}
		}

		public string StatusMessage => App.PersonRepo.StatusMessage;

		public ObservableCollection<Person> AllPeople => new(App.PersonRepo.GetAllPeople());

		public ICommand AddPerson { get; }

		public ICommand Delete { get; }

		public MainPageViewModel()
		{
			AddPerson = new Command(() =>
			{
				App.PersonRepo.AddNewPerson(PersonName);
				PersonName = "";

				OnPropertyChanged(nameof(StatusMessage));
				OnPropertyChanged(nameof(AllPeople));
			});

			Delete = new Command<int>((id) =>
			{
				App.PersonRepo.DeletePerson(id);

				App.Current!.MainPage!.DisplayAlert("Usuario Eliminado", "El registro ha sido eliminado por Jordy Aguilar", "Aceptar");
				
				OnPropertyChanged(nameof(StatusMessage));
				OnPropertyChanged(nameof(AllPeople));
			});
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string name = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
