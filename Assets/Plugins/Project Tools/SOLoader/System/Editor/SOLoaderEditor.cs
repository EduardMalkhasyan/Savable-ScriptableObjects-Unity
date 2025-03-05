using UnityEditor;
using UnityEngine;

namespace Project.Tools.SOHelp
{
#if !ODIN_INSPECTOR 
    [CustomEditor(typeof(SOLoader<>), true)]
    public class SOLoaderEditor : Editor
    {
        private SerializedProperty savableProperty;
        private SerializedProperty autoSaveProperty;
        private SerializedProperty debugProperty;

        private const string savableFieldName = "savable";
        private const string autoSaveFieldName = "autoSave";
        private const string debugFieldName = "debug";
        private const string scriptFieldName = "m_Script";

        private bool showControlGroup = true;

        private GUIStyle foldoutStyle;
        private GUIStyle buttonStyle;
        private GUIStyle boxStyle;

        protected void OnEnable()
        {
            savableProperty = serializedObject.FindProperty(savableFieldName);
            autoSaveProperty = serializedObject.FindProperty(autoSaveFieldName);
            debugProperty = serializedObject.FindProperty(debugFieldName);
        }

        public override void OnInspectorGUI()
        {
            InitializeStyles();
            serializedObject.Update();

            DrawScriptReference();

            showControlGroup = EditorGUILayout.Foldout(showControlGroup,
                               new GUIContent("Control", "Toggle to show/hide control settings"),
                               true, foldoutStyle);

            if (showControlGroup)
            {
                EditorGUILayout.BeginVertical(boxStyle);
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUI.indentLevel++;

                    EditorGUILayout.PropertyField(savableProperty);

                    if (savableProperty.boolValue)
                    {
                        EditorGUILayout.PropertyField(autoSaveProperty);
                        EditorGUILayout.PropertyField(debugProperty);
                        EditorGUILayout.Space(10);

                        if (GUILayout.Button("Manual Save Data", buttonStyle))
                        {
                            ((ISOLoader)target).ManualSaveData();
                        }

                        if (GUILayout.Button("Save Reserve Data", buttonStyle))
                        {
                            ((ISOLoader)target).SaveReserveData();
                        }

                        if (GUILayout.Button("Manual Delete Data", buttonStyle))
                        {
                            ((ISOLoader)target).ManualDeleteData();
                        }
                    }

                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            DrawInheritedClassFields();

            serializedObject.ApplyModifiedProperties();
        }

        private void InitializeStyles()
        {
            if (foldoutStyle == null)
            {
                foldoutStyle = new GUIStyle(EditorStyles.foldout)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 12,
                    padding = new RectOffset(15, 0, 3, 3)
                };
            }

            if (buttonStyle == null)
            {
                buttonStyle = new GUIStyle(GUI.skin.button)
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold,
                    fixedHeight = 25,
                    padding = new RectOffset(10, 10, 5, 5)
                };
            }

            if (boxStyle == null)
            {
                boxStyle = new GUIStyle(GUI.skin.box)
                {
                    padding = new RectOffset(10, 10, 10, 10),
                    margin = new RectOffset(5, 5, 5, 5)
                };
            }
        }

        private void DrawScriptReference()
        {
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((ScriptableObject)target), typeof(MonoScript), false);
            }
            EditorGUILayout.Space(10);
        }

        private void DrawInheritedClassFields()
        {
            SerializedProperty property = serializedObject.GetIterator();
            bool enterChildren = true;

            while (property.NextVisible(enterChildren))
            {
                if (property.name == scriptFieldName ||
                    property.name == savableFieldName ||
                    property.name == autoSaveFieldName ||
                    property.name == debugFieldName)
                    continue;

                EditorGUILayout.PropertyField(property, true);
                enterChildren = false;
            }
        }
    }
#endif 
}