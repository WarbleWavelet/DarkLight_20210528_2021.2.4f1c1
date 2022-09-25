using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : Skill
{
    public UISprite skillSprite;
    public UILabel nameLabel;
    public UILabel effectTypeLabel;
    public UILabel descriptionLabel;
    public UILabel costMpLabel;
    public UISprite maskSprite;

    // Start is called before the first frame update
    void Start()
    {
     //  transform.parent.GetComponent<UIWidget>().depth = 10;
     //  transform.parent.GetComponent<UIWidget>().depth = 10;
        //GetComponent<UIDragScrollView>().scrollView = transform.parent.parent.GetComponent<UIScrollView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetValue(Skill skill)
    {
        id =skill.id;
        name =skill.name;
        icon_name =skill.icon_name;
        des =skill.des;

        coolTime =skill.coolTime;
        applyType =skill.applyType;

        lv =skill.lv;
        roleLv =skill.roleLv;
        costMp =skill.costMp;

        effectProperty =skill.effectProperty;
        effectTarget =skill.effectTarget;
        effectTime =skill.effectTime;
        effectType =skill.effectType;
        effectValue =skill.effectValue;
        releaseDis =skill.releaseDis;
        //
        skill_efx_name = skill.skill_efx_name;
        player_ani_name = skill.player_ani_name; 
        player_ani_time = skill.player_ani_time;
    }
   
    //

    
    public void UnlockSkillItem()//根据人物等级和职业显示技能
    {
        if (roleLv > Player._instance.level || applyType!=Player._instance.applyType)
        {
            maskSprite.gameObject.SetActive(true);
        }
        else
        {
            maskSprite.gameObject.SetActive(false);
        }       
    }
    public void SetUI()
    {
        skillSprite.spriteName = icon_name;
        nameLabel.text = name;
        switch (effectType)
        {
            case EffectType.Passive: effectTypeLabel.text = "增益"; break;
            case EffectType.Buff: effectTypeLabel.text = "增强"; break;
            case EffectType.SingleTarget: effectTypeLabel.text = "单个目标"; break;
            case EffectType.MultiTarget: effectTypeLabel.text = "多个目标"; break;
        }
        effectTypeLabel.text = effectType.ToString();
        descriptionLabel.text = des;
        costMpLabel.text = costMp.ToString();
    }
#if false
    public void SetUIBySkill()
    {
        skillSprite.spriteName = skill.icon_name;
        nameLabel.text = name;
        switch (skill.effectType)
        {
            case EffectType.Passive: effectTypeLabel.text = "增益"; break;
            case EffectType.Buff: effectTypeLabel.text = "增强"; break;
            case EffectType.SingleTarget: effectTypeLabel.text = "单个目标"; break;
            case EffectType.MultiTarget: effectTypeLabel.text = "多个目标"; break;
        }
        effectTypeLabel.text = skill.effectType.ToString();
        descriptionLabel.text = skill.description;
        costMpLabel.text = skill.costMp.ToString();
    }
    public void SetValueBySkill(Skill skill)
    {
        this.skill.id = skill.id;
        this.skill.name = skill.name;
        this.skill.icon_name = skill.icon_name;
        this.skill.description = skill.description;

        this.skill.coolTime = skill.coolTime;
        this.skill.applyType = skill.applyType;

        this.skill.lv = skill.lv;
        this.skill.roleLv = skill.roleLv;
        this.skill.costMp = skill.costMp;

        this.skill.effectProperty = skill.effectProperty;
        this.skill.effectTarget = skill.effectTarget;
        this.skill.effectTime = skill.effectTime;
        this.skill.effectType = skill.effectType;
        this.skill.effectValue = skill.effectValue;
        this.skill.releaseDistance = skill.releaseDistance;
    }
    public void UnlockSkillItemBySkill()//根据人物等级和职业显示技能
    {
        if (skill.roleLv > Player._instance.level || skill.applyType != Player._instance.applyType)
        {
            maskSprite.gameObject.SetActive(true);
        }
        else
        {
            maskSprite.gameObject.SetActive(false);
        }
    }
    #endif


}
