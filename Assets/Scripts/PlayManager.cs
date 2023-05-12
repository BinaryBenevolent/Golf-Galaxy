using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] BallController ballController;

    [SerializeField] CameraController camController;

    private bool isBallOutside;

    private bool isBallTeleporting;

    private Vector3 lastBallPosition;

    private void Update()
    {
        if(ballController.IsShootingMode)
        {
            lastBallPosition = ballController.transform.position;
        }

        var inputAction = Input.GetMouseButton(0) && ballController.IsMove() == false && ballController.IsShootingMode == false && isBallOutside == false;
        camController.SetInputActive(inputAction);
    }

    public void OnBallOutside()
    {
        if(isBallTeleporting == false)
            Invoke("MoveBallLastPosition", 1);

        isBallOutside = true;
        isBallTeleporting = true;
    }

    public void MoveBallLastPosition()
    {
        var rb = ballController.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        ballController.transform.position = lastBallPosition;

        rb.isKinematic = false;
        isBallOutside = false;
        isBallTeleporting = false;
    }
}
