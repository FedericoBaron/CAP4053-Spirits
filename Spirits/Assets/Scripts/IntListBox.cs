using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.UI;

public class IntListBox : ListBox
{
    [SerializeField]
    public GameObject[] panels;
    public Sprite[] spritesNeeded; //these are the dull sprites
    public Sprite[] spritesReady; //these are the vivid ones
    public GameObject inventory;
    public Text[] playerInv;
    public Player_Combat player;
    public Control_List ControlList;
    public int[] vals = null;

    void Start()
    {
        inventory = GameObject.Find("GhostCount");
        GameObject p = GameObject.Find("Bartender");
        player = p.GetComponent<Player_Combat>();
        ControlList = p.GetComponent<Control_List>();
        playerInv = inventory.GetComponentsInChildren<Text>();
    }

    public void submitRecipe(){
        if (vals == null) return;
        if (vals[0] < 0) return;
        int[] inv = player.capturedGhosts;
        bool flag = true;
        for (int i = 0; i < vals.Length; i++){
            if (inv[i] < vals[i])
                flag = false;
        }    
        if (!flag) return;
        for (int i = 0; i < vals.Length; i++){
            inv[i] -= vals[i];
            playerInv[i].text = inv[i].ToString();
        }
        player.totalMoney += player.submitRecipe;
        ControlList.remove(vals);
        vals = null;
        reset();
    }

    protected override void UpdateDisplayContent(object content)
    {
        reset();

        vals = (int[])(content);

        if (vals[0] < 0){
            return;
        }

        int cnt = 0;
        for (int i = 0; i < 6; i++){
            if (vals[i] != 0)
            {
                cnt++;
            }
        }

        int tempCnt = 0;

        for (int i = 0; i < 6; i++)
        {
            if(vals[i] != 0)
            {
                if(player.capturedGhosts[i] >= vals[i])
                    panels[cnt - 1].transform.GetChild(tempCnt).GetComponent<Image>().overrideSprite = spritesReady[i];
                else
                    panels[cnt - 1].transform.GetChild(tempCnt).GetComponent<Image>().overrideSprite = spritesNeeded[i];
                panels[cnt - 1].transform.GetChild(tempCnt).GetChild(0).GetComponent<Text>().text = vals[i].ToString();
                tempCnt++;
            }
        }
        panels[cnt - 1].SetActive(true);
    }

    void reset(){
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(false);
    }
}