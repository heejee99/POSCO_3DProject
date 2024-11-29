using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerContactEnemyState : PlayerStateBase
{
    //���� ����
    private Monster enemyMonster;

    public PlayerContactEnemyState(Player player, Monster enemy) : base(player)
    {
        this.enemyMonster = enemy;
    }

    public override void Enter()
    {
        //���� ������ ������ ���� �� ������Ѵ�.
        player.canMove = false;
        //���� ������ ���� �����ٴ� UI�� �������
        //uiPopup.EnemyContactCanvasOpen();
        //���� ���� GameManager���� �÷��̾��� ������ ���� ������ ����ȭ

        var buttons = new Dictionary<string, UnityAction>
        {
            { "Fight", () => StartBattle() },
            { "RunAway", () => RunAway() }
        };

        UIPopupManager.Instance.ShowPopup(
            $"{enemyMonster.name} is appear!, what do you do?",
            buttons
            );
    }

    private void StartBattle()
    {
        gameManager.SetMonsterInformation(player.selectedMonsterList, enemyMonster);

        player.ChangeState(new PlayerBattleState(player));
    }

    private void RunAway()
    {
        player.ChangeState(new PlayerIdleState(player));
    }

    //Update���� ���� ���°� ����
    public override void Update()
    {
        
    }

    public override void Exit()
    {
        //���� ������� ������ �� �ִ�.
        player.canMove = true;
        //uiPopup.enemyContactCanvas.SetActive(false);
        UIPopupManager.Instance.ClosePopup();
    }

    //�̰͵� ���� ���°� ����.
    public override void HandleCollision(Collision collision)
    {
        
    }

}