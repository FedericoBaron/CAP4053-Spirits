using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{

    public class IntListBox : ListBox
    {
        [SerializeField]
        public GameObject[] panels;
        public Sprite[] sprites;

        protected override void UpdateDisplayContent(object content)
        {
            reset();
            int[] vals = (int[])(content);
            int cnt = 0;
            for (int i = 0; i < 6; i++){
                if (vals[i] != 0)
                    cnt++;
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
            for (int i = 0; i < cnt; i++){
                panels[cnt - 1].transform.GetChild(i).GetComponent<Image>().overrideSprite = sprites[temp[i][0]];
            }
            panels[cnt - 1].SetActive(true);
        }

        void reset(){
            for (int i = 0; i < panels.Length; i++)
                panels[i].SetActive(false);
        }
    }
}
