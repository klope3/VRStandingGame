using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiObjectSetActive : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    public void SetAllActive(bool b)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(b);
        }
    }
}
