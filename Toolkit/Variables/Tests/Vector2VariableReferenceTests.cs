using JuanBorges.UnityTools;
using NUnit.Framework;
using UnityEngine;

public class Vector2VariableReferenceTests
{

    private Vector2Reference vectorRef;

    [SetUp]
    public void Setup()
    {
        vectorRef = new()
        {
            UseConstant = false,
            Variable = ScriptableObject.CreateInstance<Vector2VariableSO>()
        };
    }

    [TearDown]
    public void TearDown()
    {
        if (vectorRef != null)
        {
            if (!ReferenceEquals(vectorRef.Variable, null))
                Object.DestroyImmediate(vectorRef.Variable);
        }
    }

    [Test]
    public void ImplicitConversion_ExtractsCorrectValue()
    {
        vectorRef.Variable.Value = new Vector2(5.5f, 10.2f);

        // Tests the implicit operator: Vector2Reference -> Vector2
        Vector2 primitiveValue = vectorRef;

        Assert.AreEqual(5.5f, primitiveValue.x, 0.0001f, "Implicit conversion failed for X component.");
        Assert.AreEqual(10.2f, primitiveValue.y, 0.0001f, "Implicit conversion failed for Y component.");
    }

    [Test]
    public void VectorAddition_WithVector2AndReference_WorksCorrectly()
    {
        vectorRef.Value = new Vector2(1.0f, 2.0f);
        Vector2Reference otherRef = new Vector2Reference(new Vector2(3.0f, 4.0f));

        // Test Reference + Raw Vector2
        Vector2 res1 = vectorRef + new Vector2(10.0f, 20.0f);
        Assert.AreEqual(new Vector2(11.0f, 22.0f), res1, "Reference + Vector2 addition failed.");

        // Test Raw Vector2 + Reference
        Vector2 res2 = new Vector2(5.0f, 5.0f) + vectorRef;
        Assert.AreEqual(new Vector2(6.0f, 7.0f), res2, "Vector2 + Reference addition failed.");

        // Test Reference + Reference
        Vector2 res3 = vectorRef + otherRef;
        Assert.AreEqual(new Vector2(4.0f, 6.0f), res3, "Reference + Reference addition failed.");
    }

    [Test]
    public void VectorSubtraction_WorksCorrectly()
    {
        vectorRef.Value = new Vector2(10.0f, 15.0f);
        Vector2Reference otherRef = new Vector2Reference(new Vector2(2.0f, 3.0f));

        // Test Reference - Raw Vector2
        Vector2 res1 = vectorRef - new Vector2(4.0f, 5.0f);
        Assert.AreEqual(new Vector2(6.0f, 10.0f), res1, "Reference - Vector2 subtraction failed.");

        // Test Raw Vector2 - Reference
        Vector2 res2 = new Vector2(20.0f, 30.0f) - vectorRef;
        Assert.AreEqual(new Vector2(10.0f, 15.0f), res2, "Vector2 - Reference subtraction failed.");

        // Test Reference - Reference
        Vector2 res3 = vectorRef - otherRef;
        Assert.AreEqual(new Vector2(8.0f, 12.0f), res3, "Reference - Reference subtraction failed.");
    }

    [Test]
    public void ScalarMultiplicationAndDivision_WorksCorrectly()
    {
        vectorRef.Value = new Vector2(2.0f, 4.0f);

        // Reference * float
        Vector2 resMul1 = vectorRef * 2.5f;
        Assert.AreEqual(new Vector2(5.0f, 10.0f), resMul1, "Reference * float multiplication failed.");

        // float * Reference
        Vector2 resMul2 = 3.0f * vectorRef;
        Assert.AreEqual(new Vector2(6.0f, 12.0f), resMul2, "float * Reference multiplication failed.");

        // Reference / float
        Vector2 resDiv = vectorRef / 2.0f;
        Assert.AreEqual(new Vector2(1.0f, 2.0f), resDiv, "Reference / float division failed.");
    }

    [Test]
    public void UnaryNegation_InvertsVectorCorrectly()
    {
        vectorRef.Value = new Vector2(4.0f, -6.5f);

        // Tests unary minus operator: -vectorRef
        Vector2 result = -vectorRef;

        Assert.AreEqual(-4.0f, result.x, 0.0001f, "Unary negation failed on X component.");
        Assert.AreEqual(6.5f, result.y, 0.0001f, "Unary negation failed on Y component.");
    }

    [Test]
    public void UnaryNegation_NullReference_ReturnsZeroVector()
    {
        Vector2Reference nullRef = null;

        Vector2 result = -nullRef;

        Assert.AreEqual(Vector2.zero, result, "Unary negation on a null reference should safely return Vector2.zero.");
    }

    [Test]
    public void OnValueChanged_InvokesEventWhenValueSet()
    {
        Vector2 callbackValue = Vector2.zero;
        vectorRef.Variable.OnValueChanged += (val) => callbackValue = val;

        // Setting through the reference wrapper should trigger the SO's event
        vectorRef.Value = new Vector2(7f, 14f);

        Assert.AreEqual(new Vector2(7f, 14f), callbackValue, "OnValueChanged event did not broadcast through the reference wrapper.");
    }

    [Test]
    public void NullReference_EqualityCheck_DoesNotThrowException()
    {
        Vector2Reference nullRef = null;

        // Verify that ReferenceEquals safely identifies the unassigned wrapper
        Assert.IsTrue(ReferenceEquals(nullRef, null), "ReferenceEquals should confirm the Vector2Reference is null.");
    }
}
