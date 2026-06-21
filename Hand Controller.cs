using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandController : MonoBehaviour
{
    private Camera _cam;
    private Rigidbody2D _rb;
    private SpringJoint2D _spring;

    [SerializeField] private float springDistanceTarget;
    [SerializeField] private float springDistanceLerp;
    [SerializeField] private LayerMask wallsLayerMask;
    [SerializeField] private int mouseButton;
    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
    }

    private void FixedUpdate()
    {
        _spring.distance = Mathf.Lerp(_spring.distance, springDistanceTarget, Mathf.Exp(springDistanceLerp));

        if (Input.GetMouseButton(mouseButton))
        {
            // Shoot a raycast from player origin to test for wall to grab onto
            var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var ray = Physics2D.Raycast(_spring.connectedBody.position, (Vector2)mousePos -  _spring.connectedBody.position, Mathf.Infinity, wallsLayerMask);
            if (ray.collider != null)
            {
                print(ray.collider.gameObject.name);
                _rb.MovePosition(ray.point);
                _spring.distance = Vector2.Distance(_spring.connectedBody.position,_rb.transform.position);
            }
        }
        
    }
}
