using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Diagnostics;

namespace TrainingGuys.ViewModels
{
	public class ViewBViewModel : BindableBase, INavigationAware
	{
		INavigationService _navigationService;

		string _Param = "Parameter";
		public string Param
		{
			get { return _Param; }
			set { SetProperty(ref _Param, value); }
		}

		string _Title = "VIEW B";
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

		public DelegateCommand NavigateCommand { get; private set; }
		public ViewBViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			NavigateCommand = new DelegateCommand(Navigate);
		}

		void Navigate()
		{
			var p = new NavigationParameters();
			p.Add("id",Param);
			_navigationService.NavigateAsync("ViewA",p);			 			 
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
