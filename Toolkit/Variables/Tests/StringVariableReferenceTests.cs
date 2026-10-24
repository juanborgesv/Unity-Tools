using JuanBorges.UnityTools;
using NUnit.Framework;
using UnityEngine;

public class StringVariableReferenceTests
{
    private StringReference stringRef;

    [SetUp]
    public void Setup()
    {
        stringRef = new()
        {
            UseConstant = false,
            Variable = ScriptableObject.CreateInstance<StringVariableSO>()
        };
    }

    [TearDown]
    public void TearDown()
    {
        if (stringRef != null)
        {
            if (!ReferenceEquals(stringRef.Variable, null))
                Object.DestroyImmediate(stringRef.Variable);
        }
    }

    [Test]
    public void ImplicitConversion_ExtractsCorrectValue()
    {
        stringRef.Value = "Unity";

        // Tests the implicit operator: StringVariableSO -> string
        string primitiveValue = stringRef;

        Assert.AreEqual("Unity", primitiveValue, "Implicit conversion failed to extract the correct string .Value.");
    }

    [Test]
    public void ImplicitConversion_AllowsStringConcatenation()
    {
        stringRef.Value = "Hello";

        // Implicitly converts stringRef to string, then concatenates
        string result = stringRef + " World";

        Assert.AreEqual("Hello World", result, "Implicit conversion failed when used in string concatenation.");
    }

    [Test]
    public void EqualityOperator_ComparesWithStringCorrectly()
    {
        stringRef.Value = "PlayerOne";

        // Tests both orders of the overloaded '==' operator
        Assert.IsTrue(stringRef == "PlayerOne", "ScriptableObject should equal its underlying string value.");
        Assert.IsTrue("PlayerOne" == stringRef, "Reversed comparison should also evaluate to true.");
        Assert.IsFalse(stringRef == "PlayerTwo", "Different strings should not match.");
    }

    [Test]
    public void InequalityOperator_ComparesWithStringCorrectly()
    {
        stringRef.Value = "Active";

        // Tests the overloaded '!=' operator
        Assert.IsTrue(stringRef != "Inactive", "Different strings should trigger inequality.");
        Assert.IsFalse(stringRef != "Active", "Equal strings should not trigger inequality.");
    }

    [Test]
    public void OnValueChanged_InvokesEventWhenValueSet()
    {
        string callbackValue = null;
        stringRef.Variable.OnValueChanged += (val) => callbackValue = val;

        stringRef.Value = "NewState";

        Assert.AreEqual("NewState", callbackValue, "OnValueChanged event did not broadcast the updated string value.");
    }

    [Test]
    public void NullReference_EqualityCheck_DoesNotThrowException()
    {
        StringVariableSO nullVar = null;

        // 1. Verify that comparing an unassigned ScriptableObject wrapper against a string safely evaluates
        bool equalsHello = (nullVar == "Hello");
        Assert.IsFalse(equalsHello, "A null ScriptableObject should not match non-null strings.");

        // 2. Verify that ReferenceEquals safely identifies the unassigned wrapper
        Assert.IsTrue(ReferenceEquals(nullVar, null), "ReferenceEquals should confirm the ScriptableObject reference is null.");

        // 3. Verify that the IsNullOrEmpty convenience property correctly handles a stored null string
        stringRef.Value = null;
        Assert.IsTrue(string.IsNullOrEmpty(stringRef.Value), "A VariableSO holding a null string should report IsNullOrEmpty as true.");

        // 4. Verify that IsNullOrEmpty correctly handles an empty string
        stringRef.Value = "";
        Assert.IsTrue(string.IsNullOrEmpty(stringRef.Value), "A VariableSO holding an empty string should report IsNullOrEmpty as true.");

        // 5. Verify that IsNullOrEmpty returns false for valid text
        stringRef.Value = "PlayerOne";
        Assert.IsFalse(string.IsNullOrEmpty(stringRef.Value), "A VariableSO holding text should report IsNullOrEmpty as false.");
    }
}
