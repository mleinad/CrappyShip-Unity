
using UnityEngine;

public class MiniMapElement : MonoBehaviour
{
    // Start is called before the first frame update
   

    [SerializeField]
    private Sprite sprite;
   
    [SerializeField]
    GameObject element_prefab;

    [SerializeField]
    Color color;

    private RectTransform element_transform;
    private Transform canvas_transform;
    private SpriteRenderer spriteRenderer;

    void Awake(){
    }

    void Start()
    {
        canvas_transform = Mini_map_controller.Instance.GetCanvasTransform();


        InstatiateElement();
    }

    // Update is called once per frame
    void Update()
    {
        element_transform.position = new Vector3(transform.position.x, 0, transform.position.z);    //update frame by frame, might be unecessary
    }


    void InstatiateElement(){
        
        GameObject element;
        element = Instantiate(element_prefab, canvas_transform);
        spriteRenderer = element.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
       
        element_transform = element.GetComponent<RectTransform>();
    }


    public Sprite GetSprite()
    {
        return sprite;
    }

}
