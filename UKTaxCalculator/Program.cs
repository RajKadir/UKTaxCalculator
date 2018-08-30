using System;

namespace UKTaxCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Info about program
            PrintWelcomeMessage();
            // Main program execution
            CalculateTax();
        }

        static void CalculateTax()
        {
            // Ask for income
            Console.WriteLine("What is your yearly income? (all numbers e.g. 44000)");

            // Store income from user
            double grossIncome = Double.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------");

            double taxable = CalculateTaxable(grossIncome);
            double highestTaxPercentage = CalculateTaxBands(grossIncome);
            double taxPaid = CalculateIncomeTax(taxable, highestTaxPercentage);
            double weeklyWage = CalculateWeeklyWage(grossIncome);
            double nationalInsurance = CalculateWeeklyNationalInsurance(weeklyWage);


            // Output income
            Console.WriteLine("Your Total Taxable: " + taxable);
            Console.WriteLine("Your Highest tax band: " + highestTaxPercentage);
            Console.WriteLine("Yearly Tax paid: " + taxPaid);

            
            Console.WriteLine("Weekly National Insurance: " + Round(nationalInsurance));

            

            Console.WriteLine("Yearly National Insurance: " + Round(nationalInsurance * 52));
            Console.WriteLine("You take home: " + CalculateNetIncome(grossIncome, taxPaid, nationalInsurance*52));

            // Wait for user to end program
            Console.ReadKey();
        }

        static string Round(double val)
        {
            return String.Format("{0:0.00}", val); ;
        }

        static void PrintWelcomeMessage()
        {
            // String interpolation
            string version = "0.0.5";
            string author = "Raj";
            string email = "raj.nry.k@gmail.com";

            // Intro text
            Console.WriteLine("Welcome to my UK tax calculator (except Scotland)");
            Console.WriteLine($"author: {author}, version: {version}, email: {email} \n");
        }

        /**
         * For example, if you earn £1,000 a week, you pay:
         * nothing on the first £162
         * 12% (£87.60) on the next £730
         * 2% (£2.16) on the next £108.
         **/
        static double CalculateWeeklyNationalInsurance(double weeklyWage)
        {
            double nationalInsurance = 0;
            double weeklyAllowance = 162;

            // If our wekly wage is greater than allowance we are eligible for tax
            if(weeklyWage > weeklyAllowance)
            {
                // on the first 730 after (162) tax by 12%
                double taxable = weeklyWage - weeklyAllowance;

                // do we have values fitting in this range
                if(taxable - 730 < 0)
                {
                    // apply 12% tax on next 730
                    nationalInsurance = (taxable) * 0.12;
                }
                else
                {
                    // apply a 2% tax on next 108
                    nationalInsurance += CalculateTwoPercentNI(nationalInsurance, taxable);
                }
            }

            return nationalInsurance;
        }

        static double CalculateTwoPercentNI(double nationalInsurance, double taxable)
        {
            // Apply the original tax which maxes on 730
            nationalInsurance = (730) * 0.12;

            // Calculate our new taxable amount (take off 730)
            double twoTax = taxable - 730;

            // Apply another tax
            nationalInsurance += (twoTax) * 0.02;

            return nationalInsurance;
        }

        static double CalculateIncomeTax(double taxableAmount, double taxPercentage)
        {
            // Depending on the tax Percentage we need to work out which bands we apply to
            double incomeTax = 0;

            // Firstly apply 20% tax on the first 34,499
            if(taxableAmount - 34999 < 0)
            {
                incomeTax += CalculateTwentyPercentTax(taxableAmount);
            }
            else
            {
                // its higher (apply max amount)
                incomeTax += 34999 * 0.2;
                Console.WriteLine("20% income tax applied: " + incomeTax);

                // Now try and add a 40% tax with the next
                if(taxableAmount < 150000)
                {
                    incomeTax += CalculateFortyPercentTax(taxableAmount);
                }
                else
                {
                    // take off the previous taxableAmount
                    taxableAmount -= 34999;

                    // Apply (maximum amount) 40%
                    incomeTax += CalculateFortyPercentTax(103649);

                    // take off previous 
                    taxableAmount -= 103649;

                    // Now apply 0.45% on the rest over 150k
                    incomeTax += CalculateFortyFivePercentTax(taxableAmount);
                }
            }
            return incomeTax;
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

                // Cannot have negative tax
                if (taxable < 0) taxable = 0;
            }

            return taxable;
        }

        static double CalculateTwentyPercentTax(double taxableAmount)
        {
            double twentyTax = taxableAmount * 0.2;
            Console.WriteLine("20% income tax applied: " + twentyTax);
            return twentyTax;
        }

        static double CalculateFortyPercentTax(double taxableAmount)
        {
            double fortyTax = taxableAmount * 0.4;
            Console.WriteLine("40% income tax applied: " + fortyTax * 0.4);
            return fortyTax;
        }

        static double CalculateFortyFivePercentTax(double taxableAmount)
        {
            double fortfiveTax = taxableAmount * 0.45;
            Console.WriteLine("45% income tax applied: " + taxableAmount * 0.45);
            return fortfiveTax;
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
