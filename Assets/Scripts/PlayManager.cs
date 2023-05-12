using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] BallController ballController;

    [SerializeField] CameraController camController;

    private void Update()
    {
        var inputAction = Input.GetMouseButton(0) && ballController.IsMove() == false && ballController.IsShootingMode == false;
        camController.SetInputActive(inputAction);
    }
}
