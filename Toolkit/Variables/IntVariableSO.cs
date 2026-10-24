using UnityEngine;


namespace JuanBorges.UnityTools
{
    [CreateAssetMenu(fileName = "Int Variable ", menuName = "Scriptable Objects/Variables/Int")]
    public class IntVariableSO : VariableSO<int> 
    {
        // Allows: -myIntSO (e.g., int result = -nullVar;)
        public static int operator -(IntVariableSO variable) => (Object)variable != null ? -variable.Value : 0;
    }
}
