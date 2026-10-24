using JuanBorges.UnityTools;
using NUnit.Framework;
using UnityEngine;

public class Vector3VariableReferenceTests
{
    private Vector3Reference vectorRef;

    [SetUp]
    public void Setup()
    {
        vectorRef = new()
        {
            UseConstant = false,
            Variable = ScriptableObject.CreateInstance<Vector3VariableSO>()
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
        vectorRef.Variable.Value = new Vector3(5.5f, 10.2f, -3.1f);

        // Tests the implicit operator: Vector3Reference -> Vector3
        Vector3 primitiveValue = vectorRef;

        Assert.AreEqual(5.5f, primitiveValue.x, 0.0001f, "Implicit conversion failed for X component.");
        Assert.AreEqual(10.2f, primitiveValue.y, 0.0001f, "Implicit conversion failed for Y component.");
        Assert.AreEqual(-3.1f, primitiveValue.z, 0.0001f, "Implicit conversion failed for Z component.");
    }

    [Test]
    public void VectorAddition_WithVector3AndReference_WorksCorrectly()
    {
        vectorRef.Value = new Vector3(1.0f, 2.0f, 3.0f);
        Vector3Reference otherRef = new Vector3Reference(new Vector3(4.0f, 5.0f, 6.0f));

        // Test Reference + Raw Vector3
        Vector3 res1 = vectorRef + new Vector3(10.0f, 20.0f, 30.0f);
        Assert.AreEqual(new Vector3(11.0f, 22.0f, 33.0f), res1, "Reference + Vector3 addition failed.");

        // Test Raw Vector3 + Reference
        Vector3 res2 = new Vector3(5.0f, 5.0f, 5.0f) + vectorRef;
        Assert.AreEqual(new Vector3(6.0f, 7.0f, 8.0f), res2, "Vector3 + Reference addition failed.");

        // Test Reference + Reference
        Vector3 res3 = vectorRef + otherRef;
        Assert.AreEqual(new Vector3(5.0f, 7.0f, 9.0f), res3, "Reference + Reference addition failed.");
    }

    [Test]
    public void VectorSubtraction_WorksCorrectly()
    {
        vectorRef.Value = new Vector3(10.0f, 15.0f, 20.0f);
        Vector3Reference otherRef = new Vector3Reference(new Vector3(2.0f, 3.0f, 4.0f));

        // Test Reference - Raw Vector3
        Vector3 res1 = vectorRef - new Vector3(4.0f, 5.0f, 6.0f);
        Assert.AreEqual(new Vector3(6.0f, 10.0f, 14.0f), res1, "Reference - Vector3 subtraction failed.");

        // Test Raw Vector3 - Reference
        Vector3 res2 = new Vector3(20.0f, 30.0f, 40.0f) - vectorRef;
        Assert.AreEqual(new Vector3(10.0f, 15.0f, 20.0f), res2, "Vector3 - Reference subtraction failed.");

        // Test Reference - Reference
        Vector3 res3 = vectorRef - otherRef;
        Assert.AreEqual(new Vector3(8.0f, 12.0f, 16.0f), res3, "Reference - Reference subtraction failed.");
    }

    [Test]
    public void ScalarMultiplicationAndDivision_WorksCorrectly()
    {
        vectorRef.Value = new Vector3(2.0f, 4.0f, 6.0f);

        // Reference * float
        Vector3 resMul1 = vectorRef * 2.5f;
        Assert.AreEqual(new Vector3(5.0f, 10.0f, 15.0f), resMul1, "Reference * float multiplication failed.");

        // float * Reference
        Vector3 resMul2 = 3.0f * vectorRef;
        Assert.AreEqual(new Vector3(6.0f, 12.0f, 18.0f), resMul2, "float * Reference multiplication failed.");

        // Reference / float
        Vector3 resDiv = vectorRef / 2.0f;
        Assert.AreEqual(new Vector3(1.0f, 2.0f, 3.0f), resDiv, "Reference / float division failed.");
    }

    [Test]
    public void UnaryNegation_InvertsVectorCorrectly()
    {
        vectorRef.Value = new Vector3(4.0f, -6.5f, 2.2f);

        // Tests unary minus operator: -vectorRef
        Vector3 result = -vectorRef;

        Assert.AreEqual(-4.0f, result.x, 0.0001f, "Unary negation failed on X component.");
        Assert.AreEqual(6.5f, result.y, 0.0001f, "Unary negation failed on Y component.");
        Assert.AreEqual(-2.2f, result.z, 0.0001f, "Unary negation failed on Z component.");
    }

    [Test]
    public void UnaryNegation_NullReference_ReturnsZeroVector()
    {
        Vector3Reference nullRef = null;

        Vector3 result = -nullRef;

        Assert.AreEqual(Vector3.zero, result, "Unary negation on a null reference should safely return Vector3.zero.");
    }

    [Test]
    public void OnValueChanged_InvokesEventWhenValueSet()
    {
        Vector3 callbackValue = Vector3.zero;
        vectorRef.Variable.OnValueChanged += (val) => callbackValue = val;

        // Setting through the reference wrapper should trigger the SO's event
        vectorRef.Value = new Vector3(7f, 14f, 21f);

        Assert.AreEqual(new Vector3(7f, 14f, 21f), callbackValue, "OnValueChanged event did not broadcast through the reference wrapper.");
    }

    [Test]
    public void NullReference_EqualityCheck_DoesNotThrowException()
    {
        Vector3Reference nullRef = null;

        // Verify that ReferenceEquals safely identifies the unassigned wrapper
        Assert.IsTrue(ReferenceEquals(nullRef, null), "ReferenceEquals should confirm the Vector3Reference is null.");
    }
}
