using Microsoft.Azure.Devices.Client;
using SoilMoisture.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SoilMoisture.Device
{
    class Program
    {
        private static DeviceClient deviceClient;
        private readonly static string connectionString = "HostName=SoilMoisture.azure-devices.net;DeviceId=ashirwad-simulator;SharedAccessKey=jIKMUr+5+qYrd9dhPFTWm8bYawxiHyI4nmb6Y/XP4mc=";
        static void Main(string[] args)
        {
            Console.WriteLine("Sending Messages");
            Task.Delay(10000).Wait();
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
            int i = 0;
            bool result;
            //sends sensor data from the device simulator every second for 10 minutes
            while (i < 1)
            {
                Task.Delay(6000).Wait();
                result = SendMessages(deviceClient);
                if (result)
                {
                    Console.WriteLine($"Message {i} delivered");
                }
                else
                {
                    Console.WriteLine("Message failed");
                }
                i++;
            }
        }

        /// <summary>
        /// Method to send data from the device simulator to IoT Hub
        /// </summary>
        /// <param name="deviceClient"></param>
        /// <returns></returns>
        public static bool SendMessages(DeviceClient deviceClient)
        {
            var sensorData = new SoilMoistureModel()
            {
                deviceId = "ashirwad-simulator",
                moistureLevel = GetRandomNumberInRange(100, 200),
                recordedAt = DateTime.Now
            };
            var jsonData = JsonSerializer.Serialize(sensorData);

            try
            {
                var data = new Message(Encoding.ASCII.GetBytes(jsonData));
                deviceClient.SendEventAsync(data).GetAwaiter().GetResult();
                //Console.WriteLine("Message Sent");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Info - {ex.Message}");
                return false;
            }

        }

        /// <summary>
        /// Method to generate random value to mock the Soil Moisture level values
        /// </summary>
        /// <param name="minNumber"></param>
        /// <param name="maxNumber"></param>
        /// <returns></returns>
        public static double GetRandomNumberInRange(double minNumber, double maxNumber)
        {
            return new Random().NextDouble() * (maxNumber - minNumber) + minNumber;
        }
    }
}
