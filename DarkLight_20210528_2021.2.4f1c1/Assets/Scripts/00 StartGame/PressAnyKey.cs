using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    public GameObject newGameButton;
    public GameObject loadGameButton;
    public GameObject pressAnyKey;

    private float time = 4f;
    // Start is called before the first frame update
    void Start()
    {
        newGameButton.SetActive(false);  
        loadGameButton.SetActive(false);
        pressAnyKey.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (Input.anyKeyDown && time<=0)//因为开头有个动画，time秒后
        {
            newGameButton.SetActive(true);
            loadGameButton.SetActive(true);
            pressAnyKey.SetActive(false);
        }
    }

    public void OnNewGame()//开始游戏
    {
        PlayerPrefs.SetInt("LoadFrom", 0);
        SceneManager.LoadScene(1);
    }
    public void OnLoadGame()//加载游戏
    {
        PlayerPrefs.SetInt("LoadFrom", 1);
        SceneManager.LoadScene(1);
    }
}
