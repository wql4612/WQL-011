using System;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = {
                { 1, 2, 3, 4 },
                { 5, 1, 2, 3 },
                { 9, 5, 1, 2 }
            };

            Console.WriteLine(IsToeplitzMatrix(matrix) ? "True" : "False");
        }

        static bool IsToeplitzMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int row = 0; row < rows - 1; row++)
            {
                for (int col = 0; col < cols - 1; col++)
                {
                    if (matrix[row, col] != matrix[row + 1, col + 1])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
