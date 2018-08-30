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
            double income = Double.Parse(Console.ReadLine());

            // Output income
            Console.WriteLine("Total taxable is: " + calculateTaxable(income) );

            // Wait for user to end program
            Console.ReadKey();
        }

        /**
         * Calculates the amount taxable
         **/
        static double calculateTaxable(double amount)
        {
            // basic allowance which is not taxed
            double basicPersonalAllowance = 11850.0;

            double taxable = amount - basicPersonalAllowance;

            // Now check which bands are applicable

            return taxable;
        }
    }
}
