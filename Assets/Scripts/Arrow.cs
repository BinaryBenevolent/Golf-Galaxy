using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float[] colorStops;

    [SerializeField] private BallController ball;

    private Gradient gradient;

    [SerializeField] private Renderer arrowRenderer;

    private void Start()
    {
        arrowRenderer.material.color = Color.green;

        gradient = new Gradient();

        GradientColorKey[] colorKeys = new GradientColorKey[3];
        colorKeys[0] = new GradientColorKey(Color.green, 0f);
        colorKeys[1] = new GradientColorKey(Color.yellow, 0.5f);
        colorKeys[2] = new GradientColorKey(Color.red, 1f);

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1f, 0f);
        alphaKeys[1] = new GradientAlphaKey(1f, 1f);

        gradient.SetKeys(colorKeys, alphaKeys);
    }

    private void Update()
    {
        Color color = gradient.Evaluate(ball.ForceFactor);
        arrowRenderer.material.color = color;
    }
}
