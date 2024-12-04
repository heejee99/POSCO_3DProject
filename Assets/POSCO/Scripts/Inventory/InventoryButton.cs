using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{

    //��ư ����
   private UIInventory uiInventory;
   private InventoryPopUp inventoryPopUp;


    private void Awake()
    {
        
    }
    private void Start()
    {
        uiInventory = FindObjectOfType<UIInventory>(); // �� ���� UIInventory�� ã��
        inventoryPopUp = FindObjectOfType<InventoryPopUp>(); // �� ���� InventoryPopUp�� ã��
    }

    public void OnShowMonster()
    {
        if (UIInventoryManager.Instance.IsPopupOpen())
        {
            print("Monster 팝업이 이미 열려 있습니다.");
            // 이미 열린 팝업이 있다면 열리지 않음
            return;
        }
        inventoryPopUp.InstantiateShowMonster();
    }

    public void OnShowSelectedMonster()
    {
        if (UIInventoryManager.Instance.IsPopupOpen())
        {
            // 이미 열린 팝업이 있다면 열리지 않음
            return;
        }
        inventoryPopUp.InstantiateSelectedMonster();
    }

    public void OnShowItem()
    {
        if (UIInventoryManager.Instance.IsPopupOpen())
        {
            // 이미 열린 팝업이 있다면 열리지 않음
            return;
        }
        inventoryPopUp.InstantiateShowItem();
    }


    public void OnChioseBattleMonsterButton()
    {
        if (UIInventoryManager.Instance.IsPopupOpen())
        {
            // 이미 열린 팝업이 있다면 열리지 않음
            return;
        }
        uiInventory.OnCardListInteractable();
        inventoryPopUp.InstantiateSelectedMonster();
    }

    public void OnSelectCardButton(int num)
    {
  
        uiInventory.OnMonsterCard(num);
    }


    //private void ShowSetCelectMonster(int num)
    //{
    //    targetGameObject = ShowColectedMonster[num];
    //    rawImage = targetGameObject.GetComponent<RawImage>();
    //    rawImage.texture = targetRawImage.texture;
    //}

  

}
