using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AirFishLab.ScrollingList.Demo
{
    public class IntListBank : BaseListBank
    {
        // private readonly int[] _listContents = {
        //     -1, -1, -1, -1, -1, -1, -1, -1, -1
        // };

        public List<int[]> _listContents = new List<int[]>();

        public override object GetListContent(int index)
        {
            return _listContents[index];
        }

        public override int GetListLength()
        {
            return _listContents.Count;
        }
    }
}
