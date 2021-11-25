using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AirFishLab.ScrollingList;
using AirFishLab.ScrollingList.Demo;
using UnityEngine.SceneManagement;

public class Control_List : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject List;
    public CircularScrollingList scrollingList;
    public IntListBank bankList;
    public List<Texture> _listContents = new List<Texture>();
    public List<Texture> images;
    public bool currExist = true;
    public string[] badScenes;
    
    private void Awake()
    {
        // It is save to remove listeners even if they
        // didn't exist so far.
        // This makes sure it is added only once
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Add the listener to be called when a scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        //DontDestroyOnLoad(gameObject);

        // Store the creating scene as the scene to trigger start
        //scene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        Reset();
        //Debug.Log(bankList.GetListLength());
        //Debug.Log(images.Count);
        populate();
        // for (int i = 0; i < bankList.GetListLength(); i++)
        //     Debug.Log(bankList.GetListContent(i));
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
        List = GameObject.Find("GroceryList");
        scrollingList = List.GetComponent<CircularScrollingList>();
        bankList = List.GetComponent<IntListBank>();    
        bankList._listContents = _listContents;
        Player_Combat player = GetComponent<Player_Combat>();
        GameObject currHealthBar = GameObject.Find("HealthMask");
		if (currHealthBar != null){
			player.healthBar = currHealthBar.GetComponent<HealthBar>();
            //player.healthBar.player = this.transform;
            Debug.Log(player.damageTakenCurrently);
            player.healthBar.UpdateHealthBar(player.damageTakenCurrently);
        }
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
        // Return the current Active Scene in order to get the current Scene name.        
        //Debug.Log(currExist);
        if (currExist){
            scrollingList.Refresh();
        }

    }

    public bool remove(int val){
        if (!currExist) return false;
        bool v = _listContents.Remove(images[val]);
        scrollingList.Refresh();
        return v;
    }

    public void add(int val){
        if (!currExist) return;
        _listContents.Add(images[val]);
        scrollingList.Refresh();
    }

    public void update(int index, int val){
        if (index == -1) return;
        if (!currExist) return;
        _listContents[index] = images[val];
        scrollingList.Refresh();
    }

    public void AddToBank(int val){
        if (!currExist) return;
        add(val);
    }

    public bool RemoveFromBank(int val){
        if (!currExist) return false;
        return remove(val);
    }

    public int FindInBank(int val){
        if (!currExist) return -1;
        for (int i = 0; i < bankList.GetListLength(); i++)
            if ((Texture)bankList.GetListContent(i) == images[val])  
                return i;
        return -1;
    }
}
