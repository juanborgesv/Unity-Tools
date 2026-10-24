using JuanBorges.UnityTools;
using NUnit.Framework;
using UnityEngine;

public class IntVariableReferenceTests
{
    private IntReference intRef;

    [SetUp]
    public void Setup()
    {
        intRef = new ()
        {
            UseConstant = false,
            Variable = ScriptableObject.CreateInstance<IntVariableSO>()
        };
    }

    [TearDown]
    public void TearDown()
    {
        if (intRef != null)
        {
            if (intRef.Variable != null)
                Object.DestroyImmediate(intRef.Variable);
        }
    }

    [Test]
    public void ImplicitConversion_ExtractsCorrectValue()
    {
        intRef.Value = 42;

        // Tests the implicit operator: IntReference -> int
        int primitiveValue = intRef;

        Assert.AreEqual(42, primitiveValue, "Implicit conversion failed to extract the correct .Value.");
    }

    [Test]
    public void ImplicitConversion_AllowsMathOperations()
    {
        intRef.Value = 10;

        // Implicitly converts IntReference to int, then performs math
        int result = 10 + intRef - 5;

        Assert.AreEqual(15, result, "Implicit conversion failed when used inside an arithmetic expression.");
    }

    [Test]
    public void EqualityOperator_ComparesWithIntCorrectly()
    {
        intRef.Value = 100;

        // Tests both orders of the overloaded '==' operator
        Assert.IsTrue(intRef == 100, "ScriptableObject should equal its underlying integer value.");
        Assert.IsTrue(100 == intRef, "Reversed comparison should also evaluate to true.");
        Assert.IsFalse(intRef == 50, "Different values should not match.");
    }

    [Test]
    public void InequalityOperator_ComparesWithIntCorrectly()
    {
        intRef.Value = 100;

        // Tests the overloaded '!=' operator
        Assert.IsTrue(intRef != 50, "Different values should trigger inequality.");
        Assert.IsFalse(intRef != 100, "Equal values should not trigger inequality.");
    }

    [Test]
    public void GreaterThanOperator_ComparesCorrectly()
    {
        intRef.Value = 15;

        // Tests '>' using implicit conversion to int
        Assert.IsTrue(intRef > 10, "15 should be greater than 10.");
        Assert.IsFalse(intRef > 20, "15 should not be greater than 20.");
    }

    [Test]
    public void LessThanOperator_ComparesCorrectly()
    {
        intRef.Value = 5;

        // Tests '<' using implicit conversion to int
        Assert.IsTrue(intRef < 10, "5 should be less than 10.");
        Assert.IsFalse(intRef < 2, "5 should not be less than 2.");
    }

    [Test]
    public void UnaryNegation_InvertsValueCorrectly()
    {
        intRef.Value = 10;

        int result = -intRef;

        Assert.AreEqual(-10, result, "Unary negation failed to invert a positive integer.");
    }

    [Test]
    public void UnaryNegation_WithNegativeValue_ReturnsPositive()
    {
        intRef.Value = -5;

        int result = -intRef;

        Assert.AreEqual(5, result, "Unary negation failed to invert a negative integer.");
    }

    [Test]
    public void UnaryNegation_NullReference_ReturnsZero()
    {
        IntVariableSO nullVar = null;

        int result = -nullVar;

        Assert.AreEqual(0, result, "Unary negation on a null reference should safely return zero.");
    }

    [Test]
    public void OnValueChanged_InvokesEventWhenValueSet()
    {
        int callbackValue = -1;
        intRef.Variable.OnValueChanged += (val) => callbackValue = val;

        intRef.Value = 77;

        Assert.AreEqual(77, callbackValue, "OnValueChanged event did not broadcast the updated value.");
    }

    [Test]
    public void NullReference_EqualityCheck_DoesNotThrowException()
    {
        IntVariableSO nullVar = null;

        // Verifies that Unity-safe null checks in the operator prevent NullReferenceExceptions
        bool equalsZero = (nullVar == 0);
        bool equalsTen = (nullVar == 10);

        Assert.IsTrue(equalsZero, "A null ScriptableObject should evaluate as equivalent to default/zero.");
        Assert.IsFalse(equalsTen, "A null ScriptableObject should not match non-zero values.");
    }
}