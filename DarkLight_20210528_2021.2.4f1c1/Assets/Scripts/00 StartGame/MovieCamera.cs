using UnityEngine;
using System.Collections;

public class MovieCamera : MonoBehaviour {

    public float speed = 10f;

    public float endZ = 240f;
	public float deltaZ = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x< endZ)
        {//还没有达到目标位置，需要移动
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
        
	}
}  
