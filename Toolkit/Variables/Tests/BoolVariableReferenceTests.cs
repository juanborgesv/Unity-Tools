using JuanBorges.UnityTools;
using NUnit.Framework;
using UnityEngine;

public class BoolVariableReferenceTests
{
    private BoolReference boolRef;

    [SetUp]
    public void Setup()
    {
        boolRef = new()
        {
            UseConstant = false,
            Variable = ScriptableObject.CreateInstance<BoolVariableSO>()
        };
    }

    [TearDown]
    public void TearDown()
    {
        if (boolRef != null)
        {
            if (boolRef.Variable != null)
                Object.DestroyImmediate(boolRef.Variable);
        }
    }

    [Test]
    public void ImplicitConversion_ExtractsCorrectValue()
    {
        boolRef.Value = true;

        // Tests the implicit operator: BoolVariableSO -> bool
        bool primitiveValue = boolRef;

        Assert.IsTrue(primitiveValue, "Implicit conversion failed to extract the correct .Value.");
    }

    [Test]
    public void LogicalNegation_InvertsValueCorrectly()
    {
        boolRef.Value = true;

        // Tests unary '!' operator
        bool result = !boolRef;

        Assert.IsFalse(result, "Logical negation failed to invert true to false.");

        boolRef.Value = false;
        result = !boolRef;

        Assert.IsTrue(result, "Logical negation failed to invert false to true.");
    }

    [Test]
    public void LogicalNegation_NullReference_ReturnsTrue()
    {
        BoolVariableSO nullVar = null;

        // A null ScriptableObject defaults to false, so !null should evaluate to true
        bool result = !nullVar;

        Assert.IsTrue(result, "Logical negation on a null reference should safely return true (negation of default false).");
    }

    [Test]
    public void EqualityOperator_ComparesWithBoolCorrectly()
    {
        boolRef.Value = true;

        // Tests both orders of the overloaded '==' operator
        Assert.IsTrue(boolRef == true, "ScriptableObject should equal its underlying true value.");
        Assert.IsTrue(true == boolRef, "Reversed comparison should also evaluate to true.");
        Assert.IsFalse(boolRef == false, "True ScriptableObject should not equal false.");
    }

    [Test]
    public void InequalityOperator_ComparesWithBoolCorrectly()
    {
        boolRef.Value = true;

        // Tests the overloaded '!=' operator
        Assert.IsTrue(boolRef != false, "True ScriptableObject should trigger inequality against false.");
        Assert.IsFalse(boolRef != true, "Equal values should not trigger inequality.");
    }

    [Test]
    public void OnValueChanged_InvokesEventWhenValueSet()
    {
        bool callbackValue = false;
        boolRef.Variable.OnValueChanged += (val) => callbackValue = val;

        boolRef.Value = true;

        Assert.IsTrue(callbackValue, "OnValueChanged event did not broadcast the updated boolean value.");
    }

    [Test]
    public void NullReference_EqualityCheck_DoesNotThrowException()
    {
        BoolVariableSO nullVar = null;

        // Verifies that a null ScriptableObject treats itself as default(bool) -> false
        bool equalsFalse = (nullVar == false);
        bool equalsTrue = (nullVar == true);

        Assert.IsTrue(equalsFalse, "A null ScriptableObject should evaluate as equivalent to default/false.");
        Assert.IsFalse(equalsTrue, "A null ScriptableObject should not equal true.");
    }
}
