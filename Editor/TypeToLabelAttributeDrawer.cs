using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.Extendable.Runtime;
using UnityEditor;
using UnityEngine;

namespace Plugins.Extendable.Editor
{
    [CustomPropertyDrawer(typeof(TypeToLabelAttribute))]
    public class TypeToLabelAttributeDrawer : PropertyDrawer
    {
        private static Dictionary<string, string> _cachedTypeNames = new Dictionary<string, string>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string fullTypeName = property.managedReferenceFullTypename;

            if (!string.IsNullOrEmpty(fullTypeName))
            {
                if (!_cachedTypeNames.ContainsKey(fullTypeName))
                {
                    _cachedTypeNames.Add(fullTypeName, fullTypeName.Split('.').LastOrDefault());
                }

                label.text = _cachedTypeNames[fullTypeName];
            }

            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }
    }
}
