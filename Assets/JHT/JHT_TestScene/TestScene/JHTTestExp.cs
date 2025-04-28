using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JHTTestExp : MonoBehaviour
{
    public int curExp;
    private int startExp = 0;
    public GameObject[] levelPrefabs;
    private GameObject currentPrefab;


    JHTTestPokeClass poke;
    private void Awake()
    {
         poke = GetComponent<JHTTestPokeClass>();
    }


    private void Start()
    {
        //SpawnLevelPrefab(poke.level);
        curExp = startExp;
    }

    public void GetExp(int amount)
    {
        curExp += amount;
        Debug.Log($"���� ����ġ�� : {curExp} �Դϴ�");
        if (curExp > JHTGameManager.Instance.LevelUpPoint1)
        {
            curExp = 0;
            poke.level++;
            poke.damage *= 1.5f;

            if (poke.level > levelPrefabs.Length)
            {
                poke.level = levelPrefabs.Length;
            }

        }
    }


    //public void SpawnLevelPrefab(int level)
    //{
    //    if (level - 1 < 0 || level - 1 >= levelPrefabs.Length)
    //    {
    //        Debug.LogWarning("���� ������ �ε����� ������ ������ϴ�.");
    //        return;
    //    }
    //
    //    if (currentPrefab != null)
    //    {
    //        Destroy(currentPrefab);
    //    }
    //
    //    currentPrefab = Instantiate(levelPrefabs[level-1], transform.position, Quaternion.identity);
    //}
}
