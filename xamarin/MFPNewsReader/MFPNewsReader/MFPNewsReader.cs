using System;

using Xamarin.Forms;
using Worklight;
//@author: ajaychebbi
namespace MFPNewsReader
{
	public class App : Application
	{
		public static IWorklightClient wlClient;
		public App ()
		{
			MainPage = new MyPage ();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

