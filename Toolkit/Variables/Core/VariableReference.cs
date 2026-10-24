namespace JuanBorges.UnityTools
{
    public class VariableReference<T>
    {
        public bool UseConstant = true;
        public T ConstantValue;
        public VariableSO<T> Variable;

        public VariableReference() { }

        public VariableReference(T constantValue)
        {
            UseConstant = true;
            ConstantValue = constantValue;
        }

        public T Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant) ConstantValue = value;
                else Variable.Value = value;
            }
        }
    }
}
