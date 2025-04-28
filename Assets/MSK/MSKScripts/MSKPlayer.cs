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

	public event Action OnEncountered;


    void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
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
    void Interact()
    {
        var facingDit = new Vector3(anim.GetFloat("x"), anim.GetFloat("y"));
        var interactPos = transform.position + facingDit;
		Debug.Log(interactPos);
		
		var collider = Physics2D.OverlapCircle(interactPos, 0.3f, LayerMask.GetMask("ObjectLayer") | LayerMask.GetMask("InteractableLayer"));
		if (collider != null)
		{
			collider.GetComponent<IInteractable>()?.Interact();
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
