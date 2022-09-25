using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public List<GameObject> playerList;
    public Vector3 spawnPos;
    public int index;
    public new string name;     


    // Start is called before the first frame update
    void Awake()
    {
        //if (PlayerPrefs.HasKey("SelectedPlayerName") )
        //{
        //    name = PlayerPrefs.GetString("SelectedPlayerName"); 
        //    spawnPos = new Vector3(236.27f, 0f, 155.61f);
        //    Instantiate(playerList[index], spawnPos, Quaternion.identity);
        //}
        //if (PlayerPrefs.HasKey("SelectedPlayerIndex"))
        //{
        //    index = PlayerPrefs.GetInt("SelectedPlayerIndex"); 
        //    Player._instance.name = name;//PlayerStatus做了Update显示name，所以直接设
        //}       
    }


}
