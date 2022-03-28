using System;
using System.Linq;
using System.Reflection;
using Plugins.Extendable.Runtime;
using UnityEditor;
using UnityEngine;

namespace Plugins.Extendable.Editor
{
    #if !ODIN_INSPECTOR
    [InitializeOnLoad]
    public class TileExtensionContextMenu
    {
        static TileExtensionContextMenu()
        {
            EditorApplication.contextualPropertyMenu -= OnContextualPropertyMenu;
            EditorApplication.contextualPropertyMenu += OnContextualPropertyMenu;
        }

        private static void OnContextualPropertyMenu(GenericMenu menu, SerializedProperty property)
        {
            if (property.isArray) return;
            if (property.propertyType != SerializedPropertyType.ManagedReference) return;

            var propertyCopy = property.Copy();
            var types = TypeCache.GetTypesDerivedFrom<TileExtension>().Where(t => (t.Attributes & TypeAttributes.Serializable) != 0);

            foreach (Type type in types)
            {
                menu.AddItem(new GUIContent($"set to {type.Name}"), false, () =>
                {
                    propertyCopy.serializedObject.Update();

                    foreach (var target in property.serializedObject.targetObjects)
                    {
                        Undo.RegisterCompleteObjectUndo(target,$"change type to {type.Name}");
                    }
                    propertyCopy.managedReferenceValue = Activator.CreateInstance(type);
                    propertyCopy.serializedObject.ApplyModifiedProperties();
                });
            }
        }
    }
#endif
}
