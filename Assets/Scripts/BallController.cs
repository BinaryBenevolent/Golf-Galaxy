using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Collider col;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float force;

    private bool shoot;

    private bool isShootingMode;

    private float forceFactor;

    public bool IsShootingMode { get => isShootingMode; }

    private void Update()
    {
        if(isShootingMode)
        {
            if(Input.GetMouseButtonDown(0))
            {

            }
            else if (Input.GetMouseButton(0))
            {
                var mouseViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                var ballViewportnPos = Camera.main.WorldToViewportPoint(this.transform.position);
                this.forceFactor = Vector2.Distance(ballViewportnPos, mouseViewportPos) * 2;
                Debug.Log(forceFactor);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                shoot = true;
                isShootingMode = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(shoot)
        {
            shoot = false;
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;

            rb.AddForce(direction * force * forceFactor, ForceMode.Impulse);
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
