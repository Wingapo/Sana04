internal class Program
{
    private static void Main(string[] args)
    {
        int rows, cols;
        Random random = new Random();

        Console.WriteLine("Enter sizes for 2d array: ");

        rows = GetNaturalNumber("rows = ");
        cols = GetNaturalNumber("columns = ");
            
        int[,] array = new int[rows, cols];

        Generate2dArray(array, random, -20, 20);
        Print2dArray(array);
    }

    private static int GetInteger(string text = "")
    {
        int integer;
        bool valid = false;

        do
        {
            Console.Write(text);
            valid = int.TryParse(Console.ReadLine(), out integer);
            if (!valid) Console.WriteLine("Invalid input");
        } while (!valid);

        return integer;
    }

    private static int GetNaturalNumber(string text = "")
    {
        int number = 0;
        do
        {
            number = GetInteger(text);
            if (number <= 0) Console.WriteLine("Number must be natural");
        } while (number <= 0);

        return number;
    }

    private static void Generate2dArray(int[,] array, Random random, int min, int max)
    {
        for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
                array[i, j] = random.Next(min, max);
    }

    private static void Print2dArray(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++) {
            for (int j = 0; j < array.GetLength(1); j++)
                Console.Write("{0, 4}", array[i, j]);
            Console.WriteLine();
        }
    }
}