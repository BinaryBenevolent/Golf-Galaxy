using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Collider col;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float force;

    [SerializeField] private LineRenderer aimLine;

    [SerializeField] private Transform aimWorld;

    [SerializeField] private int shootCount;

    private bool shoot;

    private bool isShootingMode;

    private float forceFactor;
    private Vector3 forceDirection;

    private Ray ray;
    private Plane plane;

    public bool IsShootingMode { get => isShootingMode; }
    public float ForceFactor { get => forceFactor; }
    public int ShootCount { get => shootCount; }

    [SerializeField] private UnityEvent<int> OnShoot = new UnityEvent<int>();

    private void Start()
    {
        shootCount = 0;
    }

    private void Update()
    {
        if(isShootingMode)
        {
            if(Input.GetMouseButtonDown(0))
            {
                aimLine.gameObject.SetActive(true);
                aimWorld.gameObject.SetActive(true);
                plane = new Plane(Vector3.up, this.transform.position);
            }
            else if (Input.GetMouseButton(0))
            {
                var mouseViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                var ballViewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
                var pointerDirection = ballViewportPos - mouseViewportPos;
                pointerDirection.z = 0;            

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                plane.Raycast(ray, out var distance);
                forceDirection = this.transform.position - ray.GetPoint(distance);
                forceDirection.Normalize();

                forceFactor = pointerDirection.magnitude * 2;

                aimWorld.transform.position = this.transform.position;
                aimWorld.forward = forceDirection;
                aimWorld.localScale = new Vector3(1, 1, forceFactor + 0.5f);

                var ballScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
                var mouseScreenPos = Input.mousePosition;

                ballScreenPos.z = 1f;
                mouseScreenPos.z = 1f;

                var positions = new Vector3[] { Camera.main.ScreenToWorldPoint(ballScreenPos), Camera.main.ScreenToWorldPoint(mouseScreenPos) };
                aimLine.SetPositions(positions);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                shoot = true;
                isShootingMode = false;

                aimLine.gameObject.SetActive(false);
                aimWorld.gameObject.SetActive(false);

                shootCount += 1;

                OnShoot.Invoke(shootCount);
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

        if(rb.velocity.sqrMagnitude < 0.5f && rb.velocity.sqrMagnitude > 0f)
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
        if (this.IsMove())
            return;

        isShootingMode = true;
    }
}
