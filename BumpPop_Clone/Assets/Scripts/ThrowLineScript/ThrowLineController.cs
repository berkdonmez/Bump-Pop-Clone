using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLineController : MonoBehaviour
{
    Vector3 lookPos;


    private void Update()
    {
        CharacterRotatingArroundMouse();
    }

    void CharacterRotatingArroundMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        Vector3 lookDirection = lookPos - transform.position;
        lookDirection.y = 0;

        transform.LookAt(transform.position + lookDirection, Vector3.up);
    }

}
