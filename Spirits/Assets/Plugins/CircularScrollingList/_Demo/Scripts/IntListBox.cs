using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
    public class IntListBox : ListBox
    {
        [SerializeField]
        private RawImage _contentText;

        protected override void UpdateDisplayContent(object content)
        {
            _contentText.texture = ((Texture) content);
        }
    }
}
