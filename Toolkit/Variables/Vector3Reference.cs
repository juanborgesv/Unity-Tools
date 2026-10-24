using UnityEngine;

namespace JuanBorges.UnityTools
{
    [System.Serializable]
    public class Vector3Reference : VariableReference<Vector3>
    {
        public Vector3Reference() : base() { }
        public Vector3Reference(Vector3 constantValue) : base(constantValue) { }

        // Implicit conversion to raw Vector3
        public static implicit operator Vector3(Vector3Reference reference) =>
            reference is null ? Vector3.zero : reference.Value;

        // --- Addition (+) ---
        public static Vector3 operator +(Vector3Reference a, Vector3 b) =>
            (a is null ? Vector3.zero : a.Value) + b;

        public static Vector3 operator +(Vector3 a, Vector3Reference b) =>
            a + (b is null ? Vector3.zero : b.Value);

        public static Vector3 operator +(Vector3Reference a, Vector3Reference b) =>
            (a is null ? Vector3.zero : a.Value) + (b is null ? Vector3.zero : b.Value);

        // --- Subtraction (-) ---
        public static Vector3 operator -(Vector3Reference a, Vector3 b) =>
            (a is null ? Vector3.zero : a.Value) - b;

        public static Vector3 operator -(Vector3 a, Vector3Reference b) =>
            a - (b is null ? Vector3.zero : b.Value);

        public static Vector3 operator -(Vector3Reference a, Vector3Reference b) =>
            (a is null ? Vector3.zero : a.Value) - (b is null ? Vector3.zero : b.Value);

        // --- Unary Negation (-) ---
        public static Vector3 operator -(Vector3Reference reference) =>
            reference is null ? Vector3.zero : -reference.Value;

        // --- Scalar Multiplication (*) ---
        public static Vector3 operator *(Vector3Reference reference, float scalar) =>
            (reference is null ? Vector3.zero : reference.Value) * scalar;

        public static Vector3 operator *(float scalar, Vector3Reference reference) =>
            scalar * (reference is null ? Vector3.zero : reference.Value);

        // --- Scalar Division (/) ---
        public static Vector3 operator /(Vector3Reference reference, float scalar) =>
            (reference is null ? Vector3.zero : reference.Value) / scalar;
    }
}
