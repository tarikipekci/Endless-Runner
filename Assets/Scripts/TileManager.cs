using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Tile[] tiles;
    private float zSpawn;
    private float tileLength;

    private void Start()
    {
        SpawnTile(0);
        SpawnTile(1);
        SpawnTile(2);
    }

    private void SpawnTile(int tileIndex)
    {
        Instantiate(tiles[tileIndex], transform.forward * zSpawn, Quaternion.identity);
        var meshRenderer = tiles[tileIndex].MeshRenderer;
        tileLength = meshRenderer.bounds.size.z - 1.5f;
        zSpawn += tileLength;
    }
}
