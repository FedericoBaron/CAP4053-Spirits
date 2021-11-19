using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AirFishLab.ScrollingList;
using AirFishLab.ScrollingList.Demo;


public class Control_List : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject List;
    public CircularScrollingList scrollingList;
    public IntListBank bankList;
    public List<Texture> _listContents;
    public List<Texture> images;
    void Start()
    {
        scrollingList = List.GetComponent<CircularScrollingList>();
        bankList = List.GetComponent<IntListBank>();    
        _listContents = bankList._listContents;
        Debug.Log(bankList.GetListLength());
        Debug.Log(images.Count);
        populate();
        for (int i = 0; i < bankList.GetListLength(); i++)
            Debug.Log(bankList.GetListContent(i));
    }

    void populate(){
        add(0);
        add(0);
        add(0);
        add(0);
        add(0);
        add(0);
        add(0);
    }

    void Update(){
        scrollingList.Refresh();

    }

    public bool remove(int val){
        bool v = _listContents.Remove(images[val]);
        scrollingList.Refresh();
        return v;
    }

    public void add(int val){
        _listContents.Add(images[val]);
        scrollingList.Refresh();
    }

    public void update(int index, int val){
        if (index == -1) return;
        _listContents[index] = images[val];
        scrollingList.Refresh();
    }

    public void AddToBank(int val){
        add(val);
    }

    public bool RemoveFromBank(int val){
        return remove(val);
    }

    public int FindInBank(int val){
        for (int i = 0; i < bankList.GetListLength(); i++)
            if ((Texture)bankList.GetListContent(i) == images[val])  
                return i;
        return -1;
    }
}
