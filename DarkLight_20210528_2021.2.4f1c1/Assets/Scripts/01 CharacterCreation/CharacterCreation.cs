using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreation : MonoBehaviour
{


    [Tooltip("角色数组")] public GameObject[] characterPrefabArray;
    [Tooltip("角色索引值")] public int index=0;//看的
    [Tooltip("角色预制体实例的对象")] private GameObject[] goArray;
    [Tooltip("角色的位置")] public Transform creationPlace;
    [Tooltip("角色名字")] public UIInput characterNameInput; //
    // Start is called before the first frame update
    void Start()
    {
        if (characterPrefabArray.Length != 0)
        {
            //实例 隐藏
            goArray = new GameObject[characterPrefabArray.Length];
            for (int i = 0; i < characterPrefabArray.Length; i++)
            {
                goArray[i] = Instantiate(characterPrefabArray[i], creationPlace.position, creationPlace.rotation);
                goArray[i].SetActive(false);
                goArray[i].transform.parent = creationPlace;
            }
            goArray[0].SetActive(true);
        }
    }

    public void PreCharacter()
    {
        goArray[index].SetActive(false);

        index = index == 0 ? (characterPrefabArray.Length - 1) : --index;
        goArray[index].SetActive(true);
    }
    public void NextCharacter()
    {
        goArray[index].SetActive(false);

        index = index == characterPrefabArray.Length - 1 ? 0 : ++index;
        goArray[index].SetActive(true);
    }
    public void LoadScene()
    {
        //保存
        PlayerPrefs.SetString("SelectedPlayerName", characterNameInput.value);
        PlayerPrefs.SetInt("SelectedPlayerIndex", index);
        //打印
        print(PlayerPrefs.GetInt("SelectedPlayerIndex"));
        print(PlayerPrefs.GetString("SelectedPlayerName"));
        //下一个场景
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);//2是数据，要分离开，但我还没学过
    }
}
