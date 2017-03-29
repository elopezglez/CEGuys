using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Diagnostics;
using Prism.Events;

namespace TrainingGuys.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		INavigationService _navigationService;
		IUserDialogs _userDialogs = UserDialogs.Instance;

		string _Param;
		public string Param
		{
			get { return _Param; }
			set { SetProperty(ref _Param, value); }
		}

		private string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}


		bool _IsEnabled = false;
		public bool IsEnabled
		{
			get { return _IsEnabled; }
			set { SetProperty(ref _IsEnabled, value); }
		}

		ObservableCollection<CEGuy> _VisibleGuys = new ObservableCollection<CEGuy>();
		public ObservableCollection<CEGuy> VisibleGuys
		{
			get { return _VisibleGuys; }
			set
			{				
				SetProperty(ref this._VisibleGuys, value);
			}
		}

		string _NewGuyName;
		public string NewGuyName
		{
			get { return _NewGuyName; }
			set
			{				
				SetProperty(ref this._NewGuyName, value);
			}
		}

		string _NewGuyTraining;
		public string NewGuyTraining 
		{
			get { return _NewGuyTraining; }
			set
			{
				SetProperty(ref this._NewGuyTraining, value);
			}
		}

		public DelegateCommand CreateGuy { get; set; }
		public DelegateCommand ReloadGuys { get; set; }
		public DelegateCommand NavigateCommand { get; private set; }
		public DelegateCommand SubscribeCommand { get; private set; }

		AzureService azureSvc;
		IEventAggregator _ea;
		public MainPageViewModel(INavigationService navigationService, IEventAggregator ea)
		{
			_ea = ea;
			_navigationService = navigationService;
			CreateGuy = new DelegateCommand(AddGuy);
			ReloadGuys = new DelegateCommand(GetGuys);
			NavigateCommand = new DelegateCommand(Navigate).ObservesCanExecute((p) => IsEnabled);
			SubscribeCommand = new DelegateCommand(PublishEvent);		
		}

		void Navigate()
		{
			_navigationService.NavigateAsync("ViewB");
		}

		void PublishEvent()
		{
			_ea.GetEvent<PrismEvent>().Publish(Param);
		}

		async void AddGuy()
		{
			try
			{
				InitializeAzure();
				var serv = await azureSvc.AddCEGuy(NewGuyName, NewGuyTraining);
				VisibleGuys.Add(serv);
				NewGuyName = string.Empty;
				NewGuyTraining = string.Empty;
			}
			catch (Exception ex)
			{
				_userDialogs.ShowError(string.Format("AddGuy. {0}", ex.Message), 5000);
				Debug.WriteLine("AddGuy. {0}", ex.Message);
			}
		}

		void InitializeAzure()
		{
			if (azureSvc == null)
				azureSvc = DependencyService.Get<AzureService>();
		}

		async void GetGuys()
		{
			InitializeAzure();
			var guys = await azureSvc.GetCEGuys();
			VisibleGuys = new ObservableCollection<CEGuy>(guys);
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{

		}
	}
}

