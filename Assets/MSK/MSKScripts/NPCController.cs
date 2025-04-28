using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] Dialog dialog;
    public void Interact() 
    {   // ��ȣ�ۿ� �� �÷��̾ �ٶ󺸴� ������ �߰�
        Debug.Log("NPC Interact success");
        DialogManager.Instance.ShowText(dialog);
    }

}
