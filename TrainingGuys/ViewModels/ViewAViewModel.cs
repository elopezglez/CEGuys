using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Diagnostics;
using Prism.Events;
using Acr.UserDialogs;

namespace TrainingGuys.ViewModels
{
	public class ViewAViewModel : BindableBase, INavigationAware
	{
		INavigationService _navigationService;
		IUserDialogs _userDialogs = UserDialogs.Instance;

		string _Param = "Parameter";
		public string Param 
		{
			get { return _Param; }
			set { SetProperty(ref _Param, value); }
		}

		string _Title = "VIEW A";
		public string Title
		{
			get { return _Title; }
			set { SetProperty(ref _Title, value); }
		}

		bool _IsActive = true;
		public bool IsActive
		{
			get { return _IsActive; }
			set
			{
				SetProperty(ref _IsActive, value);
			}
		}

		public DelegateCommand MessageWithXFCommand { get; private set; }
		public DelegateCommand NavigateCommand { get; private set; }
		public ViewAViewModel(INavigationService navigationService, IEventAggregator ea)
		{			
			_navigationService = navigationService;
			NavigateCommand = new DelegateCommand(Navigate);
			ea.GetEvent<PrismEvent>().Subscribe(HandleEvent);
		}

		void HandleEvent(string obj)
		{
			_userDialogs.Alert(obj, "Handling Event", "ok");
		}


	 	void Navigate()
		{
			var p = new NavigationParameters();
			p.Add("id", Param);
			_navigationService.GoBackAsync(p);
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
 		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{			
			if (parameters.ContainsKey("id"))
				Param = (string)parameters["id"];
			
 		}
	}
}
