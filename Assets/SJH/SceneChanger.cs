using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	[SerializeField] Vector2 targetPos;
	[SerializeField] Define.PortalType portalType;
	[SerializeField] bool isPlayerIn;
	[SerializeField] Vector2 keyDirection;
	Coroutine sceneCoroutine;
	[SerializeField] bool isChange;
	void Awake()
	{

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (portalType == Define.PortalType.Stair && !isChange)
			{
				// �÷��̾� �̵�
				//SceneChange(gameObject.name, collision.transform.gameObject);
				sceneCoroutine = StartCoroutine(Change(gameObject.name, collision.gameObject));
			}
		}
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if (portalType == Define.PortalType.Foothold)
		{
			isPlayerIn = true;
		}
		if (collision.CompareTag("Player"))
		{
			if (portalType == Define.PortalType.Foothold && !isChange)
			{
				Player player = collision.gameObject.GetComponent<Player>();

				// ����Ű �Է� ���� üũ
				Vector2 inputDir = Vector2.zero;
				if (Input.GetKey(KeyCode.UpArrow)) inputDir = Vector2.up;
				else if (Input.GetKey(KeyCode.DownArrow)) inputDir = Vector2.down;
				else if (Input.GetKey(KeyCode.LeftArrow)) inputDir = Vector2.left;
				else if (Input.GetKey(KeyCode.RightArrow)) inputDir = Vector2.right;

				if (inputDir == keyDirection)
				{
					// �÷��̾� �̵�
					//SceneChange(gameObject.name, player.gameObject);
					sceneCoroutine = StartCoroutine(Change(gameObject.name, player.gameObject));
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		isPlayerIn = false;
	}

	IEnumerator Change(string sceneName, GameObject player)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		asyncLoad.allowSceneActivation = false;
		// �ܷ��̵� �����
		Player pc = player.GetComponent<Player>();
		isChange = true;
		pc.isSceneChange = true;
		player.transform.position = new Vector3(targetPos.x, targetPos.y);
		pc.StopMoving();
		pc.currentDirection = keyDirection;
		pc.AnimChange();

		while (!asyncLoad.isDone)
		{
			if (asyncLoad.progress >= 0.9f)
			{
				Debug.Log(player);
				Debug.Log(gameObject.name);
				player.transform.position = targetPos;
				yield return new WaitForSeconds(0.1f);
				asyncLoad.allowSceneActivation = true;

				// ���� �ʱ�ȭ
				isChange = false;
				pc.isSceneChange = false;
				sceneCoroutine = null;
				Debug.Log("state init");

				break;  // ���� Ż��
			}
			yield return null;
		}
	}
}
