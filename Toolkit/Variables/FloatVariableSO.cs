using UnityEngine;

namespace JuanBorges.UnityTools
{
    [CreateAssetMenu(fileName = "Float Variable", menuName = "Scriptable Objects/Variables/Float")]
    public class FloatVariableSO : VariableSO<float>
    {
        // Allows: -myFloatSO
        public static float operator -(FloatVariableSO variable) => (Object)variable != null ? -variable.Value : 0f;
    }
}