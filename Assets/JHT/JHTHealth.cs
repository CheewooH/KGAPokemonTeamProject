using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class JHTHealth : MonoBehaviour
{
    [SerializeField] int healthPoints = 50;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }
    void Start()
    {
        healthPoints = GetComponent<JHTPokemonStat>().GetPokeStat(JHTStat.ü��);
    }

    public void TakeDamage(int damage) //GameObject instigator, -> �ش� ���� �׾������
    {
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        if (healthPoints <= 0)
        {
            Die();
            //GetRewardExp(instigator); ->����ġ ȹ�� ���߿� ��Ʋ���� ������ �� ����
        }
    }

    public void GetRewardExp(GameObject instigator)
    {
        JHTExperience experience = instigator.GetComponent<JHTExperience>();
        if (experience == null) return;

        experience.GetExperience(GetComponent<JHTPokemonStat>().GetPokeStat(JHTStat.����ġ_ȹ�淮));
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
    }

}
