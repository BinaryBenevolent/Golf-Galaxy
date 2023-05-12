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

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(shoot)
        {
            shoot = false;
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;

            rb.AddForce(direction * force, ForceMode.Impulse);
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
            shoot = true;
    }
}
