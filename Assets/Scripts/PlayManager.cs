using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] BallController ballController;

    [SerializeField] CameraController camController;

    private bool isBallOutside;

    private bool isBallTeleporting;

    private bool isGoal;

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

    public void OnBallGoalEnter()
    {
        isGoal = true;
        ballController.enabled = false;
    }

    public void OnBallOutside()
    {
        if (isGoal)
            return;

        if(isBallTeleporting == false)
            Invoke("TeleportBallLastPosition", 1);

        isBallOutside = true;
        isBallTeleporting = true;
    }

    public void TeleportBallLastPosition()
    {
        TeleportBall(lastBallPosition);
    }

    public void TeleportBall (Vector3 targetPosition)
    {
        var rb = ballController.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        ballController.transform.position = lastBallPosition;

        rb.isKinematic = false;
        isBallOutside = false;
        isBallTeleporting = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
