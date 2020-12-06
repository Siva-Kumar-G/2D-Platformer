using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RunTimeComponentRemover : MonoBehaviour
{
    private void Start()
    {
        var objectToDelete = gameObject.GetComponentsInChildren<Collider2D>();
        Debug.Log($"There are {objectToDelete.Length} Childs having this Component, Continue to Removing them");

        foreach (var col2D in objectToDelete)
        {
            DestroyImmediate(col2D);
        }

        Debug.Log("Deletion Completed!!!");
    }
}