
using TMPro;
using UnityEngine;

public class PurseUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI banlance; // ui_coin
    Purse shopperPurse;

    private void Start()
    {
        shopperPurse = GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();
        shopperPurse.onChange += RefeshUI;
        RefeshUI();

    }
    private void Update() {
        RefeshUI(); // dung de in data balance khi va scene

    }

    public void RefeshUI()
    {
        //banlance.text = shopperPurse.GetBalance().ToString();
        banlance.text = $"${shopperPurse.GetBalance()}";
    }
}
