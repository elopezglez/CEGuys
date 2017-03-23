using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using TrainingGuys;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace TrainingGuys
{
	public class AzureService
	{

		MobileServiceClient client;
		IMobileServiceSyncTable<CEGuy> CEGuyTable;
		IUserDialogs _userDialogs = UserDialogs.Instance;
		public async Task Initialize()
		{
			if (client?.SyncContext?.IsInitialized ?? false)
				return;
			try
			{
				//here you set your azure service url
				var appUrl = "http://ketser.azurewebsites.net";
				client = new MobileServiceClient(appUrl);
				var fileName = "LocalDB.db";
				var store = new MobileServiceSQLiteStore(fileName);
				store.DefineTable<CEGuy>();
				await client.SyncContext.InitializeAsync(store);
				CEGuyTable = client.GetSyncTable<CEGuy>();
			}
			catch (Exception ex)
			{
				_userDialogs.ShowError(string.Format("Initialize. {0}", ex.Message), 2000);
				Debug.WriteLine("Initialize. {0}", ex.Message);
			}
		}

		public async Task SyncGuys()
		{
			await Initialize();
			try
			{
				if (!CrossConnectivity.Current.IsConnected)
					return;
				await client.SyncContext.PushAsync();
				await CEGuyTable.PullAsync("allGuys", CEGuyTable.CreateQuery());
			}
			catch (Exception ex)
			{
				_userDialogs.ShowError(string.Format("SyncGuys. {0}", ex.Message), 2000);
				Debug.WriteLine("SyncGuys. {0}", ex.Message);
			}
		}

		public async Task<IEnumerable<CEGuy>> GetCEGuys()
		{
			await Initialize();
			await SyncGuys();
			var data = await CEGuyTable.ToEnumerableAsync();
			return data;
		}

		public async Task<CEGuy> AddCEGuy(string name, string training)
		{
			await Initialize();
			var guy = new CEGuy
			{				
				Name = name,
				Training= training
			};
			await CEGuyTable.InsertAsync(guy);
			await SyncGuys();
			return guy;
		}

	}
}

