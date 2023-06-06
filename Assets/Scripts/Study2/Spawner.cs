using Oculus.Platform.Samples.VrBoardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject BallPrefab;
    [Header("Circle Attributes")]
    public float CircleRadius;
    public int NumberOfBalls;
    [Header("Ball Attributes")]
    public float BallSize = 0.06f;

    [Header("Parent Object")]
    public GameObject ParentObject;
    // Start is called before the first frame update
    void Start()
    {
        SpawnBalls();
    }

    void SpawnBalls()
    {
        ParentObject = GameObject.Find("CameraLock");

        Vector3 BallScale = new Vector3(BallSize, BallSize, BallSize);

        for (int i = 0; i<NumberOfBalls; i++)
        {
            float angle = i * (360f / NumberOfBalls);

            float x = transform.position.x + CircleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = transform.position.y + CircleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 position = new Vector3(x, y, transform.position.z);

            GameObject ball = Instantiate(BallPrefab, position, Quaternion.identity);
            ball.transform.localScale = BallScale;
            ball.transform.parent = ParentObject.transform;
        }
    }
}
