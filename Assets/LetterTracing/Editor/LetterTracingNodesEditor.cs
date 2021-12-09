using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LetterTracingNodes))]
public class LetterTracingNodesEditor : Editor
{
    LetterTracingNodes _mLetterTracingNodes;

    private void OnEnable()
    {
        _mLetterTracingNodes = target as LetterTracingNodes;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Add Node"))
        {
            Undo.RecordObject(target, "added node");
            Vector3 position = _mLetterTracingNodes.localNodes[_mLetterTracingNodes.localNodes.Length - 1] + Vector3.right;

            ArrayUtility.Add(ref _mLetterTracingNodes.localNodes, position);
        }

        EditorGUIUtility.labelWidth = 64;
        int delete = -1;
        for (int i = 0; i < _mLetterTracingNodes.localNodes.Length; ++i)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            int size = 64;
            EditorGUILayout.BeginVertical(GUILayout.Width(size));
            EditorGUILayout.LabelField("Node " + i, GUILayout.Width(size));
            if (i != 0 && GUILayout.Button("Delete", GUILayout.Width(size)))
            {
                delete = i;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            Vector3 newPosition;
            if (i == 0)
            {
                newPosition = _mLetterTracingNodes.localNodes[i];
            }
            else
            {
                newPosition = EditorGUILayout.Vector3Field("Position", _mLetterTracingNodes.localNodes[i]);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "changed time or position");
                _mLetterTracingNodes.localNodes[i] = newPosition;
            }
        }
        EditorGUIUtility.labelWidth = 0;

        if (delete != -1)
        {
            Undo.RecordObject(target, "Removed point moving platform");
            ArrayUtility.RemoveAt(ref _mLetterTracingNodes.localNodes, delete);
        }
    }

    private void OnSceneGUI()
    {
        MovePreview();

        for (int i = 0; i < _mLetterTracingNodes.localNodes.Length; ++i)
        {
            Vector3 worldPos;
            
            
            if (Application.isPlaying)
            {
                worldPos = _mLetterTracingNodes.worldNode[i];
            }
            else
            {
                worldPos = _mLetterTracingNodes.transform.TransformPoint(_mLetterTracingNodes.localNodes[i]);
            }

            Vector3 newWorld = worldPos; 
            if(i != 0)
                newWorld = Handles.PositionHandle(worldPos, Quaternion.identity);

            Handles.color = Color.red;

            if (i != 0)
            {
                if (Application.isPlaying)
                {
                    Handles.DrawDottedLine(worldPos, _mLetterTracingNodes.worldNode[i - 1], 10);
                }
                else
                {
                    Handles.DrawDottedLine(worldPos,
                        _mLetterTracingNodes.transform.TransformPoint(_mLetterTracingNodes.localNodes[i - 1]), 10);
                }

                if (worldPos != newWorld)
                {
                    Undo.RecordObject(target, "moved point");
                    _mLetterTracingNodes.localNodes[i] = _mLetterTracingNodes.transform.InverseTransformPoint(newWorld);
                }
            }
        }
    }

    private void MovePreview()
    {
        if (Application.isPlaying)
            return;
        SceneView.RepaintAll();
    }
}