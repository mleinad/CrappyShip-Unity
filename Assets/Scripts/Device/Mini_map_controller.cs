using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Mini_map_controller : MonoBehaviour
{

public float posX=0 , posY=0;

public RectTransform player_map_pos, content_transform;
public Transform player_pos;

public CinemachineVirtualCamera virtual_camera;

public bool rotate;

void Start()
{
    Debug.Log( content_transform.rect.size);
}


void Update()
{
    player_map_pos.position = new Vector3(player_pos.position.x, 0, player_pos.position.z);

    if(rotate){
        virtual_camera.transform.rotation = Quaternion.Euler(90, player_pos.eulerAngles.y, 0);
    }


}

private Vector2 WorldPositionToMapPosition(Vector3 world_pos)
{
    var pos = new Vector2(world_pos.x, world_pos.z);
    var worldSize = new Vector2(63, 55);
    var scale_ratio =  content_transform.rect.size/worldSize;
    Matrix4x4 matrix = Matrix4x4.TRS(-content_transform.rect.size/2, Quaternion.identity, scale_ratio);
    return matrix.MultiplyPoint3x4(pos);
}


}
