using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace iBeaconSimulateDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Keep Broadcasting animation large
            SizeChanged += MainPage_SizeChanged;

            // Transition smoothly between states
            TransitionToBroad.Completed += ContinueWithBroadcast;
            TransitionToStarting.Completed += ContinueWithStarting;
            TransitionToError.Completed += ContinueWithError;
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double radius = Math.Sqrt(Math.Pow(e.NewSize.Width, 2) + Math.Pow(e.NewSize.Height, 2));
            BroadWidth.To = radius/100;
            BroadHeight.To = radius/100;
        }

        private void Toggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (Toggle.IsOn)
            {
                TransitionToStarting.Begin();
            } else
            {
                TransitionToError.Begin();
            }
        }

        private void ContinueWithBroadcast(object sender, object e)
        {
            BroadcastingAnimation.Begin();
        }

        private void ContinueWithStarting(object sender, object e)
        {
            StartingAnimation.Begin();
        }

        private void ContinueWithError(object sender, object e)
        {
            ErrorAnimation.Begin();
        }
    }
}
