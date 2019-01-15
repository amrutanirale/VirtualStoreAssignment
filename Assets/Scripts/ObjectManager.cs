using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    bool isObjectSelected = false;
    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private GameObject helpText;

    void OnMouseDown()
    {
        isObjectSelected = true;
        helpText.SetActive(true);
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }
    private void OnMouseUp()
    {
        isObjectSelected = false;
        helpText.SetActive(false);
    }
    void OnMouseDrag()
    {
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
        transform.position = cursorPosition;
    }

    private void Update()
    {
        if (isObjectSelected == true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                transform.Rotate(Vector3.up * rotationSpeed);
            }

            if (Input.GetKey(KeyCode.T))
            {
                transform.Rotate(Vector3.down * rotationSpeed);
            }
        }
    }
}
