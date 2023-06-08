using Oculus.Platform.Samples.VrBoardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public GameObject BallPrefab;
    [Header("Circle Attributes")]
    public float CircleRadius;
    public int NumberOfBalls;
    public float depthOffset;

    [Header("Ball Attributes")]
    public float BallSize = 0.06f;
    public Color BallColor = new Color(150, 200, 150, 0);

    [Header("Parent Object")]
    public GameObject ParentObject;

    [Header("Study Settings")]
    public int NumofLayers = 1;

    [Header("isObstacle Switch")]
    public bool isObstacle = false;
    public Color ObstacleColor = new Color(150, 150, 150, 0);
    public Material ObstacleMatrial;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBalls(CircleRadius, NumberOfBalls, transform.position.z + depthOffset);

        if (NumofLayers > 1)
        {
            for (int i = 1; i <= NumofLayers; i++)
            {
                SpawnBalls(CircleRadius, NumberOfBalls, transform.position.z + depthOffset * i);
            }
        }
    }

    void SpawnBalls(float Radius, int BallCount, float CenterDepthCoordinate)
    {
        ParentObject = GameObject.Find("Spawner");
        Renderer BallRenderer = BallPrefab.GetComponent<Renderer>();

        if (isObstacle == true)
        {
            print("true");
            if (BallRenderer != null && BallRenderer.sharedMaterial != null)
            {
                BallRenderer.sharedMaterial.SetColor("_Color", (ObstacleColor));
            }
        }
        else if (isObstacle == false)
        {
            if (BallRenderer != null && BallRenderer.sharedMaterial != null)
            {
                BallRenderer.sharedMaterial.SetColor("_Color", (BallColor));
            }
        }

        Vector3 BallScale = new Vector3(BallSize, BallSize, BallSize);

        for (int i = 0; i < BallCount; i++)
        {
            float angle = i * (360f / BallCount);

            float x = transform.position.x + Radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = transform.position.y + Radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 position = new Vector3(x, y, CenterDepthCoordinate);

            GameObject ball = Instantiate(BallPrefab, position, Quaternion.identity);
            ball.transform.localScale = BallScale;
            ball.transform.parent = ParentObject.transform;
        }
    }
}
