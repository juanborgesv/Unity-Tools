using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatRange))]
public class FloatRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw the label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        // Calculate the width for each field
        float fieldWidth = (position.width - 10) / 2; // 10 is the space between fields
                                                      // Draw the Min field

        Rect minRect = new Rect(position.x, position.y, fieldWidth, position.height);
        EditorGUI.PropertyField(minRect, property.FindPropertyRelative("Min"), GUIContent.none);

        // Draw the Max field
        Rect maxRect = new Rect(position.x + fieldWidth + 10, position.y, fieldWidth, position.height);
        EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("Max"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}
