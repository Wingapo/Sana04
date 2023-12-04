internal class Program
{
    private static void Main(string[] args)
    {
        int rows, cols, value;
        Random random = new();

        Console.WriteLine("Enter sizes for 2d array: ");

        rows = GetNaturalNumber("rows = ");
        cols = GetNaturalNumber("columns = ");
            
        int[,] array = new int[rows, cols];

        Generate2dArray(array, random, -2, 11);
        Console.WriteLine("\nGenerated 2d array:");
        Print2dArray(array);

        Console.WriteLine("\nNumber of positive elements: " + NumberOfPositive(array));

        value = MaxValueOccursSeveralTimes(array);
        Console.WriteLine("Max value which occurs several times: " +
             ((value == int.MinValue) ? "null" : value));

        Console.WriteLine("Number of rows without zeros: " + RowsWithoutZeros(array));
        Console.WriteLine("Number of columns with zeros: " + ColsWithZeros(array));
        Console.WriteLine("Index of first row with most duplicates: " + RowWithMostDupl(array));

        Console.WriteLine("Product of elements for rows without negative elements:");
        PrintDictionary(ProdOfNonNegativeRows(array), "\trow index ");

        value = MaxDiagonalSumUnderMain(array);
        Console.WriteLine("Max elements sum of diagonals parallel to main: " + 
            ((value == int.MinValue) ? "null" : value));

        Console.WriteLine("Sum of elements for columns without negative elements:");
        PrintDictionary(SumOfNonNegativeCols(array), "\tcolumn index ");

        Console.WriteLine("Min elements sum by absolute value of diagonals parallel to side: " +
            MinAbsDiagonalSumAboveSide(array));

        Console.WriteLine("Sum of elements for columns with at least one negative element:");
        PrintDictionary(ColSumWithNegative(array), "\tcolumn index ");

        Console.WriteLine("Array after transposition:");
        Print2dArray(Transpose(array));
    }

    private static int GetInteger(string text = "")
    {
        int integer;
        bool valid;

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
        int number;
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

    private static int NumberOfPositive(int[,] array)
    {
        int count = 0;
        for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
                if (array[i, j] > 0) count++;

        return count;
    }

    private static int MaxValueOccursSeveralTimes(int[,] array)
    {
        int rows = array.GetLength(0), cols = array.GetLength(1);

        int[] copy = new int[rows * cols];
        for (int i = 0; i < rows * cols; i++)
            copy[i] = array[i / cols, i % cols];

        Array.Sort<int>(copy);

        for (int i = rows * cols - 1; i > 0; i--)
            if (copy[i] == copy[i - 1]) return copy[i];

        return int.MinValue;
    }

    private static int RowsWithoutZeros(int[,] array)
    {
        int count = 0;

        for (int i = 0; i < array.GetLength(0); i++)
        {
            count++;
            for (int j = 0; j < array.GetLength(1); j++)
                if (array[i, j] == 0)
                {
                    count--;
                    break;
                }
        }
        return count;
    }

    private static int ColsWithZeros(int[,] array)
    {
        int count = 0;

        for(int i = 0; i < array.GetLength(1); i ++)
            for(int j = 0; j < array.GetLength(0); j ++)
                if (array[j, i] == 0)
                {
                    count++;
                    break;
                }
        return count;
    }

    private static int RowWithMostDupl(int[,] array)
    {
        int index = -1, max = 0, current;

        for (int row = 0; row < array.GetLength(0); row++) {
            for (int col = 0; col < array.GetLength(1); col++)
            {
                current = -1;
                for (int e = 0; e < array.GetLength(1); e++)
                {
                    if (array[row, col] == array[row, e])
                        current++;
                }
                if (current > max)
                {
                    max = current;
                    index = row;
                }
            }
        }
        return index;
    }

    private static Dictionary<int, int> ProdOfNonNegativeRows(int[,] array)
    {
        Dictionary<int, int> result = new();

        bool save;
        int currentProd;

        for (int i = 0; i < array.GetLength(0); i++) {
            currentProd = 1;
            save = true;
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] >= 0)
                    currentProd *= array[i, j];
                else
                {
                    save = false;
                    break;
                }
            }
            if (save) result.Add(i, currentProd);
        }
        return result;
    }

    public static void PrintDictionary(Dictionary<int, int> dict, string keyText = "")
    {
        foreach (KeyValuePair<int, int> pair in dict)
            Console.WriteLine(keyText + pair.Key + ": " + pair.Value);
    }

    private static int MaxDiagonalSumUnderMain(int[,] array)
    {
        int rows = array.GetLength(0), cols = array.GetLength(1);

        if (rows <= cols) return int.MinValue;

        int max = 0, current;
        for (int i = 1; i <= rows - cols; i++) {
            current = 0;
            for (int j = 0; j < cols; j++)
                current += array[j + i, j];
            if (i == 1) max = current;
            else if (current > max) max = current;
        }

        return max;
    }

    private static Dictionary<int, int> SumOfNonNegativeCols(int[,] array)
    {
        Dictionary<int, int> result = new();

        bool save;
        int currentSum;

        for (int i = 0; i < array.GetLength(1); i++)
        {
            currentSum = 0;
            save = true;
            for (int j = 0; j < array.GetLength(0); j++)
            {
                if (array[j, i] >= 0)
                    currentSum += array[j, i];
                else
                {
                    save = false;
                    break;
                }
            }
            if (save) result.Add(i, currentSum);
        }
        return result;
    }

    private static int MinAbsDiagonalSumAboveSide(int[,] array)
    {
        int rows = array.GetLength(0), cols = array.GetLength(1);

        if (rows <= cols) return -1;

        int min = 0, current;
        for (int i = 1; i <= rows - cols; i++) {
            current = 0;
            for (int j = 0; j < cols; j++)
                current += Math.Abs(array[rows - j - i - 1, j]);
            if (i == 2) min = current;
            else if (current < min) min = current;
        }
        return min;
    }

    private static Dictionary<int, int> ColSumWithNegative(int[,] array)
    {
        Dictionary<int, int> result = new();

        int currentSum, negCount;

        for (int i = 0; i < array.GetLength(1); i++)
        {
            currentSum = 0;
            negCount = 0;
            for (int j = 0; j < array.GetLength(0); j++)
            {
                currentSum += array[j, i];
                if (array[j, i] < 0) negCount++;
            }
            if (negCount > 0) result.Add(i, currentSum);
        }
        return result;
    }

    private static int[,] Transpose(int[,] array)
    {
        int rows = array.GetLength(0), cols = array.GetLength(1);
        int[,] result = new int[cols, rows];

        for (int i = 0; i < cols; i ++) 
            for (int j = 0; j < rows; j++)
                result[i, j] = array[j, i];

        return result;
    }
}