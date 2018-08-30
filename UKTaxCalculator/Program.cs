using System;

namespace UKTaxCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // String interpolation
            string version = "0.0.1";
            string author = "Raj";
            string email = "raj.nry.k@gmail.com";

            // Intro text
            Console.WriteLine("Welcome to my UK tax calculator ");
            Console.WriteLine($"author: {author}, version: {version}, email: {email} \n");

            // Ask for income
            Console.WriteLine("What is your yearly income? (all numbers no commas or spaces");

            // Store income from user
            double grossIncome = Double.Parse(Console.ReadLine());

            double taxable = CalculateTaxable(grossIncome);
            double taxPercentage = CalculateTaxBands(grossIncome);
            double taxPaid = taxable * taxPercentage;


            double weeklyWage = grossIncome / 52;

            double nationalInsurance = ((weeklyWage - 162) * 0.12) * 52;


            // Output income
            Console.WriteLine("Total taxable is: " + taxable );

            Console.WriteLine("You fall under tax percentage: " + taxPercentage);

            Console.WriteLine("Tax paid: " + taxPaid);
            Console.WriteLine("National Insurance: " + nationalInsurance);

            Console.WriteLine("You take home: " + CalculateNetIncome(grossIncome, taxPaid, nationalInsurance));

            // Wait for user to end program
            Console.ReadKey();

        }

        /**
         * Calculates the amount taxable
         **/
        static double CalculateTaxable(double amount)
        {
            // basic allowance which is not taxed
            double basicPersonalAllowance = 11859;
            double taxable = amount;

            // You don’t get a Personal Allowance on taxable income over £123,700.
            if (amount < 123700)
            {
                taxable = amount - basicPersonalAllowance;
            }

            return taxable;
        }


        /**
         * Calculate tax bands 
         * */
         static double CalculateTaxBands(double amount)
         {
            double percentageTax = 0;
            /*
             * Personal Allowance  Up to £11,850   0 %
             * Basic rate  £11,851 to £46,350  20 %
             * Higher rate £46,351 to £150,000 40 %
             * Additional rate over £150,000   45 %
             */

            if(amount >= 11851 && amount <= 46350)
            {
                percentageTax = 0.20;
            }else if(amount >= 46351 && amount <= 150000)
            {
                percentageTax = 0.40;
            }else if(amount > 150000)
            {
                percentageTax = 0.45;
            }

            return percentageTax;
         }

         static double CalculateNetIncome(double grossIncome, double taxPaid, double nationalInsuarance)
         {
            /*
            double basicPersonalAllowance = 11850.0;
            double netIncome = 0;
            double taxed = taxableAmount * taxPercentage;

            if( (taxableAmount + basicPersonalAllowance) < 123700)
            {
                netIncome = taxableAmount - (taxed) + basicPersonalAllowance;
            }
            else
            {
                netIncome = taxableAmount - (taxed);
            }*/

            return grossIncome - taxPaid - nationalInsuarance;
         }
    }


}
