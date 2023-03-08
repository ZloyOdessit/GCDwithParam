using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCDwithParam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Console.WriteLine("Сколько будет чисел?");
            string input = Console.ReadLine();
            while (!InputValidator.IsNonNegativeInt(input))
            {
                Console.WriteLine("Введите положительное число:");
                input = Console.ReadLine();
            }

            int count = Convert.ToInt32(input);
            int[] array_of_numbers = new int[count];
            Console.Clear();

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Введите число:");
                int randomNumber = random.Next(int.MinValue, int.MaxValue);
                int input_random = randomNumber;
                string input_number = Convert.ToString(input_random);
                while (!InputValidator.IsNumeric(input_number))
                {
                    Console.WriteLine("Ошибка ввода! Введите число:");
                    input_number = Console.ReadLine();
                }
                array_of_numbers[i] = int.Parse(input_number);
                Console.Clear();
            }
            Console.WriteLine("Ввод завершен, изначальный массив:");
            Console.WriteLine();
            string arrStr = string.Join(", ", array_of_numbers);
            Console.WriteLine(arrStr);

            // Console.WriteLine(array_of_numbers.ToCsvString());

            CheckNonZeroNumbers(array_of_numbers);
            int gcd = FindGCD(array_of_numbers);

            Console.WriteLine("GCD (НОК)");
            Console.WriteLine(gcd);
            Console.ReadLine();

        }
        private static void CheckNonZeroNumbers(int[] numbers)
        {
            if (numbers.All(x => x == 0))
            {
                throw new ArgumentException("all numbers = 0");
            }

            int index = Array.IndexOf(numbers, int.MinValue);

            if (index != -1)
            {
                throw new ArgumentOutOfRangeException($"The array contains int.MinValue at index {index}.");
            }
        }

        private static int FindGCD(int[] numbers)
        {
            int result = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                result = FindGCD(result, numbers[i]);
            }

            return result;
        }

        private static int FindGCD(int a, int b)
        {
            while (b != 0)
            {
                int remainder = a % b;
                a = b;
                b = remainder;
            }

            return Math.Abs(a);
        }
        public class InputValidator
        {
            public static bool IsNonNegativeInt(string input)
            {
                int number;
                if (int.TryParse(input, out number))
                {
                    if (number >= 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            public static bool IsNumeric(string str)
            {
                double number;
                return double.TryParse(str, out number);
            }

        }

        /*
        public static class ArrayExtensions
        {
            public static string ToCsvString<T>(this IEnumerable<T> array)
            {
                return string.Join(", ", array);
            }
        }
        */
    }
}
