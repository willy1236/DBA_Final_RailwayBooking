using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayBooking
{
    public static class lib
    {
        public static int CalculateFare(double km)
        {
            double[] thresholds = { 50, 100, 200, 300 };
            double[] rates = { 3.39, 2.92, 2.81, 2.37, 2.20 };

            double fare = 0;
            double remaining = km;

            for (int i = 0; i < thresholds.Length; i++)
            {
                double segmentKm = 0;

                if (remaining > 0)
                {
                    if (i == 0)
                        segmentKm = Math.Min(remaining, thresholds[i]);
                    else
                        segmentKm = Math.Min(remaining, thresholds[i] - thresholds[i - 1]);

                    fare += segmentKm * rates[i];
                    remaining -= segmentKm;
                }
            }

            if (remaining > 0)
            {
                fare += remaining * rates[rates.Length - 1];
            }

            return (int)Math.Round(fare, MidpointRounding.AwayFromZero);
        }
        public static int CalculateMemberPoint(int money)
        {
            return (int)money / 50;
        }
    }
}
