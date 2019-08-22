using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrickState {
    Unknown,
    Known,
}

public class Brick : MonoBehaviour
{
    public bool mine = false;

    public float radius = 1.42f;

    private List<Brick> mNeighbors;

    // Start is called before the first frame update
    void Start()
    {
        FindNeighbors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindNeighbors()
    {
        var allBricks = GameObject.FindGameObjectsWithTag("Brick");

        mNeighbors = new List<Brick>();

        for (int i = 0; i < allBricks.Length; i++) {
            var brick = allBricks[i];
            var distance = Vector3.Distance(transform.position, brick.transform.position);
            if (0 < distance && distance <= radius) {
                mNeighbors.Add(brick.GetComponent<Brick>());
            }
        }

        Debug.Log($"{mNeighbors.Count} neighbors");
    }
}
