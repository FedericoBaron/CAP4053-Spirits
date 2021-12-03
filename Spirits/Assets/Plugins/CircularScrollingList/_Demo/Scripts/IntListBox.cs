using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
    public class IntListBox : ListBox
    {
        [SerializeField]
        public GameObject[] panels;
        public Sprite[] spritesNeeded; //these are the dull sprites
        public Sprite[] spritesReady; //these are the vivid ones

        public GameObject inventory;
        public Text[] playerInv;

        void Start()
        {
            inventory = GameObject.Find("GhostCount");
        }

        protected override void UpdateDisplayContent(object content)
        {
            reset();
            playerInv = inventory.GetComponentsInChildren<Text>();

            int[] vals = (int[])(content);
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
}
