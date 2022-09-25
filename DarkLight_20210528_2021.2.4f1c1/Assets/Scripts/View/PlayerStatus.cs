using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public UILabel lvLabel;
    public UILabel nameLabel;

    public UISlider expBar;
    public UISlider hpBar;
    public UISlider mpBar;


    public static PlayerStatus _instance;

	void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdatePlayerStatus();
    }

    public void UpdatePlayerStatus()
    {
        if (Player._instance == null) return;//死了
        //
        Player player = Player._instance;
        

        lvLabel.text="Lv:"+ player.level.ToString();
        nameLabel.text= player.name;

        
        float hp = (float)player.hp /  (float)player.maxHp;
        float mp = (float)player.mp /  (float)player.maxMp;
        
        hpBar.GetComponent<UISlider>().value = hp;
        mpBar.GetComponent<UISlider>().value = mp;

        if (player.level > 6) return;//只做了6的经验表，但是为了解锁技能，我设100级，所以...
        //TODO 
      //  float exp = (float)player.exp / (float)player.maxExpLevelList[player.level + 1];
      //  expBar.GetComponent<UISlider>().value = exp;
    }
}
