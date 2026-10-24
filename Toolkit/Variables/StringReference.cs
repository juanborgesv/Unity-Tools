namespace JuanBorges.UnityTools
{
    [System.Serializable]
    public class StringReference : VariableReference<string>
    {
        public StringReference() : base() { }
        public StringReference(string constantValue) : base(constantValue) { }
        public static implicit operator string(StringReference reference) => reference.Value;
    }
}
