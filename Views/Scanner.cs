using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;

namespace Views
{
    public class Scanner
    {
        private readonly DeviceInfo _deviceInfo;

        public Scanner(DeviceInfo deviceInfo)
        {
            this._deviceInfo = deviceInfo;
        }

        public ImageFile Scan()
        {
            // Connect to the device and instruct it to scan
            // Connect to the device
            var device = this._deviceInfo.Connect();

            // Start the scan
            var item = device.Items[1];
            var imageFile = (ImageFile)item.Transfer(FormatID.wiaFormatJPEG);

            // Return the imageFile
            return imageFile;
        }

        public override string ToString()
        {
            return _deviceInfo.Properties["Name"].get_Value().ToString();
            /*_deviceInfo.Properties["Name"].get_Value();*/
        }

        //// Clear the ListBox.
        //lbDevices.Items.Clear();

        //// Create a DeviceManager instance
        //var deviceManager = new DeviceManager();

        //// Loop through the list of devices and add the name to the listbox
        //for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
        //{
        //    //Add the device to the list if it is a scanner
        //    if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
        //    {
        //        continue;
        //    }

        //    lbDevices.Items.Add(new Scanner(deviceManager.DeviceInfos[i]));
        //}

        //if (lbDevices.Items.Count == 0)
        //{
        //    MessageBox.Show("¡No se encontraron dispositivos!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //}
        //else
        //{
        //    lbDevices.SelectedIndex = 0;

        //    // Scanner selected?
        //    var device = lbDevices.SelectedItem as Scanner;
        //    if (device == null)
        //    {
        //        MessageBox.Show("¡Seleccione un dispositivo!", "Aviso",
        //                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Scan
        //    var image = device.Scan();

        //    // Save the image
        //    var path = @"C:\Users\Public\Pictures\" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".jpeg";
        //    if (File.Exists(path))
        //    {
        //        File.Delete(path);
        //    }
        //    image.SaveFile(path);
        //}
    }
}
