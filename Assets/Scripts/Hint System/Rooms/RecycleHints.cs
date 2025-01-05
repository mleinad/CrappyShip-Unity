using System;
using UnityEngine;

public class RecycleHints : MonoBehaviour, HintData
{
    public Transform pos1, pos2;

    public float globalTime, relativeTime;

    [SerializeField] public float timeTrigger1, timeTrigger2, timeTrigger3, timeTrigger4;
    
    private bool globalEntered = false, localEntered;
    
    public Vector3[] Area 
    {
        get
        {
            return new Vector3[] { pos1.position, pos2.position };
        }
        set
        {
            if (value.Length == 2)
            {
                pos1.position = value[0];
                pos2.position = value[1];
            }
            else
            {
                throw new ArgumentException("Area must contain exactly two Vector3 values.");
            }
        } 
    }
    HintsUIManager hintsUIManager;

    private void Start()
    {
        hintsUIManager = HintsUIManager.Instance;
    }

    private void Update()
    {
        if (!globalEntered)
        {
            globalEntered = true;
            globalTime = 0;
        }
        if (PlayerInArea())
        { 
            Tick();           
            
            CheckInactivity();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Calculate center and size of the box
        Vector3 center = (Area[0] + Area[1]) / 2;
        Vector3 size = Area[0] - Area[1];

        // Draw the box
        Gizmos.DrawWireCube(center, size);
    }
    
    public bool PlayerInArea()
    {
        Vector3 playerPosition = Player.Instance.transform.position;

        // Get the min and max bounds from the corners
        Vector3 minBounds = Vector3.Min(Area[0], Area[1]);
        Vector3 maxBounds = Vector3.Max(Area[0], Area[1]);

        // Check if the player's position is within the bounds
        return playerPosition.x >= minBounds.x && playerPosition.x <= maxBounds.x &&
               playerPosition.y >= minBounds.y && playerPosition.y <= maxBounds.y &&
               playerPosition.z >= minBounds.z && playerPosition.z <= maxBounds.z;
    }


    void CheckInactivity()
    {
        if (globalTime >= timeTrigger1)
        {
            //throw hint
        }
    }

    void Tick()
    {
        globalTime += Time.deltaTime;    
    }
    
    
}
