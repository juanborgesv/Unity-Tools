namespace JuanBorges.UnityTools
{ 
    [System.Serializable]
    public class IntReference : VariableReference<int>
    {
        public IntReference() : base() { }
        public IntReference(int constantValue) : base(constantValue) { }
        public static implicit operator int(IntReference reference) => reference.Value;
    }
}
