using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObject : MonoBehaviour, IInteractable
{
	[SerializeField] string text;

	public void Interact()
	{
		// TODO : �ʵ� ������Ʈ ��ȣ�ۿ� UI Ȱ��ȭ
		Debug.Log($"{gameObject.name} : {text}");
	}
}
