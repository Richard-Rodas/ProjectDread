using UnityEngine;

public class AlignChildrenToParent : MonoBehaviour
{
    void Start()
    {
        // Loop through all child objects
        foreach (Transform child in transform)
        {
            // Set the local position of each child to (0, 0, 0) relative to the parent
            child.localPosition = Vector3.zero;
        }
    }
}
