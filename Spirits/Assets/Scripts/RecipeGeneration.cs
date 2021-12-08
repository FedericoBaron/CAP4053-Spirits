using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeGeneration : MonoBehaviour
{
    public int[] id = {1, 2, 3, 4, 5, 6};

    public int[] probDefault = {20, 20, 20, 20, 20, 0};

    public int[] proba1 = {50, 10, 15, 15, 10, 0}; 
    public int[] proba2 = {30, 20, 15, 15, 10, 10};

    public int[] probb1 = {30, 20, 15, 15, 10, 10};
    public int[] probb2 = {30, 20, 15, 15, 10, 10};

    public int[] probc1 = {30, 20, 15, 15, 10, 10};
    public int[] probc2 = {30, 20, 15, 15, 10, 10};

    public int[] curr;

    public void printArr(int[] arr){
        string x = "";
        for (int i = 0; i < arr.Length; i++)
            x += arr[i].ToString() + " ";
        Debug.Log(x);
    }

    public void setNext(int money, string scene){
        if (scene.Equals("Tutorial")){
           curr = probDefault;
        }
        else if (scene.Equals("Forest")){
            if (money >= 300){
                curr = proba2;
            }
            else
                curr = proba1;
        }
        else if (scene.Equals("Mansion")){
            if (money >= 500){
                curr = probb2;
            }
            else
                curr = probb1;
        }
        else if (scene.Equals("ApartmentWBarriers")){
            if (money >= 700){
                curr = probc2;
            }
            else
                curr = probc1;
        }
    }

    public int[] generateRecipe(int money, string scene){
        //Debug.Log(scene.name);
        setNext(money, scene);
        printArr(curr);
        int amt = 0;

        if (money >= 700)
            amt = (int)Random.Range(3, 6);
        else if (money >= 500)
            amt = (int)Random.Range(3, 5);
        else
            amt = (int)Random.Range(2, 4);

        int[] list = new int[amt];
        printArr(curr);
        for (int i = 0; i < amt; i++){
            float random = Random.Range(1, 100);
            int minR = 0;
            for (int j = 0; j < curr.Length; j++){
                int maxR = minR + curr[j];
                if (random > minR && random <= maxR){
                    list[i] = j;
                    break;
                }
                minR = maxR;
            }
        }
        return list;
    }

}