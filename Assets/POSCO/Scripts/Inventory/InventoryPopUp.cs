using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopUp : MonoBehaviour
{

    private static InventoryPopUp instance;
    public static InventoryPopUp Instance { get { return instance; } }

    public GameObject ivnetoryMenuBackgoundPrefab;
    public GameObject showMonsterBackgoundPrefab;
    public GameObject showSelectedMonsterBackgoundPrefab;
    public GameObject showItemBackgoundPrefab;
    public GameObject canvasTransform;
    public GameObject monsterCardPrefab;
    public GameObject inventoryPrefab;
    


    public RectTransform inventoryPos;
    

    private bool isOpenInventory = false;

    private UIInventory uiInventory;
    private InventoryButton inventoryButton;

    private int choiceNum = 0;
    private UIPopupManager uiPopupManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        uiPopupManager = new UIPopupManager();
        inventoryButton = FindObjectOfType<InventoryButton>();
        uiInventory = FindObjectOfType<UIInventory>();
        
    }

    private void Update()
    {
        //open

        if (Input.GetKeyUp(KeyCode.I))
        {
                     //isOpenInventory = true;
            //UIInventoryManager.Instance.OpenPopup(this.gameObject); 
            InstantiateInventoryMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopup();
        }

    }

    public void InstantiateInventoryMenu()
    {

        if (UIInventoryManager.Instance.IsPopupOpen() >= 1) // 팝업이 이미 열려 있는지 확인
        {
            print("이미열림");
            return; // 팝업이 열려 있으면 다시 열지 않음
        }

        inventoryPrefab = Instantiate(ivnetoryMenuBackgoundPrefab, inventoryPos);
        Transform monsterButton = inventoryPrefab.transform.Find("ShowMonsterButton");
        Transform itemButton = inventoryPrefab.transform.Find("ShowItemButton");
        Button onMonsterButton = monsterButton.GetComponentInChildren<Button>();
        Button onItemItemButton = itemButton.GetComponentInChildren<Button>();

        onMonsterButton.onClick.AddListener(() => InstantiateShowMonster());
        onItemItemButton.onClick.AddListener(() => InstantiateShowItem());
        UIInventoryManager.Instance.OpenPopup(inventoryPrefab);
    }

    public void InstantiateShowMonster()
    {
        if (UIInventoryManager.Instance.IsPopupOpen()>=2) // 팝업이 이미 열려 있는지 확인
        {
            print("이미열림");
            return; // 팝업이 열려 있으면 다시 열지 않음
        }



        GameObject monsterCardBackgroundPrefab = Instantiate(showMonsterBackgoundPrefab, inventoryPos);
        
       
        uiInventory.InstantiateMonsterCard(monsterCardBackgroundPrefab);
        uiInventory.SetSelectButton(monsterCardBackgroundPrefab);

        UIInventoryManager.Instance.OpenPopup(monsterCardBackgroundPrefab);
    }

    
    public void InstantiateSelectedMonster()
    {
        if (UIInventoryManager.Instance.IsPopupOpen()>=3) // 팝업이 이미 열려 있는지 확인
        {
            print("이미열림");
            return; // 팝업이 열려 있으면 다시 열지 않음
        }

        uiInventory.TempSelectedMonsterList.Clear();
        GameObject selectedMonsterPrefab = Instantiate(showSelectedMonsterBackgoundPrefab, inventoryPos);
        uiInventory.selectedMonster = selectedMonsterPrefab;
        Transform selectButton = selectedMonsterPrefab.transform.Find("SelectButton");
        Transform resetButton = selectedMonsterPrefab.transform.Find("Reset");
        Button onSelectButton = selectButton.GetComponentInChildren<Button>();
        Button onresetButton = resetButton.GetComponentInChildren<Button>();
        onSelectButton.onClick.AddListener(() => inventoryButton.OnSelectBoutton());
        onresetButton.onClick.AddListener(() => inventoryButton.OnRestButton());
        UIInventoryManager.Instance.OpenPopup(selectedMonsterPrefab);
    }



    public void InstantiateShowItem()
    {
        if (UIInventoryManager.Instance.IsPopupOpen()>=1) // 팝업이 이미 열려 있는지 확인
        {
            print("이미열림"); 
            return; // 팝업이 열려 있으면 다시 열지 않음
        }


        GameObject itemPrefab = Instantiate(showItemBackgoundPrefab, inventoryPos);
        UIInventoryManager.Instance.OpenPopup(itemPrefab);


    }
    public void ClosePopup()
    {
     
        isOpenInventory = false;
        UIInventoryManager.Instance.ClosePopup(); // PopupManager를 사용하여 팝업 닫기
    }
}




