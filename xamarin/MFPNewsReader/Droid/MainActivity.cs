using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
//@author ajaychebbi
namespace MFPNewsReader.Droid
{
	[Activity (Label = "MFPNewsReader.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new App ());

			//Init MFP Client. This is Android specific as it takes in a Android.Activity
			MFPNewsReader.App.wlClient = 
				Worklight.Xamarin.Android.WorklightClient.CreateInstance (this);
		}
	}
}

