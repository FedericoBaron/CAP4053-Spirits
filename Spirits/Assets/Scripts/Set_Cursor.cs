using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Cursor : MonoBehaviour
{
    // This is the CustomCursor image in Assets/Images/Sprites
    public Texture2D customImage;

    void Start()
    {
        // Line 13 makes the center of the image on Line 8 the point of the cursor
        Vector2 cursorOffset = new Vector2(customImage.width/2, customImage.height/2);
        Cursor.SetCursor(customImage, cursorOffset, CursorMode.ForceSoftware);
    }
}
