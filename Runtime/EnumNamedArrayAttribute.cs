using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Plugins.mitaywalle.ExtendableTile.Runtime
{
    public class LabeledArrayAttribute : PropertyAttribute
    {
        public readonly string[] names;
        public LabeledArrayAttribute(string[] names) { this.names = names; }
        public LabeledArrayAttribute(Type enumType) { names = Enum.GetNames(enumType); }
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(LabeledArrayAttribute))]
    public class NamedArrayDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            try
            {
                LabeledArrayAttribute config = attribute as LabeledArrayAttribute;
                string[] enum_names = config.names;
                int pos = int.Parse(property.propertyPath.Split('[').LastOrDefault().TrimEnd(']'));
                string enum_label = enum_names.GetValue(pos) as string;
                enum_label = ObjectNames.NicifyVariableName(enum_label);
                label.text = enum_label;
            }
            catch (Exception e)
            {
                //Debug.LogException(e);
            }

            EditorGUI.PropertyField(position, property, label, property.isExpanded);
        }
    }
#endif
}
