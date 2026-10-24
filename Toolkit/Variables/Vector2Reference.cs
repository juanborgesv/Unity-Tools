using UnityEngine;

namespace JuanBorges.UnityTools
{
    [System.Serializable]
    public class Vector2Reference : VariableReference<Vector2>
    {
        public Vector2Reference() : base() { }
        public Vector2Reference(Vector2 constantValue) : base(constantValue) { }

        // Implicit conversion to raw Vector2
        public static implicit operator Vector2(Vector2Reference reference) =>
            reference is null ? Vector2.zero : reference.Value;

        // --- Addition (+) ---
        public static Vector2 operator +(Vector2Reference a, Vector2 b) =>
            (a is null ? Vector2.zero : a.Value) + b;

        public static Vector2 operator +(Vector2 a, Vector2Reference b) =>
            a + (b is null ? Vector2.zero : b.Value);

        public static Vector2 operator +(Vector2Reference a, Vector2Reference b) =>
            (a is null ? Vector2.zero : a.Value) + (b is null ? Vector2.zero : b.Value);

        // --- Subtraction (-) ---
        public static Vector2 operator -(Vector2Reference a, Vector2 b) =>
            (a is null ? Vector2.zero : a.Value) - b;

        public static Vector2 operator -(Vector2 a, Vector2Reference b) =>
            a - (b is null ? Vector2.zero : b.Value);

        public static Vector2 operator -(Vector2Reference a, Vector2Reference b) =>
            (a is null ? Vector2.zero : a.Value) - (b is null ? Vector2.zero : b.Value);

        // --- Unary Negation (-) ---
        public static Vector2 operator -(Vector2Reference reference) =>
            reference is null ? Vector2.zero : -reference.Value;

        // --- Scalar Multiplication (*) ---
        public static Vector2 operator *(Vector2Reference reference, float scalar) =>
            (reference is null ? Vector2.zero : reference.Value) * scalar;

        public static Vector2 operator *(float scalar, Vector2Reference reference) =>
            scalar * (reference is null ? Vector2.zero : reference.Value);

        // --- Scalar Division (/) ---
        public static Vector2 operator /(Vector2Reference reference, float scalar) =>
            (reference is null ? Vector2.zero : reference.Value) / scalar;
    }
}
