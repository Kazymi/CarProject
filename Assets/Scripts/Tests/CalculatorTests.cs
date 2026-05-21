using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class CalculatorTests
{
    [Test]
    public void CalculatorTestForPlus()
    {
        Calculator calculator = new Calculator();
        int x = 5;
        int y = 5;
        
        int result = 10;
        int actualResult = calculator.PlusCalculator(x, y);
        
        Assert.That(actualResult, Is.EqualTo(result));
    }
    
    [Test]
    public void CalculatorTestForReduce()
    {
        Calculator calculator = new Calculator();
        int x = 5;
        int y = 5;
        
        int result = 0;
        int actualResult = calculator.MinusCalculator(x, y);
        
        Assert.That(actualResult, Is.EqualTo(result));
    }
    
    
    public class Calculator
    {
        public int PlusCalculator(int x, int y)
        {
            return x + y;
        }

        public int MinusCalculator(int x, int y)
        {
            return x - y;
        }
    }
}