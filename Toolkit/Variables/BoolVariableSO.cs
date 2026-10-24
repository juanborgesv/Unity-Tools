using UnityEngine;

namespace JuanBorges.UnityTools
{
    [CreateAssetMenu(fileName = "Bool Variable", menuName = "Scriptable Objects/Variables/Bool")]
    public class BoolVariableSO : VariableSO<bool>
    {
        public static bool operator !(BoolVariableSO variable)
        {
            if ((Object)variable == null) return true;
            return !variable.Value;
        }
    }
}
