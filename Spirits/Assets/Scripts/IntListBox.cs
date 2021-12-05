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
    }

    protected override void UpdateDisplayContent(object content)
    {
        reset();

        vals = (int[])(content);
        int[] present = {0, 0, 0, 0, 0, 0};
        int cnt = 0;
        for (int i = 0; i < 6; i++){
            if (vals[i] != 0)
            {
                cnt++;
                present[i] = 1;
            }
        }

        int[][] temp = new int[cnt][];
        for (int i = 0; i < cnt; i++)
            temp[i] = new int[2];
        int pnt = 0;
        for (int i = 0; i < 6; i++){
            if (vals[i] != 0){
                temp[pnt][0] = i;
                temp[pnt][1] = vals[i];
                pnt++;
            }
        }
        int tempCnt = 0;
        for (int i = 0; i < 6; i++)
        {
            if(vals[i] != 0)
            {
                if(int.Parse(playerInv[i].text) >= vals[i])
                    panels[cnt - 1].transform.GetChild(tempCnt).GetComponent<Image>().overrideSprite = spritesReady[temp[tempCnt][0]];
                else
                    panels[cnt - 1].transform.GetChild(tempCnt).GetComponent<Image>().overrideSprite = spritesNeeded[temp[tempCnt][0]];
            }

            if(present[i] == 1)
                panels[cnt - 1].transform.GetChild(tempCnt).GetChild(0).GetComponent<Text>().text = vals[i].ToString();

            if(tempCnt < cnt - 1)
                tempCnt++;
        }
        panels[cnt - 1].SetActive(true);
    }

    void reset(){
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(false);
    }
}

