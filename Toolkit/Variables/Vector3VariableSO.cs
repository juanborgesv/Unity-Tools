using UnityEngine;

namespace JuanBorges.UnityTools
{
    [CreateAssetMenu(fileName = "Vector3 Variable", menuName = "Scriptable Objects/Variables/Vector3")]
    public class Vector3VariableSO : VariableSO<Vector3>
    {
        // Implicit conversion to raw Vector3
        public static implicit operator Vector3(Vector3VariableSO variable) =>
            variable is null ? Vector3.zero : variable.Value;

        // --- Addition (+) ---
        public static Vector3 operator +(Vector3VariableSO a, Vector3 b) =>
            (a is null ? Vector3.zero : a.Value) + b;

        public static Vector3 operator +(Vector3 a, Vector3VariableSO b) =>
            a + (b is null ? Vector3.zero : b.Value);

        public static Vector3 operator +(Vector3VariableSO a, Vector3VariableSO b) =>
            (a is null ? Vector3.zero : a.Value) + (b is null ? Vector3.zero : b.Value);

        // --- Subtraction (-) ---
        public static Vector3 operator -(Vector3VariableSO a, Vector3 b) =>
            (a is null ? Vector3.zero : a.Value) - b;

        public static Vector3 operator -(Vector3 a, Vector3VariableSO b) =>
            a - (b is null ? Vector3.zero : b.Value);

        public static Vector3 operator -(Vector3VariableSO a, Vector3VariableSO b) =>
            (a is null ? Vector3.zero : a.Value) - (b is null ? Vector3.zero : b.Value);

        // --- Unary Negation (-) ---
        public static Vector3 operator -(Vector3VariableSO variable) =>
            variable is null ? Vector3.zero : -variable.Value;

        // --- Scalar Multiplication (*) ---
        public static Vector3 operator *(Vector3VariableSO variable, float scalar) =>
            (variable is null ? Vector3.zero : variable.Value) * scalar;

        public static Vector3 operator *(float scalar, Vector3VariableSO variable) =>
            scalar * (variable is null ? Vector3.zero : variable.Value);

        // --- Scalar Division (/) ---
        public static Vector3 operator /(Vector3VariableSO variable, float scalar) =>
            (variable is null ? Vector3.zero : variable.Value) / scalar;
    }
}