using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] int experiencePoints = 0;

    public void GetExperience(int experience)
    {
        experiencePoints += experience;
        if (GameManager.Instance.LevelUpPoint1 < experiencePoints)
        {
            Debug.Log("������ ������ ����");
        }
    }

    public int GetExp()
    {
        return experiencePoints;
    }
}
