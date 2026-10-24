namespace JuanBorges.UnityTools
{
    [System.Serializable]
    public class FloatRangeReference : VariableReference<FloatRange>
    {
        public FloatRangeReference() : base() { }
        public FloatRangeReference(FloatRange value) : base(value) { }

        public static implicit operator FloatRange(FloatRangeReference reference) =>
        reference is null ? default : reference.Value;
    }
}