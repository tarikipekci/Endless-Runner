using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    public Tile[] tiles;
    private float zSpawn;
    private float tileLength;
    private int numberOfTiles;
    private List<Tile> activeTiles = new List<Tile>();
    public Transform playerTransform;

    private void Start()
    {
        numberOfTiles = tiles.Length;
        for (int i = 0; i < numberOfTiles; i++)
        {
            var randomTileIndex = Random.Range(0, tiles.Length);
            if (i == 0 && randomTileIndex == 1)
            {
                randomTileIndex = 0;
            }
            SpawnTile(randomTileIndex);
        }
    }

    private void Update()
    {
        if (playerTransform.position.z - (2.5f * tileLength) > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tiles.Length));
            DestroyTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        var newTile = Instantiate(tiles[tileIndex], transform.forward * zSpawn, Quaternion.identity);
        activeTiles.Add(newTile);
        var meshRenderer = tiles[tileIndex].MeshRenderer;
        tileLength = meshRenderer.bounds.size.z;
        zSpawn += tileLength;
    }

    private void DestroyTile()
    {
        Destroy(activeTiles[0].gameObject);
        activeTiles.RemoveAt(0);
    }
}
