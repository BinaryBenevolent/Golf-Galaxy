using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Collider col;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float force;

    //[SerializeField] private LineRenderer aimLine;

    [SerializeField] private Transform aimWorld;

    private bool shoot;

    private bool isShootingMode;

    private float forceFactor;
    private Vector3 forceDirection;

    private Ray ray;
    private Plane plane;

    public bool IsShootingMode { get => isShootingMode; }

    private void Update()
    {
        if(isShootingMode)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //aimLine.gameObject.SetActive(true);
                aimWorld.gameObject.SetActive(true);
                plane = new Plane(Vector3.up, this.transform.position);
            }
            else if (Input.GetMouseButton(0))
            {
                var mouseViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                var ballViewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
                var ballScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
                var pointerDirection = ballViewportPos - mouseViewportPos;
                pointerDirection.z = 0;

                //var positions = new Vector3[] { ballScreenPos, Input.mousePosition };
                //aimLine.SetPositions(positions);

                

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                plane.Raycast(ray, out var distance);
                forceDirection = this.transform.position - ray.GetPoint(distance);
                forceDirection.Normalize();

                forceFactor = pointerDirection.magnitude * 2;

                aimWorld.transform.position = this.transform.position;
                aimWorld.forward = forceDirection;                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                shoot = true;
                isShootingMode = false;

                //aimLine.gameObject.SetActive(false);
                aimWorld.gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if(shoot)
        {
            shoot = false;

            rb.AddForce(forceDirection * force * forceFactor, ForceMode.Impulse);
        }

        if(rb.velocity.sqrMagnitude < 0.2f && rb.velocity.sqrMagnitude > 0f)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public bool IsMove()
    {
        return rb.velocity != Vector3.zero;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(IsMove() == false)
            isShootingMode = true;
    }
}
