using System.Diagnostics;
using Acr.UserDialogs;
using Prism.Unity;
using TrainingGuys.Views;

namespace TrainingGuys
{
	public partial class App : PrismApplication
	{
		public static double ScreenWidth;
		public static double ScreenHeight;
		//IUserDialogs _userDialogs = UserDialogs.Instance;
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

		protected override void OnInitialized()
		{
			try
			{
				InitializeComponent();
				AppConstants.ScreenHeight = ScreenHeight;
				NavigationService.NavigateAsync("MainPage");
			}
			catch (System.Exception ex)
			{
				//_userDialogs.ShowError(string.Format("OnInitialized. {0}", ex.Message), 5000);
				Debug.WriteLine("OnInitialized. {0}", ex.Message);
			}


		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<MainPage>();
			Container.RegisterTypeForNavigation<ViewA>();
			Container.RegisterTypeForNavigation<ViewB>();

		}
	}
}

