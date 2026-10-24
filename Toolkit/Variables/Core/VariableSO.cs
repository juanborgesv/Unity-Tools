using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JuanBorges.UnityTools
{
    public class VariableSO<T> : ScriptableObject
    {
        [SerializeField]
        private T value;

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnValueChanged?.Invoke(this.value);
            }
        }

        public event UnityAction<T> OnValueChanged;

        public static implicit operator T(VariableSO<T> variable) => variable.Value;

        // Equality operator to compare a VariableSO<T> with a T value. Both ways are supported (VariableSO<T> == T and T == VariableSO<T>).
        public static bool operator ==(VariableSO<T> left, T right)
        {
            // Cast to UnityEngine.Object to safely check Unity's "fake null" for destroyed objects
            if ((Object)left == null)
                return EqualityComparer<T>.Default.Equals(right, default(T));

            return EqualityComparer<T>.Default.Equals(left.value, right);
        }
        public static bool operator ==(T left, VariableSO<T> right) => right == left;

        // Inequality operator a VariableSO<T> with a T value. Both ways are supported (VariableSO<T> != T and T != VariableSO<T>).
        public static bool operator !=(VariableSO<T> left, T right) => !(left == right);
        public static bool operator !=(T left, VariableSO<T> right) => right != left;


        // Standard C# best practice when overloading ==
        public override bool Equals(object obj)
        {
            if (obj is T otherValue)
                return this == otherValue;

            if (obj is VariableSO<T> otherVariable)
                return this == otherVariable.value;

            return false;
        }

        public override int GetHashCode() => value == null ? 0 : value.GetHashCode();
    }
}
