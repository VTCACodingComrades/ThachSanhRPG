using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeflDestroy : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 0.5f;
    private void Start() {
        Destroy(gameObject, timeToDestroy);
    }
}
