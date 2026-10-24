using UnityEngine;

namespace JuanBorges.UnityTools
{
    [CreateAssetMenu(fileName = "Vector2 Variable", menuName = "Scriptable Objects/Variables/Vector2")]
    public class Vector2VariableSO : VariableSO<Vector2>
    {
        #region Unary Operators
        // Allows: -myVector2SO
        public static Vector2 operator -(Vector2VariableSO variable) => 
            (Object)variable != null ? -variable.Value : Vector2.zero;
        #endregion

        #region Addition
        public static Vector2 operator +(Vector2VariableSO left, Vector2 right) => ((Object)left != null ? left.Value : Vector2.zero) + right;

        public static Vector2 operator +(Vector2 left, Vector2VariableSO right) => left + ((Object)right != null ? right.Value : Vector2.zero);

        public static Vector2 operator +(Vector2VariableSO left, Vector2VariableSO right)
        {
            Vector2 leftVal = (Object)left != null ? left.Value : Vector2.zero;
            Vector2 rightVal = (Object)right != null ? right.Value : Vector2.zero;
            return leftVal + rightVal;
        }
        #endregion

        #region Subtraction
        public static Vector2 operator -(Vector2VariableSO left, Vector2 right) => ((Object)left != null ? left.Value : Vector2.zero) - right;

        public static Vector2 operator -(Vector2 left, Vector2VariableSO right) => left - ((Object)right != null ? right.Value : Vector2.zero);

        public static Vector2 operator -(Vector2VariableSO left, Vector2VariableSO right)
        {
            Vector2 leftVal = (Object)left != null ? left.Value : Vector2.zero;
            Vector2 rightVal = (Object)right != null ? right.Value : Vector2.zero;
            return leftVal - rightVal;
        }
        #endregion

        #region Scalar Multiplication
        public static Vector2 operator *(Vector2VariableSO left, float right)
        {
            if (left is null)
                throw new System.ArgumentNullException(nameof(left));

            return ((Object)left != null ? left.Value : Vector2.zero) * right;
        }

        public static Vector2 operator *(float left, Vector2VariableSO right) => left * ((Object)right != null ? right.Value : Vector2.zero);
        #endregion

        #region Scalar Division
        public static Vector2 operator /(Vector2VariableSO left, float right)
        {
            if (Mathf.Approximately(right, 0f))
            {
                Debug.LogWarning($"[Vector2VariableSO] Division by zero on asset '{left?.name}'. Returning Vector2.zero.");
                return Vector2.zero;
            }

            return ((Object)left != null ? left.Value : Vector2.zero) / right;
        }
        #endregion
    }
}