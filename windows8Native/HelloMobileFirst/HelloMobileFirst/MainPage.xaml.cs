using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//added for this sample
using Windows.UI.Popups;
using IBM.Worklight;
using Windows.UI.Core;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Text;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HelloMobileFirst
{
    /// <summary>
    /// A sample to show connect and invoke procedure to the IBM Mobilefirsst server
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static CoreDispatcher dispatcher = null;
        private IBM.Worklight.WLClient wlClient = WLClient.getInstance();
        public static MainPage _this;

        public MainPage()
        {
            this.InitializeComponent();
            _this = this;
        }
      
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            //init a connection to the MobileFirst Server
            dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            ResponseListener RL = new ResponseListener();
            try
            {
                wlClient.connect(RL);
            }
            catch (System.Exception ex) {
                System.Diagnostics.Debug.WriteLine("Caught exception");
            }
        }
        public class ResponseListener : WLResponseListener
        {
            void WLResponseListener.onFailure(WLFailResponse response)
            {
                try
                {
                    dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        MainPage._this.ResultOut.Text = "Connection failed: " + response.getErrorMsg();
                    });
                }
                catch (Exception ex) { 
                    System.Diagnostics.Debug.WriteLine("Caught exception:"+ex.Message); 
                }
            }

            async void WLResponseListener.onSuccess(WLResponse response)
            {
                dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    StringBuilder result = new StringBuilder();
                    JObject feed = (JObject)response.getResponseJSON().GetValue("rss");
                    //if the return value is from the invoke procedure - we wil have a element called rss
                    JToken itemList = null;
                    if (feed != null)
                    {
                        //parse the result for pretty print
                        itemList = ((JObject)feed.GetValue("channel")).GetValue("item");
                        for (int i = 0; i < itemList.Count(); i++)
                        {
                            result.AppendLine( "- "+((JObject)itemList[i]).GetValue("description").ToString());
                        }
                    }
                    MainPage._this.ResultOut.Text = "Connection Success \n\n" + result;
                });
            }
        }

        private void BtnInvokeProcOnClick(object sender, RoutedEventArgs e)
        {
            //invoke a procedure on the server
            WLProcedureInvocationData proceduerParams = new WLProcedureInvocationData("SampleHTTPAdapter","getFeed");
            WLRequestOptions options = new WLRequestOptions();
            WLResponseListener listener = new ResponseListener();
            wlClient.invokeProcedure(proceduerParams,listener,options);
        }

        private void ResultOut_DoubleTap(object sender, DoubleTappedRoutedEventArgs e)
        {
            //clear out the content of the textBlock on double tap.
            ResultOut.Text = "Hello!";
        }
    }
}
