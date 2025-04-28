using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
	public static Dictionary<string, string> sceneDic = new Dictionary<string, string>()
	{
		["���ΰ���2��"] = "PlayerHouse2F",
		["���ΰ���1��"] = "PlayerHouse1F",
		["���θ���"] = "NewBarkTown",
		["������"] = "ProfessorLab",
		["29������"] = "Route29",

	};

	public enum PortalType
	{
		Stair,      // Ʈ���� ������� ������
		Foothold    // Stay �϶� Ư�� ����Ű �Է��ϸ� ������
	};

	public enum PlayerState
	{
		Field,			// �ʵ�
		SceneChange,    // �� �̵���
		Battle,			// ��Ʋ��
		UI,				// UI Ȱ��ȭ��
		Menu,			// Menu Ȱ��ȭ��
	};
}
