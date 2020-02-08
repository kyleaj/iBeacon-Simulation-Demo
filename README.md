# iBeacon-Simulation-Demo
Simulate iBeacons with Windows 10 apis.

# Introduction
New in Windows 10, the Windows.Devices.Bluetooth.Advertisement provides the BluetoothLEAdvertisementPublisher 
for periodically sending out messages over bluetooth that can be listened to by nearby devices - creating a 
bluetooth "beacon." This new class allows the simulating of "iBeacons," Apple's proprietary bluetooth beacon
protocol. This repository contains a simple sample application that generates a UUID, Major key, and Minor key
to be published over via the iBeacon protocol and be detected by iOS, Mac, Android, and other Windows 10 devices.
The commit history acts a small tutorial in emulating iBeacons with Windows 10.

For more information on the iBeacon protocol, see [here](https://developer.apple.com/ibeacon/Getting-Started-with-iBeacon.pdf).

# Calibrating your iBeacon (Calcluating the Signal Power value)
(From the above link/Apple's iBeacon specification)

To provide the best user experience, it is critical to perform calibration in your deployment
environment. As each beacon is installed you should perform a calibration step. Core Location
uses an estimation model that requires calibration at a distance of 1 meter away from the
beacon. To perform this calibration you should:

• Install the beacon and have it emitting a signal.

• Using an iPhone or iPod touch running iOS 7 or later, and has a Bluetooth 4.0 radio,
repeatedly sample the signal strength at a distance of 1 meter for a minimum of 10 seconds.
When taking these signal strength readings you should hold the device in a portrait
orientation with the top half of the device unobstructed.

• Move the device slowly back and forth on a 30cm line, maintaining orientation, and
remaining equidistant from the measuring device

• For the duration of the calibration process, gather the values reported in the CLBeacon’s rssi
property.

• Average the collected rssi values to obtain the Measured Power value.

• Apply this Measured Power value to the beacon. Consult the details provided for the beacon
used, as they may differ from manufacturer to manufacturer.

The physical surroundings can affect the signal strength. Since the
surroundings will almost certainly vary between installation locations it is important to repeat
these steps for each beacon that is installed.
