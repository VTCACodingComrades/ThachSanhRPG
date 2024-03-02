using TMPro;
using UnityEngine;

public class PlayerCoin : Singleton<PlayerCoin>
{
    [SerializeField] private float currentBalance;
    [SerializeField] private string currentName;
    [SerializeField] private int currentLevel = 1;
    //private TMP_Text coinText; //? PurseUI.cs se show coin or balance
    private TMP_Text nameText;
    private TMP_Text levelText;
    private Purse purse;


    //private const string COIN_TEXT = "coinText"; //? PurseUI.cs se show coin or balance
    private const string NAME_TEXT = "nameText";
    private const string LEVEL_TEXT = "levelText";

    public float CurrentBalance {get{return currentBalance;}}
    public float SetCurrentBalance(float coin) => purse.SetBalance(coin);

    public string CurrentName{get{return currentName;}}
    public string SetCurrentName(string name) => this.currentName = name;

    public int CurrentLevel{get{return currentLevel;}}
    public int SetCurrentLevel(int level) => this.currentLevel = level;

    protected override void Awake() {
        base.Awake();
        purse = GetComponent<Purse>();
        //coinText = GameObject.Find(COIN_TEXT).GetComponent<TMP_Text>(); //? PurseUI.cs se show coin or balance
        nameText = GameObject.Find(NAME_TEXT).GetComponent<TMP_Text>();
        levelText = GameObject.Find(LEVEL_TEXT).GetComponent<TMP_Text>();
    }

    private void Update() {
        currentBalance = purse.GetBalance(); //todo bien luu local tam thoi

        //load coin name level health
        //UpdateCurrentCoin();
        UpdateName();
        UpdaLevel();
    }
    private void UpdateName() {
        if(nameText == null) {
            nameText = GameObject.Find(NAME_TEXT).GetComponent<TMP_Text>();
        }
        nameText.text = currentName.ToString(); //phai chuyen qua kieu string
    }
    private void UpdaLevel() {
        currentLevel = UILevelSelectButton.Instance.UnlockLevelInt;
        if(levelText == null) {
            levelText = GameObject.Find(LEVEL_TEXT).GetComponent<TMP_Text>();
        }
        levelText.text = "Level: " + currentLevel.ToString(); //phai chuyen qua kieu string
    }
    public void UpdateCurrentCoin() {
        //currentCoin += 1;
        // foreach (var item in PlayerController.Instance.GetInventory().GetItemList())
        // {
        //     if(item.itemScriptableObject.itemType == Item.ItemType.Coin) {
        //         currentCoin = item.amount;
        //     }
        // }

        //?testing hien thi tien theo inventory
        // if(coinText == null) {
        //     coinText = GameObject.Find(COIN_TEXT).GetComponent<TMP_Text>(); // this.gameObject se tim gaemobject co ten cointext va getcomponent
        // }
        // coinText.text = currentBalance.ToString(); //phai chuyen qua kieu string
    }

    /* public void ShowCoinAfterLoadingCoidData() {
        coinText = GameObject.Find(COIN_TEXT).GetComponent<TMP_Text>();
        coinText.text = CurrentBalance.ToString("D3"); //phai chuyen qua kieu string
    } */
}
