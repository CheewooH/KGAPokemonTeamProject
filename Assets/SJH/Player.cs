using System;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
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
	[SerializeField] bool isMoving = false;
	bool isIdle = false;

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
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

				// ����ĳ��Ʈ
				RaycastHit2D rayHit = Physics2D.Raycast((Vector2)transform.position + (inputDir * 1.1f), inputDir, 1f);
				if (rayHit)
				{
					if (rayHit.transform.gameObject.tag == "Wall")
					{
						isMoving = false;
						anim.SetBool("isMoving", false);
						moveCoroutine = null;
						return;
					}
				}

				// ������ ������ �̵� ����
				if (inputDir == currentDirection)
				{
					moveCoroutine = StartCoroutine(Move(inputDir));
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
}
