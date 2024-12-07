using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    //플레이어의 모든 몬스터 데이터를 가져올 리스트
    public List<Monster> allMonsterDataList = new List<Monster>();
    //선택된 몬스터 데이터 리스트
    public List<Monster> selectedMonsterDataList = new List<Monster>();

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        //플레이어 연동
        player = FindAnyObjectByType<Player>();
        player.onClickSelectButton += InitializeSelectedPlayerMonsterData;
        BringPlayerAllMonsterData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            BringPlayerAllMonsterData();
        }
            //InitializeSelectedPlayerMonsterData();
        
    }

    //현재 플레이어가 들고있는 몬스터 프리팹의 정보를 다 불러온다
    private void BringPlayerAllMonsterData()
    {
        foreach(GameObject monsterObj in player.playerMonsterPrefabList)
        {
            if (monsterObj.TryGetComponent<Monster>(out Monster monster))
            {
                allMonsterDataList.Add(monster);
            }
            print($"{allMonsterDataList[0]}");
        }
    }

    //select하는 버튼을 누를때 이벤트로 추가해주면 좋을 듯 -> 이걸 allMonsterDataList에서 가져와야함
    public void InitializeSelectedPlayerMonsterData()
    {
        //기존 선택된건 일단 지우고 -> 근데 이건 버튼을 누르는 방식에서 좋은듯.
        selectedMonsterDataList.Clear();

        //플레이어의 몬스터 갯수가 3개가 되면
        if (player.selectedMonsterList.Count == 3)
        {
            //모든 몬스터 데이터의 리스트에서 
            foreach(Monster selectedMonster in player.selectedMonsterList)
            {
                Monster matchedMonster = allMonsterDataList.Find(monster => monster.name == selectedMonster.name);
                if (matchedMonster != null)
                {
                    selectedMonsterDataList.Add(matchedMonster);
                }
            }
        }

    }

    //selectButton 누름 -> 플레이어에 담기는 선택한 몬스터 리스트를 그대로 받아옴 (데이터만)
    //-> 전투떄는 이 받아온 리스트로 전투를 실행 -> 그러면 플레이어 선택한 몬스터 체력도 닳아있는 상태이고, 여기 선택된 몬스터체력도
    //-> 닳아있는 상태이다. -> 전투가 끝나면 이 세마리의 정보를 5개 있는 곳으로 넘겨준다. 아 그러면 5개에서 뽑아오는 식으로 해야겠네
    //-> 넘겨줄때 선택된 몬스터 리스트는 그대로 가져가고, 만약 체력이 0보다 작으면 리스트에서 빼주는 식으로?
    //-> 대신 초기화 버튼 누를때 리스트는 초기화 해야할듯
    //-> 아님 플레이어의 선택된 몬스터의 숫자가 3마리면 그때 초기화를 시작해도 될듯 ㅇㅇ 이게 좋은듯? 그래서 3마리 이하면
    //-> 아예 정보가 안넘어오는거지 그리고 전투는 플레이어의 몬스터 갯수가 3마리 이하면 안된다는 경고 뜨고 그럼 어짜피 전투도 안들어가서
    //상관 없을듯
}
