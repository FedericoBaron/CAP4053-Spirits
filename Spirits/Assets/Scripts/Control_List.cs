using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AirFishLab.ScrollingList;
using AirFishLab.ScrollingList.Demo;
using UnityEngine.SceneManagement;

public class Control_List : MonoBehaviour
{
    public GameObject List;
    public CircularScrollingList scrollingList;
    public IntListBank bankList;
    public List<int[]> _listContents = new List<int[]>();
    public RecipeGeneration generate;
    public bool currExist = true;
    public string[] badScenes;
    public float currentTime = 0;
    public float lastFillValue = 0;
    
    private void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        Reset();
        populate();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Re-Initializing", this);
        Scene sceneCurr = SceneManager.GetActiveScene();
        bool current = true;
        for (int i = 0; i < badScenes.Length; i++){
            if (sceneCurr.name == badScenes[i]){
                current = false;
                break;
            }
        }

        if (!currExist && current){
            Reset();
        }

        currExist = current;
    }

    void Reset(){
        Player_Combat pc = GetComponent<Player_Combat>();
        pc.selectIcon = GameObject.Find("Selector").GetComponent<RectTransform>();
		pc.uiInventory = GameObject.Find("GhostCount");
		pc.counts = pc.uiInventory.GetComponentsInChildren<Text>();
        List = GameObject.Find("GroceryList");
        scrollingList = List.GetComponent<CircularScrollingList>();
        bankList = List.GetComponent<IntListBank>();    
        bankList._listContents = _listContents;
        generate = GetComponent<RecipeGeneration>();
        populate();
    }

    int[] generateRecipe(){
        Scene sceneCurr = SceneManager.GetActiveScene();
        int[] recipe = generate.generateRecipe(GetComponent<Player_Combat>().money, sceneCurr.name);
        int[] freq = new int[6];
        for (int i = 0; i < recipe.Length; i++){
           Debug.Log(recipe[i]);
            freq[recipe[i]]++;
        }
        return freq;
    }

    void populate(){
        add(new int[]{1, 0, 0, 0, 0, 0});
        add(new int[]{1, 1, 0, 0, 0, 0});
        add(new int[]{1, 1, 1, 0, 0, 0});
        add(new int[]{1, 1, 1, 1, 0, 0});
        add(new int[]{1, 1, 1, 1, 1, 0});
        add(new int[]{1, 1, 1, 1, 1, 1});
    }

    void Update(){
        if (currExist){
            scrollingList.Refresh();
        }
    }

    public bool remove(int[] val){
        if (!currExist) return false;
        int index = find(val);
        if (index == -1) return false;
        bool flag = _listContents.Remove(_listContents[index]);
        scrollingList.Refresh();
        return flag;
    }

    public void add(int[] val){
        if (!currExist) return;
        _listContents.Add(val);
        scrollingList.Refresh();
    }

    public void update(int index, int[] val){
        if (index == -1) return;
        if (!currExist) return;
        _listContents[index] = val;
        scrollingList.Refresh();
    }

    public int find(int[] val){
        if (!currExist) return -1;
        for (int i = 0; i < bankList.GetListLength(); i++)
            if (equalsArray((int[])bankList.GetListContent(i), val))  
                return i;
        return -1;
    }

    public bool equalsArray(int[] a, int[] b){
        for (int i = 0; i < a.Length; i++){
            if (a[i] != b[i])
                return false;
        }
        return true;
    }
}
