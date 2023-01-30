using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaydua : MonoBehaviour
{
    [SerializeField] private GameObject[] cot;
    [SerializeField] private float tocdo = 2f;
    private int currenWaypointintIndex = 0;

    private void Update() {
        if(Vector2.Distance(cot[currenWaypointintIndex].transform.position, transform.position) < 1f){
            currenWaypointintIndex ++;
            if(currenWaypointintIndex >= cot.Length){
                currenWaypointintIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, cot[currenWaypointintIndex].transform.position, Time.deltaTime * tocdo);
    }
}
