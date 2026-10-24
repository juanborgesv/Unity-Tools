using JuanBorges.UnityTools;
using NUnit.Framework;
using UnityEngine;

public class floatRefiableReferenceTests
{
    private FloatReference floatRef;

    [SetUp]
    public void Setup()
    {
        floatRef = new()
        {
            UseConstant = false,
            Variable = ScriptableObject.CreateInstance<FloatVariableSO>()
        };
    }

    [TearDown]
    public void TearDown()
    {
        if (floatRef != null)
        {
            if (floatRef.Variable != null)
                Object.DestroyImmediate(floatRef.Variable);
        }
    }

    [Test]
    public void ImplicitConversion_ExtractsCorrectValue()
    {
        floatRef.Value = 3.14f;

        // Tests the implicit operator: floatRefiableSO -> float
        float primitiveValue = floatRef;

        Assert.AreEqual(3.14f, primitiveValue, 0.0001f, "Implicit conversion failed to extract the correct .Value.");
    }

    [Test]
    public void ImplicitConversion_AllowsMathOperations()
    {
        floatRef.Value = 10.5f;

        // Implicitly converts floatRef to float, then performs math
        float result = floatRef + 2.5f;

        Assert.AreEqual(13.0f, result, 0.0001f, "Implicit conversion failed when used inside an arithmetic expression.");
    }

    [Test]
    public void ImplicitConversion_AllowsAddition()
    {
        floatRef.Value = 10.5f;
        float result = floatRef + 2.5f;
        Assert.AreEqual(13.0f, result, 0.0001f, "Implicit conversion failed during addition.");
    }

    [Test]
    public void ImplicitConversion_AllowsSubtraction()
    {
        floatRef.Value = 10.5f;
        float result = floatRef - 2.5f;
        Assert.AreEqual(8.0f, result, 0.0001f, "Implicit conversion failed during subtraction.");
    }

    [Test]
    public void ImplicitConversion_AllowsMultiplication()
    {
        floatRef.Value = 4.0f;
        float result = floatRef * 2.5f;
        Assert.AreEqual(10.0f, result, 0.0001f, "Implicit conversion failed during multiplication.");
    }

    [Test]
    public void ImplicitConversion_AllowsDivision()
    {
        floatRef.Value = 10.0f;
        float result = floatRef / 4.0f;
        Assert.AreEqual(2.5f, result, 0.0001f, "Implicit conversion failed during division.");
    }

    [Test]
    public void UnaryNegation_InvertsValueCorrectly()
    {
        floatRef.Value = 10.5f;

        // Tests unary minus operator: -floatRef
        float result = -floatRef;

        Assert.AreEqual(-10.5f, result, 0.0001f, "Unary negation failed to invert a positive value.");
    }

    [Test]
    public void UnaryNegation_WithNegativeValue_ReturnsPositive()
    {
        floatRef.Value = -5.0f;

        float result = -floatRef;

        Assert.AreEqual(5.0f, result, 0.0001f, "Unary negation failed to invert a negative value.");
    }

    [Test]
    public void UnaryNegation_NullReference_ReturnsZero()
    {
        FloatVariableSO nullVar = null;

        // Uses our safety guard to ensure it doesn't crash on null
        float result = -nullVar;

        Assert.AreEqual(0f, result, 0.0001f, "Unary negation on a null reference should safely return zero.");
    }

    [Test]
    public void GreaterThanOperator_ComparesCorrectly()
    {
        floatRef.Value = 15.2f;

        // Tests '>' using implicit conversion to float
        Assert.IsTrue(floatRef > 10.0f, "15.2 should be greater than 10.0.");
        Assert.IsFalse(floatRef > 20.0f, "15.2 should not be greater than 20.0.");
    }

    [Test]
    public void LessThanOperator_ComparesCorrectly()
    {
        floatRef.Value = 4.5f;

        // Tests '<' using implicit conversion to float
        Assert.IsTrue(floatRef < 10.0f, "4.5 should be less than 10.0.");
        Assert.IsFalse(floatRef < 2.0f, "4.5 should not be less than 2.0.");
    }

    [Test]
    public void EqualityOperator_ComparesWithFloatCorrectly()
    {
        floatRef.Value = 42.5f;

        // Tests both orders of the overloaded '==' operator
        Assert.IsTrue(floatRef == 42.5f, "ScriptableObject should equal its underlying float value.");
        Assert.IsTrue(42.5f == floatRef, "Reversed comparison should also evaluate to true.");
        Assert.IsFalse(floatRef == 10.0f, "Different values should not match.");
    }

    [Test]
    public void InequalityOperator_ComparesWithFloatCorrectly()
    {
        floatRef.Value = 42.5f;

        // Tests the overloaded '!=' operator
        Assert.IsTrue(floatRef != 10.0f, "Different values should trigger inequality.");
        Assert.IsFalse(floatRef != 42.5f, "Equal values should not trigger inequality.");
    }

    [Test]
    public void OnValueChanged_InvokesEventWhenValueSet()
    {
        float callbackValue = -1f;
        floatRef.Variable.OnValueChanged += (val) => callbackValue = val;

        floatRef.Value = 99.9f;

        Assert.AreEqual(99.9f, callbackValue, 0.0001f, "OnValueChanged event did not broadcast the updated value.");
    }

    [Test]
    public void NullReference_EqualityCheck_DoesNotThrowException()
    {
        FloatVariableSO nullVar = null;

        // Verifies that Unity-safe null checks handle default comparisons for floats (0f)
        bool equalsZero = (nullVar == 0f);
        bool equalsTen = (nullVar == 10.0f);

        Assert.IsTrue(equalsZero, "A null ScriptableObject should evaluate as equivalent to default/zero.");
        Assert.IsFalse(equalsTen, "A null ScriptableObject should not match non-zero values.");
    }
}
