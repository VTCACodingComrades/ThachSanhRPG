using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllowDestructing : MonoBehaviour
{
    public void AllowDestruct()
    {
        List<Destructible> gameObjects = FindObjectsOfType<Destructible>().ToList();
        List<Destructible> newgameObjects = GetComponentsInChildren<Destructible>().ToList();
        foreach (var item in newgameObjects)
        {
            item.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
