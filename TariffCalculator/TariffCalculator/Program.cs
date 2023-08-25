using System.Configuration;

namespace TariffCalculatorApp
{
    class InputData
    {
        public string InputDate { get; set; }

        public float InputRecharge { get; set; }
        public float InputReading { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            float openingBalance = float.Parse(ConfigurationManager.AppSettings["openingBalance"]);
            float previousReading = float.Parse(ConfigurationManager.AppSettings["previousReading"]);
            float unitSlab1 = int.Parse(ConfigurationManager.AppSettings["unitSlab1"]);
            float unitSlab2 = int.Parse(ConfigurationManager.AppSettings["unitSlab2"]);
            float unitSlab3 = int.Parse(ConfigurationManager.AppSettings["unitSlab3"]);
            float unitSlab4 = int.Parse(ConfigurationManager.AppSettings["unitSlab4"]);
            float energyRate1 = float.Parse(ConfigurationManager.AppSettings["energyCharge1"]);
            float energyRate2 = float.Parse(ConfigurationManager.AppSettings["energyCharge2"]);
            float energyRate3 = float.Parse(ConfigurationManager.AppSettings["energyCharge3"]);
            float energyRate4 = float.Parse(ConfigurationManager.AppSettings["energyCharge4"]);
            float energyRate5 = float.Parse(ConfigurationManager.AppSettings["energyCharge5"]);
            float senctionLoad = float.Parse(ConfigurationManager.AppSettings["senctionLoad"]);

            bool calculateKey = true;
            //bool dateAdded = false;

            float totalDays = 31;
            string getDate = "Empty";

            List<InputData> newData = new List<InputData>();

            while (calculateKey)
            {

                // while (!dateAdded)
                {
                    Console.WriteLine("Enter Date ( DD-MM-YYYY )");
                    getDate = Console.ReadLine();

                    if (DateTime.TryParseExact(getDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                    {
                        int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

                        if (daysInMonth == 31)
                        {
                            totalDays = 31;
                        }
                        if (daysInMonth == 30)
                        {
                            totalDays = 30;
                        }
                        if (daysInMonth == 29)
                        {
                            totalDays = 29;
                        }
                        if (daysInMonth == 28)
                        {
                            totalDays = 28;
                        }
                        //  dateAdded = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format.");
                        //   dateAdded = false;
                    }
                }

                Console.WriteLine("Enter Recharge Value");
                float getRecharge = float.Parse(Console.ReadLine());

                Console.WriteLine("Enter Reading Value");
                float getReading = float.Parse(Console.ReadLine());

                newData.Add(new InputData { InputDate = getDate, InputRecharge = getRecharge, InputReading = getReading });

                Console.WriteLine();
                Console.WriteLine("Press 'Enter' to calculate");
                Console.WriteLine("Press 'A' to add More Value");
                string ans = Console.ReadLine();

                if (ans == "a")
                {
                    calculateKey = true;
                }
                else
                {
                    calculateKey = false;
                }
            }

            float fixedCharge = 0;
            float totalCharge = 0;
            float balance = openingBalance;
            //float presentMonthCons = 0;
            float kWh;

            bool repeatAgain = false;

            if (senctionLoad > 10)
            {
                fixedCharge += (senctionLoad - 10) * 40;
                senctionLoad = 10;
            }
            if (senctionLoad > 5 && senctionLoad <= 10)
            {
                fixedCharge += (senctionLoad - 5) * 30;
                senctionLoad = 5;
            }
            if (senctionLoad > 0 && senctionLoad <= 5)
            {
                fixedCharge += (senctionLoad - 0) * 20;
            }

            fixedCharge = fixedCharge / totalDays;


            foreach (InputData data in newData)
            {
                string date = $"{data.InputDate}";
                float recharge = float.Parse($"{data.InputRecharge}");
                float presentReading = float.Parse($"{data.InputReading}");
                float energyCharge = 0;

                float presentMonthCons = 0;

                //Console.WriteLine(energyCharge + "  yyyyyyyyyyyy");

                if (repeatAgain == true)
                {
                    openingBalance = balance;
                }

                balance = balance + recharge;

                float presentDayConsumption = presentReading - previousReading;

                presentMonthCons = presentMonthCons + presentDayConsumption;
                kWh = presentDayConsumption;

                Console.WriteLine(presentMonthCons);


                if (presentMonthCons > unitSlab4)// if more than 600
                {
                    energyCharge += energyRate5 * (presentMonthCons - unitSlab4); // 650 - 601 = 49
                    presentMonthCons = unitSlab4;
                    Console.WriteLine(energyCharge);
                }
                if (presentMonthCons > unitSlab3 && presentMonthCons <= unitSlab4)// if bw 401 - 600
                {
                    energyCharge += energyRate4 * (presentMonthCons - unitSlab3); // 600 - 401 = 199
                    presentMonthCons = unitSlab3;
                    Console.WriteLine(energyCharge);
                }
                if (presentMonthCons > unitSlab2 && presentMonthCons <= unitSlab3) // if bw 201 - 400
                {
                    energyCharge += energyRate3 * (presentMonthCons - unitSlab2); // 400 - 201 = 199
                    presentMonthCons = unitSlab2;
                    Console.WriteLine(energyCharge);
                }
                if (presentMonthCons > unitSlab1 && presentMonthCons <= unitSlab2) // if bw 101 - 200
                {
                    energyCharge += energyRate2 * (presentMonthCons - unitSlab1); // 200 - 101 = 99
                    presentMonthCons = unitSlab1;
                    Console.WriteLine(energyCharge);
                }
                if (presentMonthCons > 0 && presentMonthCons <= unitSlab1) // if bw 0 - 100
                {
                    energyCharge += energyRate1 * (presentMonthCons); // 100 - 0 = 100
                    Console.WriteLine(energyCharge);
                    //Console.WriteLine(kWh);
                }

                totalCharge = energyCharge + fixedCharge;

                balance = balance - totalCharge;

                if (!repeatAgain)
                {
                    Console.WriteLine("   Date     opening-Balance     recharge     previous-reading     present-reading     kwh     energy-Charge     fixed-Charge     total-Charge     balance");
                }
                Console.WriteLine($"{date}       {string.Format("{0:F2}", openingBalance)}           {string.Format("{0:F2}", recharge)}             {string.Format("{0:F2}", previousReading)}              {string.Format("{0:F2}", presentReading)}         {string.Format("{0:F2}", kWh)}        {string.Format("{0:F2}", energyCharge)}             {string.Format("{0:F2}", fixedCharge)}             {string.Format("{0:F2}", totalCharge)}         {string.Format("{0:F2}", balance)}");

                repeatAgain = true;
                previousReading = previousReading + presentDayConsumption;
            }
        }
    }

}