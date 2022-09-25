using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolfType
{
    WolfBaby,
    WolfNormal,
    WolfBoss

}

public class Spawn : MonoBehaviour
{

    public float timer = 0f;
    public float time = 1f;

    [Tooltip("根据类型用到的prefab")] private GameObject prefab;
    [Tooltip("生成类型")] public WolfType wolfType;
    public GameObject wolfBabyPrefab;
    public GameObject wolfNormalPrefab;
    public GameObject wolfBossPrefab;
    //
    public List<Transform> wolfList;
    public int max_num=5;
    public int current_num;


    public static Spawn  _instance;

	void Awake()
    {
        _instance = this;
    }
        
    // Start is called before the first frame update
    void Start()
    {
        InitType();
    }

    private void InitType()
    {
        switch (wolfType)
        {
            case WolfType.WolfBaby: prefab = wolfBabyPrefab;break;
            case WolfType.WolfNormal: prefab = wolfNormalPrefab;break;
            case WolfType.WolfBoss: prefab = wolfBossPrefab;break;
            default: prefab = wolfBabyPrefab; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wolfList.Count < max_num)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                timer = 0f;
                SpawnWolf();
            }
        }

    }
    void SpawnWolf()
    {
        Vector3 pos;
        float posX = transform.position.x + Random.Range(-5f, 5f); //不包括右边界
        float posZ = transform.position.z + Random.Range(-5f, 5f); //不包括右边界
        pos = new Vector3(posX, transform.position.y, posZ);
        //
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        go.transform.parent = transform;
        //
        wolfList.Add(go.transform);
        current_num = wolfList.Count;
    }
    public void Remove(Transform wolf)//记录current_num，为了保持max_num的数量
    {
        if (wolfList.Contains(wolf))
        {
            wolfList.Remove(wolf);
            current_num = wolfList.Count;
        }  
    }





}
