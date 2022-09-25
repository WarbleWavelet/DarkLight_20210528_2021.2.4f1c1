using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutItem : MonoBehaviour
{
    public KeyCode keyCode;
    public Transform item;
    public Transform sprite;
    Player player = Player._instance;
    Entity entity = Entity._instance;

    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.GetChild(0);
        player = Player._instance;
        entity = Entity._instance;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
            PlayerInput();
    }



    void PlayerInput()
    {      
        if (sprite.childCount <1) return;
        //
        item = sprite.GetChild(0);//挂在sprite下


        ItemGroup itemGroup = item.GetComponent<ItemGroup>();
        SkillItem skillItem = item.GetComponent<SkillItem>();

        if (itemGroup != null)//物品
        {
            itemGroup.GetComponent<ItemGroup>().OnButtonDoubleClick();//用双击测试故此命名
        }
        else if (skillItem != null)//技能
        {
            Entity._instance.skillAttack.EnterSkill(skillItem);
        }
        
    }
    //
    public void AddSkillItem(Skill skill)//向快捷栏设置
    {
        //UI
        sprite.GetComponent<UISprite>().spriteName = skill.icon_name;
        //可以不设，编辑器里面设置好了
        sprite.transform.localPosition = new Vector3(76f,76f,0f);
        sprite.GetComponent<UISprite>().width = 100;
        sprite.GetComponent<UISprite>().height = 100;
        //
        //实例
        GameObject go = new GameObject();
        go.transform.position = transform.position;
        go.transform.parent = sprite;//父节点为背景图
        go.transform.localScale = Vector3.one;
        go.AddComponent<SkillItem>().SetValue(skill);


    }

    public void AddBagItem(ItemGroup item)
    {
        GameObject go = Instantiate(BagPannel._instance.itemGroupPrefab);
        go.transform.position = transform.position;
        go.transform.parent = sprite;//父节点为背景图
        go.transform.localScale = Vector3.one;
        go.GetComponent<MyDragDrop>().cloneOnDrag = false;
        go.GetComponent<ItemGroup>().SetValue(item);
    }

}
