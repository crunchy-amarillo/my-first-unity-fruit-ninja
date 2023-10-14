using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    public float minVelocity = 0.001f;

    private Rigidbody2D rigidbodyBlade;
    private Vector3 lastMousePosition;
    private Collider2D bladeCollider;

    private void Awake()
    {
        this.rigidbodyBlade = GetComponent<Rigidbody2D>();
        this.bladeCollider = GetComponent<Collider2D>();
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        //this.bladeCollider.enabled = IsMouseMoving();

        SetBladeToMouse();
    }

    private void SetBladeToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10; // Kamera Position = -10, Früchte = 0, Klinge = 10; ( mit der Klinge von -10 nach 10 einen Schnitt durchführen)
        this.rigidbodyBlade.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private bool IsMouseMoving()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        float distance = (this.lastMousePosition - currentMousePosition).magnitude;

        this.lastMousePosition = currentMousePosition;

        return distance > minVelocity;
    }
}
