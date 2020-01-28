using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Devices.Bluetooth.Advertisement;
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
        private BluetoothLEAdvertisementPublisher publisher;
        private Guid UUID_Val;
        private short MajorKey;
        private short MinorKey;
        private Random Rand;

        public MainPage()
        {
            this.InitializeComponent();

            // Setup publisher
            publisher = new BluetoothLEAdvertisementPublisher();
            publisher.StatusChanged += Publisher_StatusChanged;

            // Setup things we'll use to setup the beacon
            Rand = new Random();
            UUID_Val = Guid.Empty;
            MajorKey = 0;
            MinorKey = 0;

            // Keep Broadcasting animation large
            SizeChanged += MainPage_SizeChanged;

            // Transition smoothly between states
            TransitionToBroad.Completed += ContinueWithBroadcast;
            TransitionToStarting.Completed += ContinueWithStarting;
            TransitionToError.Completed += ContinueWithError;
        }

        private void Publisher_StatusChanged(BluetoothLEAdvertisementPublisher sender, 
            BluetoothLEAdvertisementPublisherStatusChangedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private byte[] CreatePayload()
        {
            var payload = new List<byte>();
            payload.Add(0x02); // MUST INCLUDE: Tag that declares this is an iBeacon
            payload.Add(0x15); // MUST INCLUDE: Tag that declares this is a range beacon
            UUID_Val = Guid.NewGuid();
            payload.AddRange(UUID_Val.ToByteArray()); // UUID: 16 bytes, used to declare the "type" this beacon belongs to
                                                  // For example, you would use a single UUID for all the iBeacons in a department
                                                  // store. Using a single UUID associates these beacons with your store.
                                                  // This creates a random UUID to use for this broadcast.
            MajorKey = (short)Rand.Next(0, 255); // MAJOR KEY: 2 bytes. Use this to declare what "category" this beacon belongs to. In
            payload.Add((byte)(MajorKey & 0x0011)); // our department store example again, this might be a code associated with a department,
            payload.Add((byte)(MajorKey >> 2)); // e.x. 0x1234 for clothing and 0x5678 for jewelery.

            MinorKey = (short)Rand.Next(0, 255); // MINOR KEY: 2 bytes. use this to specify the specific beacon within the category 
            payload.Add((byte)(MinorKey & 0x0011));// associated with the major key. Again, in our previous example, the beacon with
            payload.Add((byte)(MinorKey >> 2)); // Major key 0x1234 and Minor key 0x1111 would be the beacon next to the pants in the 
                                             // clothing department, perhaps.
            payload.Add(0xc5); // This is the Signal Power value, used in calculating the distance a device is to the beacon. 
                               // It must be calibrated based on device/bluetooth chip. Check the readme for info about this.
            return payload.ToArray();
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
