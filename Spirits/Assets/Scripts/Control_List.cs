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
    public string[] disablePlayer;
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
        // populate();
        //testing();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.Find("Bartender");
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
            currExist = current;
            Reset();
        }
        bool setDisable = false;
        for (int i = 0; i < disablePlayer.Length; i++){
            if (sceneCurr.name == disablePlayer[i]){
                if (player != null){
                    player.SetActive(false);
                    setDisable = true;
                }
            }
        }
        if (!setDisable)
            player.SetActive(true);
        currExist = current;
    }

    void Reset(){
        Scene sceneCurr = SceneManager.GetActiveScene();
        Player_Combat pc = GetComponent<Player_Combat>();
        pc.selectIconPos = GameObject.Find("Selector").GetComponent<RectTransform>();
        pc.selectIconRGB = GameObject.Find("Selector").GetComponent<Image>();
		pc.uiInventory = GameObject.Find("GhostCount");
		pc.counts = pc.uiInventory.GetComponentsInChildren<Text>();
        pc.capturedGhosts = new int[6];
        for (int i = 0; i < 6; i++){
            pc.capturedGhosts[i] = 0;
            pc.counts[i].text = pc.capturedGhosts[i].ToString();
        }
        pc.health = 100;
        List = GameObject.Find("GroceryList");
        scrollingList = List.GetComponent<CircularScrollingList>();
        bankList = List.GetComponent<IntListBank>(); 
        _listContents = new List<int[]>();
        bankList._listContents = _listContents;
        populate();
        generate = GetComponent<RecipeGeneration>();
        GetComponent<SpawnSettings>().setDifficulty(sceneCurr.name);

    }

    int[] generateRecipe(){
        Scene sceneCurr = SceneManager.GetActiveScene();
        int money = GetComponent<Player_Combat>().totalMoney;
        int[] recipe = generate.generateRecipe(money, sceneCurr.name);
        int[] freq = new int[6];
        for (int i = 0; i < recipe.Length; i++){
           Debug.Log(recipe[i]);
            freq[recipe[i]]++;
        }
        return freq;
    }

    void populate(){
        add(new int[]{-1});
        add(new int[]{-1});
        add(new int[]{-1});
        add(new int[]{-1});
        add(new int[]{-1});
        add(new int[]{-1});
        add(new int[]{-1});
    }

    void testing(){
        add(new int[]{2, 3, 1, 3, 2, 3});
        add(new int[]{2, 3, 1, 3, 2, 3});
        add(new int[]{2, 3, 1, 0, 2, 3});
        add(new int[]{1, 2, 0, 3, 4, 5});
        add(new int[]{2, 3, 1, 3, 0, 0});
        add(new int[]{2, 3, 1, 3, 2, 3});
        add(new int[]{2, 3, 1, 3, 2, 3});
        add(new int[]{2, 3, 1, 0, 2, 3});
        add(new int[]{1, 2, 0, 3, 4, 5});
        add(new int[]{2, 3, 1, 3, 0, 0});
        add(new int[]{2, 3, 1, 3, 2, 3});
        add(new int[]{2, 3, 1, 3, 2, 3});
        add(new int[]{2, 3, 1, 0, 2, 3});
        add(new int[]{1, 2, 0, 3, 4, 5});
        add(new int[]{2, 3, 1, 3, 0, 0});
        //add(generateRecipe());
        //printList();
        //add(generateRecipe());
        //add(generateRecipe());
        //add(generateRecipe());
        //add(generateRecipe());
        //add(generateRecipe());
    }

    void Update(){
        if (currExist){
            if (_listContents.Count <= 7)
                add(generateRecipe());
            scrollingList.Refresh();
        }
    }

   public bool remove(int[] val){
        if (!currExist) return false;
        int index = find(val);
        if (index == -1) return false;
        bool flag = _listContents.Remove(_listContents[index]);
        int[] empty = {-1};
        if (_listContents.Count < 7)
            add(empty);
        scrollingList.Refresh();
        return flag;
    }

    public void add(int[] val){
        if (!currExist) return;
        int[] empty = {-1};
        int loc = -1;
        if (val[0] != -1)
            loc = find(empty);
        if (loc != -1) _listContents[loc] = val;
        else _listContents.Add(val);
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

    public void printList(){
        Debug.Log("Start");
        Debug.Log(_listContents.Count);
        for (int i = 0; i < _listContents.Count; i++){
            string x = "";
            for (int j = 0; j < _listContents[i].Length; j++){
                x += (_listContents[i][j].ToString()) + " ";
            }
            Debug.Log("Val " + (i.ToString()) + ": " +  x);
        }
        Debug.Log("End");
    }
}
