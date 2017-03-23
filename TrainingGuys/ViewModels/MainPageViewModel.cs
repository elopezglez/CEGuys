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

namespace TrainingGuys.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		IUserDialogs _userDialogs = UserDialogs.Instance;
		private string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
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

		AzureService azureSvc;
		public MainPageViewModel()
		{			
			CreateGuy = new DelegateCommand(AddGuy);
			ReloadGuys = new DelegateCommand(GetGuys);
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
			if (parameters.ContainsKey("title"))
				Title = (string)parameters["title"] + " and Prism";
		}
	}
}

