using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    #region 属性

    [Header("Human.Clip")]
    public Animator animator;
    public string idleClip;
    public string walkClip;
    public string deathClip;
    public string attack1Clip;
    public string attack2Clip;
    public string damage1Clip;
    public string damage2Clip;
    //
    public State state;
    [HideInInspector] public new Animation animation;
    //
    [Tooltip("视野范围")] public float range_sight = 5f;
    [Tooltip("攻击范围")] public float range_atk = 1f;
    //
    [Tooltip("普攻")] public float atk_normal = 10;
    [Tooltip("攻速")] public float atk_Speed = 1f;
    [Tooltip("暴击")] public float atk_crazy;
    [Tooltip("暴击率")] public float atk_crazy_rate = 0.25f;
    //

    [Tooltip("普攻计时器")] public float atk_timer = 0f;
    [Tooltip("普攻计时")] public float atk_time = 1f;

    [Tooltip("要攻击的目标")] public Transform target;
    //
    [Tooltip("闪避率")] public float missRate = 0.2f;
    [Tooltip("闪避音效")] public AudioClip missAudioClip;
    //
    [Tooltip("预制体，加了UIFollowTarget的新的预制体")] public GameObject hudTextPrefab;
    [Tooltip("预制体的实例，全局定义，方便调用")] public GameObject hudTextGo;
    [Tooltip("预制体生成对象上的组件。用来add文本")] public HUDText hudTxt;
    [Tooltip("HUDText位置")] public Transform hudTextTrans;
    //[Tooltip("就上面那个")] public GameObject hudTextFollow;
    [Tooltip("NGUI的下相机，直接赋值报空错误")] public Camera uiCamera;
    [Tooltip("HUDText存在的时间")] public float stayDuration = 1f;
    //
    [Tooltip("身体变红的锁，类似于计时器的效果")] public bool isDamaged = false;
    //

    [Tooltip("生命值")] public float hp = 200;
    #endregion

    #region 生命
    void Start()
    {
        atk_crazy = atk_normal * (1 + atk_crazy_rate);
        if (null != GetComponent<Animator>())
        { 
             animator = GetComponent<Animator>();
        }
       
    }
    #endregion
    

    #region 辅助

    public IEnumerator ShowBodyRed()
    {
        Material material = GetComponentInChildren<Renderer>().material;
        Color oldColor = material.color;
        Color newColor = Color.red;
        material.color = newColor;
        yield return new WaitForSeconds(1f);
        material.color = oldColor;

        isDamaged = false;
    }
    public virtual void InitHudText()
    {
        hudTextGo = NGUITools.AddChild(HudTextParent._instance.gameObject, hudTextPrefab);    //实例、父节点
        hudTextGo.transform.position = Vector3.zero;
        hudTextGo.transform.localScale = Vector3.one;
        
        hudTxt = hudTextGo.GetComponent<HUDText>();//取得HUDText组件
      
        UIFollowTarget followTarget = hudTextGo.GetComponent<UIFollowTarget>();//  //填UIFollowTarget的参数，预制体自带
        followTarget.target = hudTextTrans;
        followTarget.gameCamera = Camera.main;
        //followTarget.uiCamera = UICamera.current.GetComponent<Camera>();//报空指针错误
        followTarget.uiCamera = NGUITools.FindCameraForLayer(5);//5是UI层
    }

    public void PlayAniClip(string clip)
    {
       // PlayAniClip(damage1Clip);//Animator


        if (null != animation && null != clip)
        {
            animation.CrossFade(clip);
        }
    
    }
    #endregion

    #region 状态机
    public virtual void Update_Animator()
    {
        switch (state)
        {
            case State.Idle: PlayAniClip(idleClip); break;
            case State.Walk: PlayAniClip(walkClip); break;
            case State.Dead: PlayAniClip(deathClip); break;
        }
    }
    //     public virtual float Attack() { return atk_normal; }
    public virtual void TakeDamage(float attack)
    {
      
        float r = UnityEngine.Random.Range(0f, 1f);
        if (r < missRate)
        {
            AudioSource.PlayClipAtPoint(missAudioClip, transform.position);
            if(hp>0)
                hudTxt.Add("Miss", Color.gray, stayDuration);

          //  print("Miss");
        }
        else
        {
            float remainHp = hp - attack;
            if (remainHp > 0)
            {
                hp = remainHp;
                hudTxt.Add("-" + attack.ToString(), Color.red, stayDuration);
            }
            else
            {
                if (hp > 0)
                { 
                                  hudTxt.Add("-" + hp.ToString(), Color.red, stayDuration);

                }
                hp = 0;
                Dead();
            }
            //
            TakeDamageEffect();

        }
    }
    public virtual void TakeDamage(int attack)
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        if (r < missRate)
        {
            AudioSource.PlayClipAtPoint(missAudioClip, transform.position);
            hudTxt.Add("Miss", Color.gray, stayDuration);

            print("Miss");
        }
        else
        {
            int remainHp = (int)(hp - attack);
            if (remainHp > 0)
            {
                hp = remainHp;
                hudTxt.Add("-" + attack.ToString(), Color.red, stayDuration);
            }
            else
            {
                hp = 0;
                Dead();
            }
            //
            TakeDamageEffect();

        }
    }
    public virtual void TakeDamageEffect() { }
    public virtual void Dead() { }
    public virtual void Walk() { }
    public virtual void Run() { }
    //
   
    public virtual void Range(Transform target)//视野
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance < range_atk)
        {
            Attack();
        }
        else if (distance > range_atk && distance < range_sight)
        {
            Chase();
        }
    }
    public virtual void Range()//视野
    { }
    public virtual void Chase() { }
    public virtual void Patrol() { }
    public virtual float Attack()//攻击
    {
        return 0;
    }
    #endregion
}
