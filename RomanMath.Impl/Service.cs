using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RomanMath.Impl
{
    public static class Service
    {
        static private readonly Dictionary<string, int> numbers = new Dictionary<string, int>()
        {
            { "M", 1000},
            { "D", 500},
            { "C", 100},
            { "L", 50},
            { "X", 10},
            { "V", 5 },
            { "I", 1 }
        };
        static private readonly char[] separators = { '+', '-', '*' };
        static private readonly string pattern = @"^[MDCLXVI\+\-\*\s]+$";

        /// <summary>
        /// See TODO.txt file for task details.
        /// Do not change contracts: input and output arguments, method name and access modifiers
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>

        public static int Evaluate(string expression)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                if (Regex.IsMatch(expression, pattern))
                {
                    Regex regex = new Regex(@"\s+");
                    string clearExpression = regex.Replace(expression, "");
                    string[] numbersInRomanFormat = clearExpression.Split(separators);
                    string[] operators = clearExpression.Split(numbersInRomanFormat,
                        StringSplitOptions.None).Where(x => x != "").ToArray();
                    int[] numbersInDecimalFormat = numbersInRomanFormat.
                        Select(x => ParseRomanFormatToNumber(x)).ToArray();
                    return GetResultOfExpression(numbersInDecimalFormat, operators);
                }
                else throw new ArgumentException(string.Format("Expression {0} is invalid", expression));
            }
            else throw new ArgumentNullException(string.Format("Expression {0} is empty or NULL", expression));
        }
        public static int ParseRomanFormatToNumber(string romanNumber)
        {
            var collect = (romanNumber as IEnumerable<char>).
                Select((x, value) =>
                {
                    numbers.TryGetValue(x.ToString(), out value);
                    return value;
                }).ToList();
            return collect.Select((item, index) => new { Item = item, Position = index }).
                Where(x => x.Item >= collect.ElementAtOrDefault(x.Position + 1)).
                Select(x => x.Item).Sum() -
                collect.Select((item, index) => new { Item = item, Position = index }).
                Where(x => x.Item < collect.ElementAtOrDefault(x.Position + 1)).
                Select(x => x.Item).Sum();
        }
        public static int GetResultOfExpression(int[] numbersInDecimalFormat, string[] operators)
        {
            operators.Select((item, index) => new { Item = item, Pos = index }).Where(x => x.Item == "*").Select(x =>
            {
                numbersInDecimalFormat[x.Pos + 1] = numbersInDecimalFormat[x.Pos] * numbersInDecimalFormat[x.Pos + 1];
                numbersInDecimalFormat[x.Pos] = 0;
                return x;
            }).ToArray();
            operators.Select((item, index) => new { Item = item, Pos = index }).Where(x => x.Item == "-").Select(x =>
            {
                numbersInDecimalFormat[x.Pos + 1] = numbersInDecimalFormat[x.Pos + 1] * -1;
                return x;
            }).ToArray();
            return numbersInDecimalFormat.Sum();
        }
    }
}
