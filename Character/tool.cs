using UnityEditor;
using UnityEngine;

public class AddColliders : Editor
{
    [MenuItem("Tools/Add Box Colliders to Selection")]
    private static void AddBoxColliders()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            // Only add a collider if one doesn't exist
            if (obj.GetComponent<Collider>() == null)
            {
                obj.AddComponent<BoxCollider>();
            }
        }
    }
}