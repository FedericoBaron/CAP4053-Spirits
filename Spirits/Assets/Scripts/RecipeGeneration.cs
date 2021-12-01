using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeGeneration : MonoBehaviour
{
    public int[] id = {1, 2, 3, 4, 5, 6};

    public int[] probDefault = {1, 0, 0, 0, 0, 0};

    public int[] proba1 = {90, 10, 0, 0, 0, 0}; 
    public int[] proba2 = {80, 20, 0, 0, 0, 0};

    public int[] probb1 = {15, 30, 50, 5, 0, 0};
    public int[] probb2 = {5, 20, 60, 15, 0, 0};

    public int[] probc1 = {0, 0, 20, 40, 30, 10};
    public int[] probc2 = {3, 3, 3, 3, 28, 60};

    public int[] curr;

    public void setNext(int money, string scene){
        if (scene == "Tutorial"){
           curr = probDefault;
        }
        else if (scene == "Forest"){
            if (money >= 300){
                curr = proba2;
            }
            else
                curr = proba1;
        }
        else if (scene == "Mansion"){
            if (money >= 500){
                curr = probb2;
            }
            else
                curr = probb1;
        }
        else if (scene == "Apartment"){
            if (money >= 700){
                curr = probc2;
            }
            else
                curr = probc1;
        }
    }

    public int[] generateRecipe(int money, string scene){

        setNext(money, scene);

        int amt = 0;
        if (money >= 700)
            amt = (int)Random.Range(3,4);
        else if (money >= 500)
            amt = (int)Random.Range(2,4);
        else
            amt = (int)Random.Range(1,3);

        int[] list = new int[amt];
        for (int i = 0; i < amt; i++){
            float random = Random.Range(0, 100);
            int minR = 0;
            for (int j = 0; j < curr.Length; j++){
                int maxR = minR + curr[j];
                if (random >= minR && random <= maxR){
                    list[i] = j + 1;
                    break;
                }
                minR = maxR;
            }
        }

        return list;
    }

}