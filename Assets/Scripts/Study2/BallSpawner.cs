using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public Color targetColor;
    public List<GameObject> TargetObjects = new List<GameObject> { };
    public List<int> TargetList = new List<int> { };

    public GameObject BallPrefab;
    [Header("Circle Attributes")]
    public float CircleRadius;
    public int NumberOfBalls;
    public float depthOffset;

    [Header("Ball Attributes")]
    public float BallSize = 0.06f;

    [Header("Parent Object")]
    public GameObject ParentObject;

    [Header("Study Settings")]
    public int NumofLayers = 1;

    [Header("isObstacle Switch")]
    public bool isObstacle = false;

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
        ParentObject = gameObject;

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

            Renderer BallRenderer = BallPrefab.GetComponent<Renderer>();

            if (TargetList.Contains(i))
            {
                TargetObjects.Add(ball);
                //print(i);
                Transform IndicatorText = ball.transform.Find("Text (TMP)");
                GameObject IndicatorTextObject = IndicatorText.gameObject;
                TextMeshPro text = IndicatorTextObject.GetComponent<TextMeshPro>();
                IndicatorTextObject.SetActive(true);
                if (text != null)
                {
                    print("1111");
                }

                text.fontSize = 50;

                for (int num = 0; num < TargetObjects.Count; num++)
                {
                    text.text = num.ToString();
                }
            }
        }
    }
}
