using System;
public class Program
{
    public static void Main()
    {
        int contagiousDay = 5; // TODO: create calculating algorithm

        // input reproduction rate
        Console.WriteLine("Enter Reproduction Rate:");
        int rr = int.Parse(Console.ReadLine());
        
        // input number of days to track
        Console.WriteLine("Enter Number of Days to Track:");
        int daysTracked = int.Parse(Console.ReadLine());
        
        // input population size
        Console.WriteLine("Enter Population size:");
        int pop = int.Parse(Console.ReadLine());
        
        // defining array variables
        int[] activeInfections = new int[daysTracked + 15];
        int newInfections = 1;
        int[] recovered = new int[daysTracked + 15];
        
        // entering assumed data for day 1 into arrays
        for (int a = 1; a < 15; a++)
        {
            activeInfections[a] = 1;
            recovered[a + 14] = 1;
        }
        
        // traversing through days
        for (int i = 2; i < daysTracked + 1; i++)
        {
            if (i % (contagiousDay - 1) == 1 && newInfections * rr < pop) // tests whether it is a contagious (new infection) day and ensures the algorithm doesnt go over pop
            {
                // multiply the previous newInfections by the reproduction rate to get the updated newInfections
                newInfections *= rr;
                for (int j = i; j < i + 14; j++) // traverses through the 14 days of an infectious group
                {
                    // add previous new infection
                    activeInfections[j] += newInfections; 
                }
                
                // filling recovered array based on active infections
                for (int k = i + 14; k < i + 28; k++)
                {
                    recovered[k] = activeInfections[i];
                }
            }

            if (newInfections * rr > pop) // partially broken --> day 31 onwards also might not have handled recovery of group 6
            {
                for (int l = i; l < i + 14; l++) 
                {
                    if (activeInfections[l] == 0 && recovered[l] == 0)
                    {
                        recovered[l] = pop;
                    }
                    else
                    {
                        newInfections = pop - (recovered[l] + activeInfections[l]);
                        activeInfections[l] += newInfections;
                    }
                } 
            }
            // Console.WriteLine(activeInfections[i]);
            Console.WriteLine("Day " + i + ": " + activeInfections[i] + " + " + recovered[i]);
        }
    }
}