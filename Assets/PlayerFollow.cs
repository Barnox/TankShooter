using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject followObject;
    public Vector3 offsetDistance;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followObject.transform.position + offsetDistance;
        transform.LookAt(followObject.transform);
    }
}
