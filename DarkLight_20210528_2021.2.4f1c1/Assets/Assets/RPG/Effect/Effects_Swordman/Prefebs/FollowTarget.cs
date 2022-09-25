using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{

	public GameObject player;
	private Vector3 offset;

	//����
	public float scrollSpeed = 10f;//�����ٶ�
	public float distance;//ʵ������
	public float minDistance = 3.2f;//�������
	public float maxDistance = 30f;//��С����

	//��ת
	private bool isRotate = false;
	public float rotateSpeed = 10f;



	void Start()
	{
		player = Player._instance.gameObject;
		offset = transform.position - player.transform.position;
		
		//offset = new Vector3(0, offset.y, offset.z);//x=0�����Ҳ�ƫ��
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
	void ScrollView()//�������
	{
		distance = offset.magnitude;
		distance -= scrollSpeed * Input.GetAxis("Mouse ScrollWheel");
		distance = Mathf.Clamp(distance, minDistance, maxDistance);//ǯ��
		offset = offset.normalized * distance;
	}
	void RotateView()//�����ת
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
			//��¼
			Vector3 originalPosition = transform.position;
			Quaternion originalRotation = transform.rotation;
			//��ֵ
			transform.RotateAround(player.transform.position, transform.up, rotateSpeed * Input.GetAxis("Mouse X")); 
			transform.RotateAround(player.transform.position, transform.right, rotateSpeed * Input.GetAxis("Mouse Y"));

			//���Ʒ�Χ
			if (transform.eulerAngles.x < 10 || transform.eulerAngles.x > 80)
			{
				print ("����Χ��");
				transform.position = originalPosition;
				transform.rotation = originalRotation;
			}			                                           
		}

		offset = transform.position - player.transform.position;//��תӰ�������λ�ã�offset�����仯
	}
	
}
