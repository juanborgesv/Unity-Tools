namespace JuanBorges.UnityTools
{
    [System.Serializable]
    public class BoolReference : VariableReference<bool>
    {
        public BoolReference() : base() { }
        public BoolReference(bool constantValue) : base(constantValue) { }
        public static implicit operator bool(BoolReference reference) => reference.Value;
    }
}
