using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : MonoBehaviour
{
    public GameObject creditMenu;
    // Start is called before the first frame update
    public void ShowCredit()
    {
        creditMenu.SetActive(true);
    }
    public void HideCredit()
    {
        creditMenu.SetActive(false);
    }
}
