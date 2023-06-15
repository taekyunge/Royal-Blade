using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private DropObject[] dropObjects = null;

    public int count { get { return dropObjects.Length; } }

    public bool isClear
    {
        get
        {
            return !Array.Find(dropObjects, x => x.gameObject.activeSelf);
        }
    }

    public void Initialize()
    {
        if(dropObjects == null)
            dropObjects = gameObject.GetComponentsInChildren<DropObject>(true);

        transform.localPosition = new Vector3(0, 50, 0);

        Randomize();
    }

    public void Randomize()
    {
        if(dropObjects != null)
        {            
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < dropObjects.Length; i++)
            {
                var dropObject = dropObjects[i];

                pos.y = pos.y + 1.2f * UnityEngine.Random.Range(1, 5);

                dropObject.transform.localPosition = pos;
                dropObject.gameObject.SetActive(true);
            }
        }
    }
}
