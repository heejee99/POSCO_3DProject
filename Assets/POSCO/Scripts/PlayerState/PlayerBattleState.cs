using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBattleState : PlayerStateBase
{
    public PlayerBattleState(Player player) : base(player) 
    {
        TurnManager.Instance.monsterTurnChange += OnMonsterTurnChange;
    }

    public override void Enter()
    {
        //전투에 들어가면
        //1. 필드에 있는 플레이어 움직임 정지
        //2. 카메라 배틀맵으로 교체
        //3. 만난 몬스터의 정보와 플레이어의 몬스터 정보를 GameManager에게 넘겨줌
        //4. 선택하는 UI생성
        player.canMove = false;
        CameraManager.Instance.HandleCamera(CameraType.BattleMap);
        gameManager.SetMonsterOnBattlePosition();

        //플레이어 턴일때 띄워주는 팝업
        //ShowPlayerTurnPopup();
    }

    private void OnMonsterTurnChange(Monster currentMonster)
    {
        if (!currentMonster.isEnemy)
        {
            ShowPlayerTurnPopup(currentMonster);
        }
    }

    private void ShowPlayerTurnPopup(Monster currentMonster)
    {
        var buttons = new Dictionary<string, UnityAction>
        {
            {
                "DoAttack", () =>
                {
                    DoAttack();
                }
            },
            {
                "DoHeal", () =>
                {
                    DoHeal();
                }
            }
        };

        UIPopupManager.Instance.ShowPopup(
            $"{currentMonster.name}'s Turn. What do you do?",
            buttons
            );
    }

    //공격하기를 누르면 누굴 공격할지를 선택할 수 있어야한다.
    private void DoAttack()
    {
        //GameManager.Instance.ExecutePlayerAttackAction(GameManager.Instance.currentTurnMonster);
        ChooseTarget();
    }

    //공격 대상 선택
    private void ChooseTarget()
    {

        var buttons = new Dictionary<string, UnityAction>
        {
            {
                "First", () =>
                {
                    DoAttackTarget(0);
                }
            },
            {
                "Second", () =>
                {
                    DoAttackTarget(1);
                }
            },
            {
                "Third", () =>
                {
                    DoAttackTarget(2);
                }
            }
        };

        UIPopupManager.Instance.ShowPopup(
            $"ChooseTarget!",
            buttons
            );
    }

    private void DoAttackTarget(int targetnum)
    {
        //적이 다 죽으면 고를 수 없어야함
        if (targetnum < TurnManager.Instance.enemyMonsterList.Count)
        {
            Monster target = TurnManager.Instance.enemyMonsterList[targetnum];
            if (target.hp > 0)
            {
                GameManager.Instance.ExecutePlayerAttackAction(target);
            }
            else
            {
                Debug.Log("이미 쓰러진 몬스터입니다. 다른 몬스터를 선택해주세요");
            }
        }
    }

    private void DoHeal()
    {

    }

    //update는 아직까진 필요없다.
    public override void Update()
    {
        
    }

    public override void Exit()
    {
        player.canMove = true;
        //uiPopup.chooseBattleStateCanvas.SetActive(false);
        //uiPopup.chooseTargetCanvas.SetActive(false);
        CameraManager.Instance.HandleCamera(CameraType.FieldMap);
    }

    //여기서는 필요없다.
    public override void HandleCollision(Collision collision)
    {
        
    }
}
