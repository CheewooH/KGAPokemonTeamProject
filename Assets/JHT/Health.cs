using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Health : MonoBehaviour
{
    [SerializeField] int healthPoints = 50;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }
    void Start()
    {
        healthPoints = GetComponent<PokemonStat>().GetPokeStat(Stat.ü��);
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
        Experience experience = instigator.GetComponent<Experience>();
        if (experience == null) return;

        experience.GetExperience(GetComponent<PokemonStat>().GetPokeStat(Stat.����ġ_ȹ�淮));
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
    }

}
