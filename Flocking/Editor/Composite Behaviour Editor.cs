using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CompositeBehaviour))]
public class CompositeBehaviourEditor : Editor
{
    public override void OnInspectorGUI() //seting up custom editor for behaviours
    {
        CompositeBehaviour cb = (CompositeBehaviour)target;

        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            EditorGUILayout.HelpBox("No Behaviours in array.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.LabelField("Behaviours", GUILayout.MinWidth(60f));
            EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < cb.behaviours.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                cb.behaviours[i] = (FlockBehaviour)EditorGUILayout.ObjectField(cb.behaviours[i], typeof(FlockBehaviour), false, GUILayout.MinWidth(60f));
                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(cb);
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Behaviour"))
        {
            AddBehaviour(cb);
            EditorUtility.SetDirty(cb);
        }
        EditorGUILayout.EndHorizontal();

        if (cb.behaviours != null && cb.behaviours.Length > 0)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove Behaviour"))
            {
                RemoveBehaviour(cb);
                EditorUtility.SetDirty(cb);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void AddBehaviour(CompositeBehaviour cb) //function to add behaviour for editor
    {
        int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];
        for (int i = 0; i < oldCount; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldCount] = 1f;
        cb.behaviours = newBehaviours;
        cb.weights = newWeights;
    }
    private void RemoveBehaviour(CompositeBehaviour cb) //funciton to remove behaviour from editor
    {
        int oldCount = cb.behaviours.Length;
        if (oldCount == 1)
        {
            cb.behaviours = null;
            cb.weights = null;
            return;
        }
        else
        {
            FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
            float[] newWeights = new float[oldCount - 1];
            for (int i = 0; i < oldCount - 1; i++)
            {
                newBehaviours[i] = cb.behaviours[i];
                newWeights[i] = cb.weights[i];
            }
            cb.behaviours = newBehaviours;
            cb.weights = newWeights;
        }
    }
}