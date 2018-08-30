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
            double taxPaid = CalculateIncomeTax(taxable, taxPercentage);
            double weeklyWage = CalculateWeeklyWage(grossIncome);

            double nationalInsurance = CalculateNationalInsurance(weeklyWage);


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
         * For example, if you earn £1,000 a week, you pay:
         * nothing on the first £162
         * 12% (£87.60) on the next £730
         * 2% (£2.16) on the next £108.
         **/
        static double CalculateNationalInsurance(double weeklyWage)
        {
            double nationalInsurance = 0;
            double weeklyAllowance = 162;

            // If our wekly wage is greater than allowance we are eligible for tax
            if(weeklyWage > weeklyAllowance)
            {
                // on the first 730 after (162) tax by 12%
                double taxable = weeklyWage - weeklyAllowance;
                double sumTax = 0;

                // do we have values fitting in this range
                if(taxable - 730 < 0)
                {
                    // tax by 12%
                    sumTax = (taxable) * 0.12;
                }
                else
                {
                    // We have a value greater than 730 so we need to apply two taxes
                    sumTax = (730) * 0.12;

                    // Apply another tax
                    double twoTax = taxable - 730;

                    if(twoTax < 108)
                    {
                        sumTax += (twoTax) * 0.02;
                    }
                    else
                    {
                        // Apply the maximum
                        sumTax += (108) * 0.02;
                    }
                }


                // Multiply back the sumTax by 52 weeks
                nationalInsurance = sumTax * 52;
            }

            return nationalInsurance;
        }

        static double CalculateIncomeTax(double taxableAmount, double taxPercentage)
        {
            return taxableAmount * taxPercentage;
        }

        static double CalculateWeeklyWage(double grossIncome)
        {
            return grossIncome / 52;
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
         * Personal Allowance  Up to £11,850   0 %
         * Basic rate  £11,851 to £46,350  20 %
         * Higher rate £46,351 to £150,000 40 %
         * Additional rate over £150,000   45 %
         * */
         static double CalculateTaxBands(double grossIncome)
         {
            double percentageTax = 0;

            if(grossIncome >= 11851 && grossIncome <= 46350)
            {
                percentageTax = 0.20;
            }else if(grossIncome >= 46351 && grossIncome <= 150000)
            {
                percentageTax = 0.40;
            }else if(grossIncome > 150000)
            {
                percentageTax = 0.45;
            }

            return percentageTax;
         }

         static double CalculateNetIncome(double grossIncome, double taxPaid, double nationalInsuarance)
         {
            return grossIncome - taxPaid - nationalInsuarance;
         }
    }


}
