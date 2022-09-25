using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Sphere_One : MonoBehaviour //范围伤害，一次
{


    [Tooltip("将被范围伤害的敌人")]    public List<Transform> takingDamageEnemyList;
    [Tooltip("角色传过来的攻击力")] public float attack;
    // Start is called before the first frame update
    void Start()
    {
        takingDamageEnemyList = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.Enemy) && takingDamageEnemyList.Contains(other.transform)==false)
        { 
             takingDamageEnemyList.Add(other.transform);       
        }
        RangeSkillAttack(takingDamageEnemyList, attack); //开打
    }
    void RangeSkillAttack(List<Transform> takingDamageEnemyList, float attack)
    {
        for (int i = 0; i < takingDamageEnemyList.Count; i++)
        {
            if (takingDamageEnemyList[i] != null )
            {
                Transform enemy = takingDamageEnemyList[i];
                enemy.GetComponent<Wolf>().TakeDamage(attack);
                takingDamageEnemyList.Remove(enemy);
                i--;
            }
        }
    }
}
