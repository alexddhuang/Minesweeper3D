using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Brick : MonoBehaviour
{
    private static Dictionary<string, Sprite> mTileImages;

    public bool mine = false;

    public float radius = 1.42f;

    public SpriteRenderer tile = null;

    private List<Brick> mNeighbors;

    public static void BuildSpritesMap()
    {
        if (mTileImages == null) {
            Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Textures/MinesweeperSpritesheet.png");
            mTileImages = new Dictionary<string, Sprite>();
            for (int i = 0; i < sprites.Length; i++) {
                mTileImages.Add(sprites[i].name, (Sprite) sprites[i]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FindNeighbors();
        BuildSpritesMap();
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

    public void ShowSecret()
    {
        string name;

        if (mine) {
            name = "TileMine";
        } else {
            int num = 0;
            mNeighbors.ForEach(brick => {
                if (brick.mine) num += 1;
            });
            name = $"Tile{num}";
        }

        Sprite sprite;
        if (mTileImages.TryGetValue(name, out sprite))
            tile.sprite = sprite;
    }
}
