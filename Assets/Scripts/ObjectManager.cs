using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    bool isClicked = false;
    [SerializeField]
    private float rotationSpeed = 1f;

    void OnMouseDown()
    {
        isClicked = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));


    }
    private void OnMouseUp()
    {
        isClicked = false;
    }
    void OnMouseDrag()
    {
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
        transform.position = cursorPosition;
    }

    private void Update()
    {
        if (isClicked == true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                print("r");
                transform.Rotate(Vector3.up * rotationSpeed);
            }

            if (Input.GetKey(KeyCode.T))
            {
                print("t");
                transform.Rotate(Vector3.down * rotationSpeed);
            }
        }
    }
}
