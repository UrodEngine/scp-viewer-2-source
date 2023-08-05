using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(MeshRenderer))]
public class CreateMatInstance : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate material instance"))
        {
            Renderer renderer = target as Renderer;
            renderer.materials.Clone();
        }
    }
}
#endif
