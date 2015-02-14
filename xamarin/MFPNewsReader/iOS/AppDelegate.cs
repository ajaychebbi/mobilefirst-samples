using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
//using Worklight.iOS;

//@author ajaychebbi
namespace MFPNewsReader.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());
			//Init MFP Client. This is iOS specific
			MFPNewsReader.App.wlClient = 
				Worklight.Xamarin.iOS.WorklightClient.CreateInstance ();

			return base.FinishedLaunching (app, options);
		}

	}
}

