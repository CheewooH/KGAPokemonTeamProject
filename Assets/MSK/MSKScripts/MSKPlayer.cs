using System;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSKPlayer : MonoBehaviour
{
	[SerializeField] Vector2 currentDirection = Vector2.down; // ó�� ������ �Ʒ�
	[SerializeField] Vector3 currentPos;
	[SerializeField] Scene currenScene;
	Coroutine moveCoroutine;
	WaitForSeconds moveDelay;

	[Tooltip("�̵� �Ÿ� (�⺻ 2)")]
	[SerializeField] int moveValue = 2;
	[Tooltip("�̵� �ð� (�⺻ 0.3)")]
	[SerializeField] float moveDuration = 0.3f;
	bool isMoving = false;
	bool isIdle = false;

	Animator anim;

	//	�̵�Ȯ�ο� ���̾�
	public LayerMask interactableLayer;
	public LayerMask objectsLayer;
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
    void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		PCRaycast();
		// ����Ű ���� Idle �����ϰ� �̵��� ������ isIdle�� ���� �ٲٱ�
		if (Input.GetKeyUp(KeyCode.UpArrow) ||
			Input.GetKeyUp(KeyCode.DownArrow) ||
			Input.GetKeyUp(KeyCode.LeftArrow) ||
			Input.GetKeyUp(KeyCode.RightArrow))
		{
			isIdle = true;
			moveCoroutine = null;
		}

		if (!isMoving)
		{
			Vector2 inputDir = Vector2.zero;

			if (Input.GetKey(KeyCode.UpArrow)) inputDir = Vector2.up;
			else if (Input.GetKey(KeyCode.DownArrow)) inputDir = Vector2.down;
			else if (Input.GetKey(KeyCode.LeftArrow)) inputDir = Vector2.left;
			else if (Input.GetKey(KeyCode.RightArrow)) inputDir = Vector2.right;

			if (inputDir != Vector2.zero)
			{
				// ���� ����
				anim.SetFloat("x", inputDir.x);
				anim.SetFloat("y", inputDir.y);

				// ������ ������ �̵� ����
				if (inputDir == currentDirection)
				{
					if (IsWalkable(inputDir))
					{
                        moveCoroutine = StartCoroutine(Move(inputDir));
                    
					}
                }
				// ���⸸ �ٲٰ� ���
				else
				{
					currentDirection = inputDir;
				}
			}
		}
	}

	IEnumerator Move(Vector2 direction)
	{
		// 1 �̵� = x or y 2 ��ȭ
		// �ٷ� 2�� �̵������ʰ� �̵��ð��� ���ļ� �̵�
		isMoving = true;
		isIdle = false;
		anim.SetBool("isMoving", isMoving);

		Vector2 startPos = transform.position;
		Vector2 endPos = startPos + (direction * moveValue);

		float time = 0;
		while (time < moveDuration)
		{
			time += Time.deltaTime;
			float percent = time / moveDuration;
			transform.position = Vector2.Lerp(startPos, endPos, percent);
			yield return null;
		}
		transform.position = endPos;

		isMoving = false;
		if (isIdle)
		{
			anim.SetBool("isMoving", false);
			isIdle = false;
		}
	}


	//	�÷��̾� ���� ���⿡ ���̾ Ȯ��
	//	���ͷ�Ʈ, ������Ʈ ���̾ ������ �� �̵� �Ұ���
	private bool IsWalkable(Vector3 targetPos) 
	{
		if (Physics2D.OverlapCircle(targetPos, 1f, objectsLayer | interactableLayer) != null) 
		{ 
			return false;
		}
		return true;
	}
}
