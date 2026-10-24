namespace JuanBorges.UnityTools
{
    [System.Serializable]
    public class FloatReference : VariableReference<float>
    {
        public FloatReference() : base() { }
        public FloatReference(float constantValue) : base(constantValue) { }
        public static implicit operator float(FloatReference reference) => reference.Value;
    }
}
