using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is to define behaviors for obstacle objects.
public class ObstacleBehavior : MonoBehaviour
{
    public Color ObstacleColor;
    public BallSpawner SpawnerScript;
    public GameObject SpawnerObject;

    // Start is called before the first frame update
    void Start()
    {
        SpawnerObject = gameObject;
        SpawnerScript = SpawnerObject.GetComponent<BallSpawner>();

        if(SpawnerScript != null)
        {
            if (SpawnerScript.isObstacle == true)
            {
                //print("Obstacle");
                SetObstacleColor();
            }
        }
    }

    void SetObstacleColor()
    {
        SpawnerObject = gameObject;
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

        for(int i = 0; i < SpawnerObject.transform.childCount; i++)
        {
            Transform child = SpawnerObject.transform.GetChild(i);
            Renderer childRenderer = child.GetComponent<Renderer>();

            if (childRenderer != null)
            {
                childRenderer.GetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_Color", ObstacleColor);
                childRenderer.SetPropertyBlock(propertyBlock);
            }
            SetObstacleColor();
        }
    }

    void OverrideInMaterial()
    {

    }
}
