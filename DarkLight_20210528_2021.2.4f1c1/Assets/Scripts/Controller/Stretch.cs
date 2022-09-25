using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretch : MonoBehaviour
{
    public float mouseScrollWheel;//鼠标滑轮，外正内负

    public GameObject player;
    public float distanceTotal;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        distanceTotal = (transform.position - player.transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        distance = (mouseScrollWheel + 1) * distanceTotal;
        transform.position = new Vector3(transform.position.x, transform.position.y, distance);
    }
}
