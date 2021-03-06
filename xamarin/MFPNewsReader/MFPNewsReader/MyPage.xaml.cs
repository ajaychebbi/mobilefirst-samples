﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Json;
using Xamarin.Forms;

using Worklight;
using System.Diagnostics;
//@author: ajaychebbi
namespace MFPNewsReader
{
	public partial class MyPage : MasterDetailPage
	{
		public ObservableCollection<Article> Articles { get; set; }
		public MyPage ()
		{
			InitializeComponent ();
			Articles = new ObservableCollection<Article> ();
			//Bind data to form
			BindingContext = new { 
				DetailPage = new { MyArticles = Articles } 
			};

			btnConnect.Clicked += (object sender, EventArgs e) => Connect ();
			btnInvokeProcedure.Clicked += (object sender, EventArgs e) => InvokeProcedure ();
		}
		private async void Connect()
		{
			WorklightResponse resp = await MFPNewsReader.App.wlClient.Connect ();
			if (resp.Success)
			{
				DisplayAlert("Connection Status","Yay!!","Close");
			}
			else
			{
				DisplayAlert("Connection Status failed",resp.Message,"Close");
			}
		}
		private async void InvokeProcedureOld()
		{
			//Invoke Procedure - the old way
			Articles.Clear ();
			//Params for the invocation
			WorklightProcedureInvocationData proceduerParams = new WorklightProcedureInvocationData("SampleHTTPAdapter", "getFeed", new object[] { });

			WorklightResponse resp = await MFPNewsReader.App.wlClient.InvokeProcedure(proceduerParams);
			if (resp.Success) {
				JsonArray jsonArray = (JsonArray)resp.ResponseJSON["rss"]["channel"]["item"];
				foreach (JsonObject title in jsonArray)
				{
					System.Json.JsonValue titleString;
					title.TryGetValue("title", out titleString);
					System.Json.JsonValue itemString;
					title.TryGetValue("description", out itemString);
					Articles.Add(new Article(titleString,itemString));
					
				}
			} else {
				DisplayAlert("Connection Status failed",resp.Message,"Close");
			}
		}
        private async void InvokeProcedure()
        {
            //Invoke Procedure - the new REST way
            Articles.Clear();
            //hopefully I can integrate oAuth with this sample soon
            Uri adapterMethod = new Uri(App.wlClient.ServerUrl+ "/adapters/SampleHTTPAdapter/getFeed");   
            WorklightResourceRequest wlResReq = MFPNewsReader.App.wlClient.ResourceRequest(adapterMethod,"GET");
            WorklightResponse resp = await wlResReq.Send();
           
            if (resp.Success)
            {
                JsonArray jsonArray = (JsonArray)resp.ResponseJSON["rss"]["channel"]["item"];
                foreach (JsonObject title in jsonArray)
                {
                    System.Json.JsonValue titleString;
                    title.TryGetValue("title", out titleString);
                    System.Json.JsonValue itemString;
                    title.TryGetValue("description", out itemString);
                    Articles.Add(new Article(titleString, itemString));

                }
            }
            else
            {
                DisplayAlert("Connection Status failed", resp.Message, "Close");
            }
        }
    }
}

