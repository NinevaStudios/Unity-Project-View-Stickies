#define UNITY_5_PLUS
#if UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9
#undef UNITY_5_PLUS
#endif

using UnityEngine;
using System.Reflection;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    static class ObjectTools
    {
        private static PropertyInfo cachedInspectorModeInfo;

        internal static long GetLocalIdentifierInFileForObject(Object unityObject)
        {
            long id = 0;

            if (unityObject == null)
                return id;

            if (cachedInspectorModeInfo == null)
            {
                cachedInspectorModeInfo = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
            }

            SerializedObject serializedObject = new SerializedObject(unityObject);
            cachedInspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);
            SerializedProperty serializedProperty = serializedObject.FindProperty("m_LocalIdentfierInFile");
            #if UNITY_5_PLUS
            id = serializedProperty.longValue;
            #else
            id = serializedProperty.intValue;
            #endif
            if (id <= 0)
            {
                PrefabType prefabType = PrefabUtility.GetPrefabType(unityObject);
                if (prefabType != PrefabType.None)
                {
                    // Only show at top object
                    if ((unityObject as GameObject).transform.parent == null)
                    {
                        id = GetLocalIdentifierInFileForObject(PrefabUtility.GetPrefabObject(unityObject));
                    }
                }
            }

            return id;
        }
    }
}