using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSKPCcontroller : MonoBehaviour
{
    Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        { 
        }
    }
    void PCRaycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 2f))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
        }
        else
        {
            Debug.Log("���� ����");
        }

    }

    //	�÷��̾� ���� ���⿡ ���̾ Ȯ��
    //	���ͷ�Ʈ, ������Ʈ ���̾ ������ �� �̵� �Ұ���
    bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(transform.position, LayerMask.GetMask("ObjectLayer") | LayerMask.GetMask("InteractableLayer")) != null)
        {
            return false;
        }
        return true;
    }
     

}
