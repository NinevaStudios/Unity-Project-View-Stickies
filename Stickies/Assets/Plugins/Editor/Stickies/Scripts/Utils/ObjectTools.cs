using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    static class HierarchyObjectIdTools
    {
        private static PropertyInfo cachedInspectorModeInfo;

        private static readonly Dictionary<int, long> _cache = new Dictionary<int, long>();

        public static long GetIdForHierarchyObject(int instanceId)
        {
            return _cache.ContainsKey(instanceId) ? _cache[instanceId] : 0;
        }

        static long GetLocalIdentifierInFileForObject(Object unityObject)
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
            id = serializedProperty.longValue;
            if (id <= 0)
            {
                PrefabType prefabType = PrefabUtility.GetPrefabType(unityObject);
                if (prefabType != PrefabType.None)
                {
                    // Only show at top object
                    var go = unityObject as GameObject;
                    var goParent = go.transform.parent;

                    if (goParent == null || PrefabUtility.FindPrefabRoot(go) == go)
                    {
                        id = GetLocalIdentifierInFileForObject(PrefabUtility.GetPrefabObject(unityObject));
                    }
                }
            }

            return id;
        }

        public static void Refresh()
        {
            FlushCache();
            foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
            {
                if (obj.transform.parent == null)
                {
                    Traverse(obj);
                }
            }
        }

        static void Traverse(GameObject obj)
        {
            CacheObject(obj);

            foreach (Transform child in obj.transform)
            {
                Traverse(child.gameObject);
            }
        }

        static void CacheObject(GameObject obj)
        {
            var instanceId = obj.GetInstanceID();
            var localHierarchyId = GetLocalIdentifierInFileForObject(obj);
            if (localHierarchyId != 0)
            {
                _cache[instanceId] = localHierarchyId;
            }
        }

        public static void FlushCache()
        {
            if (_cache != null)
            {
                _cache.Clear();
            }
        }
    }
}