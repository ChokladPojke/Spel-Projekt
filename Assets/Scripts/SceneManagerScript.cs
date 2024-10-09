using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // Static method to remove objects with specified tags in every scene
    public static void RemoveObjectsWithTagsInEveryScene(string[] tagsToRemove)
    {
        foreach (var tagToRemove in tagsToRemove)
        {
            GameObject[] objectsToRemove = GameObject.FindGameObjectsWithTag(tagToRemove);

            foreach (var obj in objectsToRemove)
            {
                Destroy(obj);
            }
        }
    }
}