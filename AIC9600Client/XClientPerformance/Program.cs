﻿using AIC.M9600.Client.DataProvider;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XClientPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataProvider client = new DataProvider("192.168.1.6", 9601, 1, 1, TimeSpan.FromSeconds(3000));

            //Stopwatch timer = new Stopwatch(); 
            //timer.Start(); 
            //var mainControlCardTask = client.Query<T_MainControlCard>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var wireMatchingCardTask = client.Query<T_WireMatchingCard>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var wirelessReceiveCardTask = client.Query<T_WirelessReceiveCard>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var transmissionCardTask = client.Query<T_TransmissionCard>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var abstractChannelInfoTask = client.Query<T_AbstractChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var iEPEChannelInfoTask = client.Query<T_IEPEChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var eddyCurrentDisplacementChannelInfoTask = client.Query<T_EddyCurrentDisplacementChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var eddyCurrentKeyPhaseChannelInfoTask = client.Query<T_EddyCurrentKeyPhaseChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var eddyCurrentTachometerChannelInfoTask = client.Query<T_EddyCurrentTachometerChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var digitTachometerChannelInfoTask = client.Query<T_DigitTachometerChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var analogRransducerInChannelInfoTask = client.Query<T_AnalogRransducerInChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var relayChannelInfoTask = client.Query<T_RelayChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var digitRransducerInChannelInfoTask = client.Query<T_DigitRransducerInChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var digitRransducerOutChannelInfoTask = client.Query<T_DigitRransducerOutChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var analogRransducerOutChannelInfoTask = client.Query<T_AnalogRransducerOutChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var wirelessScalarChannelInfoTask = client.Query<T_WirelessScalarChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var wirelessVibrationChannelInfoTask = client.Query<T_WirelessVibrationChannelInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var abstractSlotInfoTask = client.Query<T_AbstractSlotInfo>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var iEPESlotTask = client.Query<T_IEPESlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var eddyCurrentDisplacementSlotTask = client.Query<T_EddyCurrentDisplacementSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var eddyCurrentKeyPhaseSlotTask = client.Query<T_EddyCurrentKeyPhaseSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var eddyCurrentTachometerSlotTask = client.Query<T_EddyCurrentTachometerSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var digitTachometerSlotTask = client.Query<T_DigitTachometerSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var analogRransducerInSlotTask = client.Query<T_AnalogRransducerInSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var relaySlotTask = client.Query<T_RelaySlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var digitRransducerInSlotTask = client.Query<T_DigitRransducerInSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var digitRransducerOutSlotTask = client.Query<T_DigitRransducerOutSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var analogRransducerOutSlotTask = client.Query<T_AnalogRransducerOutSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var wirelessScalarSlotTask = client.Query<T_WirelessScalarSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var wirelessVibrationSlotTask = client.Query<T_WirelessVibrationSlot>(null, "where 1 = 1", null); PrintElapsed(timer);
            //var divFreInfoTask = client.Query<T_DivFreInfo>(null, "where 1 = 1", null); PrintElapsed(timer);

            DataProvider client = new DataProvider("127.0.0.1", 9601, 1, 1, TimeSpan.FromSeconds(3));


            long loopCount = 0;
            while (true)
            {
                try
                {
                    Thread.Sleep(100);
                    var mainControlCardTask = client.Query<T_MainControlCard>(null, "where 1 = 1", null);
                    if (mainControlCardTask.IsOK)
                    {
                        Console.WriteLine("Count = " + loopCount);
                        loopCount++;
                    }
                    else
                    {
                        Console.WriteLine(mainControlCardTask.ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("错误:" + ex.Message);
                }
            }


        }

    }
}
