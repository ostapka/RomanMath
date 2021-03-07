using NUnit.Framework;
using RomanMath.Impl;
using System;

namespace RomanMath.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[TestCase("IV+II/V")]
		[TestCase("ABC")]
		public void Evaluate_ArgumentExceptin(string expression)
		{
			Assert.Throws<ArgumentException>(() => Service.Evaluate(expression));
		}
		[Test]
		public void Evaluate_ArgumentNullExceptin()
		{
			Assert.Throws<ArgumentNullException>(() => Service.Evaluate(""));
		}
		[TestCase("IV+II*V", 14)]
		[TestCase("I+II + III", 6)]
		[TestCase("XLIX+CDXCV*MMDCLV", 1314274)]
		public void EvaluateTest(string expression, int expected)
		{
			Assert.AreEqual(expected, Service.Evaluate(expression));
		}
		[TestCase("IV", 4)]
		[TestCase("XLIX", 49)]
		[TestCase("MMDCLV", 2655)]
		public void ParseRomanFormatToNumberTest(string expression, int expected)
		{
			Assert.AreEqual(expected, Service.ParseRomanFormatToNumber(expression));
		}
		[Test]
		public void GetResultOfExpressionTest()
		{
			//arrange
			int[] numbers = { 1, 2, 3, 4, 5 };
			string[] operators = { "+", "*", "*", "+" };
			int expected = 30;
			//act & assert
			Assert.AreEqual(expected, Service.GetResultOfExpression(numbers, operators));
		}
	}
}