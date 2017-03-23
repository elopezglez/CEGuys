using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Acr.UserDialogs;

namespace TrainingGuys.Droid
{
	[Activity(Label = "TrainingGuys.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			UserDialogs.Init(this);
			GetScreenSize();
			LoadApplication(new App(new AndroidInitializer()));
		}

		void GetScreenSize()
		{
			var metrics = Resources.DisplayMetrics;
			App.ScreenWidth = ConvertPixelsToDp(metrics.WidthPixels);
			App.ScreenHeight = ConvertPixelsToDp(metrics.HeightPixels);
		}

		double ConvertPixelsToDp(float pixelValue)
		{
			var dp = (pixelValue) / Resources.DisplayMetrics.Density;
			return dp;
		}

	}

	public class AndroidInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IUnityContainer container)
		{

		}
	}
}
