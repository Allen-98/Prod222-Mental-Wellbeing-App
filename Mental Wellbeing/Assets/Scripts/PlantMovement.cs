using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMovement : MonoBehaviour
{
    //
    // Allows the plant to be tapped and dragged horizontally.
    //

    [Header("Movement Settings")]
    //[Tooltip("The total horizontal movement range of the plant. E.g. it can only move half the range left and right of the origin.")]
    //public float movementRange = 20f;
    [Tooltip("How far away the movement stops to the side of the screen.")]
    public float sidePadding = 1f;

    private bool dragging = false;
    private float dragOrigin = 0f;
    private float dragPlantX = 0f;

    private float sideMovement;

    private PlantHealth plantHealth = null;

    void Start()
    {
        plantHealth = GetComponent<PlantHealth>();

        Camera camera = Camera.main;
        sideMovement = (camera.aspect * camera.orthographicSize) - sidePadding;
    }

    void Update()
    {
        if (plantHealth != null && plantHealth.IsFinished()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null && hit.transform == transform)
            {
                dragging = true;
                dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                dragPlantX = transform.position.x;
            }
        }
        if (!Input.GetMouseButton(0) || Time.timeScale == 0f) dragging = false;

        if (dragging)
        {
            float newX = Mathf.Clamp((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - dragOrigin) + dragPlantX, -sideMovement, sideMovement);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}
