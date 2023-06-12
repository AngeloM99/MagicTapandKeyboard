using Microsoft.MixedReality.Toolkit.SceneSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    public Color targetColor;
    public List<GameObject> TargetObjects = new List<GameObject> { };
    public List<int> TargetList = new List<int> { };

    [Header("Prefabs")]
    public GameObject ObstaclePrefab;
    public GameObject TargetPrefab;

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

            if (!isObstacle)
            {
                if (TargetList.Contains(i))
                {
                    GameObject Target = Instantiate(TargetPrefab, position, Quaternion.identity);
                    Target.transform.localScale = BallScale;
                    Target.transform.parent = ParentObject.transform;

                    TargetObjects.Add(Target);
                    //print(i);
                    Transform IndicatorText = Target.transform.Find("Text (TMP)");
                    GameObject IndicatorTextObject = IndicatorText.gameObject;
                    TextMeshPro text = IndicatorTextObject.GetComponent<TextMeshPro>();
                    IndicatorTextObject.SetActive(true);
                    if (text != null)
                    {
                        print("1111");
                    }

                    text.fontSize = 50;

                    for (int num = 1; num < TargetObjects.Count; num++)
                    {
                        text.text = num.ToString();
                    }
                }
                else if (!TargetList.Contains(i))
                {
                    GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                    Obstacles.transform.localScale = BallScale;
                    Obstacles.transform.parent = ParentObject.transform;

                    Renderer BallRenderer = ObstaclePrefab.GetComponent<Renderer>();
                }
            }
            else
            {
                GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                Obstacles.transform.localScale = BallScale;
                Obstacles.transform.parent = ParentObject.transform;
            }
        }
    }
}
