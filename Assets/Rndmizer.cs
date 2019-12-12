using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rndmizer : MonoBehaviour
{
    public Color ColorR;
    public GameObject prefab;

    // Instantiate the Prefab somewhere between -10.0 and 10.0 on the x-z plane
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //int RandomNumber = Random.Range(1, 100);
        //Debug.Log(RandomNumber);
        //new WaitForSeconds(3f);
    }
    float Choose (float[] probs) {

        float total = 100;

        foreach (float elem in probs) {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i= 0; i < probs.Length; i++) {
            if (randomPoint < probs[i]) {
                return i;
            }
            else {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
}
