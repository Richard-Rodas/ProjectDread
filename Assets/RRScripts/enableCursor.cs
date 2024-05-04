using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableCursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
