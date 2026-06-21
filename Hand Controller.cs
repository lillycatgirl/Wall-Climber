using Unity.Mathematics;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private Camera _cam;
    private Rigidbody2D _rb;
    private SpringJoint2D _spring;

    [SerializeField] private float springDistanceTarget;
    [SerializeField] private float springDistanceLerp;

    [SerializeField] private int mouseButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _spring.enabled = true;
        
        _spring.distance = Mathf.Lerp(_spring.distance, springDistanceTarget, Mathf.Exp(springDistanceLerp));

        if (Input.GetMouseButton(mouseButton))
        {
            var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            _rb.MovePosition(mousePos);
            _spring.distance = Vector2.Distance(_spring.connectedBody.position,_rb.transform.position);
        }
        
    }
}
