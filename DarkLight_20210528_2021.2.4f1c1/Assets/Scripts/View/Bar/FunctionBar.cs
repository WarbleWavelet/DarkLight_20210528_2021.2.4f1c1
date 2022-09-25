using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBar : MonoBehaviour
{

    public UIButton statusButton;
    public UIButton bagButton;
    public UIButton equipmentButton;
    public UIButton skillButton;
    public UIButton settingButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnStatusButtonClick()
    {
        StatusPannel._instance.ShowWindow();
    }
    public void OnBagButtonClick()
    {
        BagPannel._instance.ShowWindow();
    }
    public void OnEquipmentButtonClick()
    {
        EquipPannel._instance.ShowWindow();
    }
    public void OnSkillButtonClick()
    {
        SkillPannel._instance.ShowWindow();
    }
    public void OnSettingButtonClick()
    {
        SettingPannel._instance.ShowWindow();
    }




}
