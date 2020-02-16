using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseCursor : MonoBehaviour
{
    public Texture2D defaultCursor, pressedCursor;
    public GameObject selectItem;

    void Start()
    {
        setDefaultCursor();
    }

    // Update is called once per frame
    void Update()
    {
        updateMousePos();
    }

    private void updateMousePos()
    {
        //setDefaultCursor();

        Vector2 newpos = new Vector3();
        Vector3 cursorPos = Input.mousePosition;

        //transform to convas position
        newpos.x = (cursorPos.x - (Screen.width / 2)) * (960f / Camera.main.pixelWidth) + 25;
        newpos.y = (cursorPos.y - (Screen.height / 2)) * (540f / Camera.main.pixelHeight) - 30;

        selectItem.GetComponent<RectTransform>().anchoredPosition = newpos;
    }

    public void setDefaultCursor()
    {
        //temporary
        if (Screen.width < 1400)
        {
            Cursor.SetCursor(pressedCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

}
