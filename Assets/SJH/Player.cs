using System;
using System.Collections;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[SerializeField] public Vector2 currentDirection = Vector2.down; // ó�� ������ �Ʒ�
	[SerializeField] Vector3 currentPos;
	[SerializeField] Scene currenScene;
	public Coroutine moveCoroutine;
	WaitForSeconds moveDelay;

	[Tooltip("�̵� �Ÿ� (�⺻ 2)")]
	[SerializeField] int moveValue = 2;
	[Tooltip("�̵� �ð� (�⺻ 0.3)")]
	[SerializeField] float moveDuration = 0.3f;
	[SerializeField] bool isMoving = false;
	bool isIdle = false;
	[SerializeField] public bool isSceneChange;

	Animator anim;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (isSceneChange)
			return;
		// ����Ű ���� Idle �����ϰ� �̵��� ������ isIdle�� ���� �ٲٱ�
		if (Input.GetKeyUp(KeyCode.UpArrow) ||
			Input.GetKeyUp(KeyCode.DownArrow) ||
			Input.GetKeyUp(KeyCode.LeftArrow) ||
			Input.GetKeyUp(KeyCode.RightArrow))
		{
			isIdle = true;
			//moveCoroutine = null;
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
				AnimChange(inputDir);

				// ����ĳ��Ʈ
				Debug.DrawRay((Vector2)transform.position + (inputDir * 1.1f), inputDir, Color.green);
				RaycastHit2D rayHit = Physics2D.Raycast((Vector2)transform.position + (inputDir * 1.1f), inputDir, 1f);
				if (rayHit)
				{
					switch (rayHit.transform.gameObject.tag)
					{
						case "Wall":
						case "NPC":
							Debug.Log($"�÷��̾� �� : {rayHit.transform.gameObject.name}");
							StopMoving();
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

	public void AnimChange()
	{
		anim.SetFloat("x", currentDirection.x);
		anim.SetFloat("y", currentDirection.y);
	}
	public void AnimChange(Vector2 direction)
	{
		anim.SetFloat("x", direction.x);
		anim.SetFloat("y", direction.y);
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
		while (time < moveDuration && isMoving)
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

	public void StopMoving()
	{
		isMoving = false;
		anim.SetBool("isMoving", false);
		moveCoroutine = null;
	}

	void OnDrawGizmos()
	{
		// �÷��̾� �̵����� �����
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine((Vector2)transform.position + Vector2.up * 1.1f, (Vector2)transform.position + Vector2.up * 1.1f + Vector2.up);
		Gizmos.DrawLine((Vector2)transform.position + Vector2.down * 1.1f, (Vector2)transform.position + Vector2.down * 1.1f + Vector2.down);
		Gizmos.DrawLine((Vector2)transform.position + Vector2.right * 1.1f, (Vector2)transform.position + Vector2.right * 1.1f + Vector2.right);
		Gizmos.DrawLine((Vector2)transform.position + Vector2.left * 1.1f, (Vector2)transform.position + Vector2.left * 1.1f + Vector2.left);
		
	}
}
