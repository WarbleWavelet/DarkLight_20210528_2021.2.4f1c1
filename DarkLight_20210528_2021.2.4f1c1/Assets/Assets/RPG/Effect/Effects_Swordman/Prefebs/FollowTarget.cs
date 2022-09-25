using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{

	public GameObject player;
	private Vector3 offset;

	//拉近
	public float scrollSpeed = 10f;//拉伸速度
	public float distance;//实际拉伸
	public float minDistance = 3.2f;//最大拉伸
	public float maxDistance = 30f;//最小拉伸

	//旋转
	private bool isRotate = false;
	public float rotateSpeed = 10f;



	void Start()
	{
		player = Player._instance.gameObject;
		offset = transform.position - player.transform.position;
		
		//offset = new Vector3(0, offset.y, offset.z);//x=0，左右不偏移
	}

	// Update is called once per frame
	void Update()
	{
		ProcessTarget();
	}
	void ProcessTarget()
	{
		if (Player._instance == null || UICamera.isOverUI)
		{ 
		    return;
		}
		//
		transform.position = player.transform.position + offset;
		transform.LookAt(player.transform.position);

		RotateView();
		ScrollView();
	}
	void ScrollView()//相机拉伸
	{
		distance = offset.magnitude;
		distance -= scrollSpeed * Input.GetAxis("Mouse ScrollWheel");
		distance = Mathf.Clamp(distance, minDistance, maxDistance);//钳制
		offset = offset.normalized * distance;
	}
	void RotateView()//相机旋转
	{			
		if (Input.GetMouseButtonDown(1))
		{
			isRotate = true;
		}
		if (Input.GetMouseButtonUp(1))
		{
			isRotate = false;
		}
		if (isRotate)
		{
			//记录
			Vector3 originalPosition = transform.position;
			Quaternion originalRotation = transform.rotation;
			//赋值
			transform.RotateAround(player.transform.position, transform.up, rotateSpeed * Input.GetAxis("Mouse X")); 
			transform.RotateAround(player.transform.position, transform.right, rotateSpeed * Input.GetAxis("Mouse Y"));

			//限制范围
			if (transform.eulerAngles.x < 10 || transform.eulerAngles.x > 80)
			{
				print ("超范围了");
				transform.position = originalPosition;
				transform.rotation = originalRotation;
			}			                                           
		}

		offset = transform.position - player.transform.position;//旋转影响相机的位置，offset发生变化
	}
	
}
