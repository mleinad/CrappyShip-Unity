using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Mini_map_controller : MonoBehaviour
{

 public static Mini_map_controller Instance { get; private set; }
public float posX=0 , posY=0;

public RectTransform player_map_pos, content_transform;
private Transform player_pos;

[SerializeField]
Canvas canvas;

public CinemachineVirtualCamera virtual_camera;

private float zoom = 10f;
public bool rotate;


void Awake()
{
        // Implement Singleton Pattern
        if (Instance == null)
        {
            Instance = this;  // Set this instance as the singleton instance
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Destroy the extra instance to ensure there is only one
        }

        DontDestroyOnLoad(gameObject);  // Optional: Persist the singleton across scenes
}

void Start()
{
  player_pos = Player.Instance.transform;
  virtual_camera.m_Lens.OrthographicSize = zoom;   
}


void Update()
{
    player_map_pos.position = new Vector3(player_pos.position.x, 0, player_pos.position.z);


    if(Input.GetKey(KeyCode.Z)){
        ZoomIn();
    }

    
    if(Input.GetKey(KeyCode.X)){
        ZoomOut();
    }
}

/*
private Vector2 WorldPositionToMapPosition(Vector3 world_pos)
{
    var pos = new Vector2(world_pos.x, world_pos.z);
    var worldSize = new Vector2(63, 55);
    var scale_ratio =  content_transform.rect.size/worldSize;
    Matrix4x4 matrix = Matrix4x4.TRS(-content_transform.rect.size/2, Quaternion.identity, scale_ratio);
    return matrix.MultiplyPoint3x4(pos);
}

*/
    void ZoomIn(){
        zoom -= 3f * Time.deltaTime;
        if(zoom <= 5) zoom =5;
        virtual_camera.m_Lens.OrthographicSize = zoom;

    }
    void ZoomOut(){
        zoom += 3f * Time.deltaTime;
        if(zoom >= 30) zoom =30;
        virtual_camera.m_Lens.OrthographicSize = zoom;
    }



    public Transform GetCanvasTransform() => canvas.transform;

}
