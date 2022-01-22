using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public float cameraOffset;

    void Start()
    {
        Camera.main.orthographicSize = cameraOffset;
        CenterCameraOnMap();
    }

    private void CenterCameraOnMap() 
    {
        if (levelGenerator != null)
        {
            Camera.main.transform.position = new Vector3(levelGenerator.mapWidth / 2f, levelGenerator.mapHeight / 2f, Camera.main.transform.position.z);
        }
    }
}
