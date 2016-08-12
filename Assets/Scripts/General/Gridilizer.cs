using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public class Gridilizer : MonoBehaviour
{

    public Sprite[] sections;
    SpriteRenderer sr;
    GameObject[,] grid;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        grid = new GameObject[4,6];
        AssignGridTo(sr.sprite);
        
    }

    void Update()
    {
        DetectDamageToGrid();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject == this.gameObject || col.gameObject.tag == "Cell") return;

        foreach (ContactPoint2D contact in col.contacts)
        {
            UtilSpawnMarkerAt(contact);
            SpriteRenderer sr = contact.otherCollider.gameObject.GetComponent<SpriteRenderer>();
            StartCoroutine(FlashSprite(sr));

        }
    }

    void UtilSpawnMarkerAt(ContactPoint2D contact)
    {
        print(contact.collider.name + " hit " + contact.otherCollider.name + " at " + contact.point);
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.name = "Marker_" + contact.point;
        marker.transform.position = contact.point * 1000;
        marker.transform.position /= 1000;
        marker.transform.localScale = new Vector3(0.1f, 1, 1);
        GameObject marker2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker2.transform.position = contact.point * 1000;
        marker2.transform.position /= 1000;
        marker2.transform.localScale = new Vector3(1, 0.1f, 1);
        marker2.transform.parent = marker.transform;
        StartCoroutine(DelayedBreak(marker));
    }

    IEnumerator DelayedBreak(GameObject go)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(go);
    }

    IEnumerator FlashSprite(SpriteRenderer sr)
    {
        sr.material.color = Color.blue;
        yield return new WaitForSeconds(0.2f);
        sr.material.color = Color.white;
    }

    void DetectDamageToGrid()
    {

    }

    void AssignGridTo(Sprite tileset)
    {
        if (sections.Length <= 0) return;
        List<Sprite> tiles = new List<Sprite>();

        foreach (Sprite section in sections)
        {
            string path = AssetDatabase.GetAssetPath(section);
            path = StringUtilsExt.SubstringBetween(path, "Resources" + "/", ".");
            Sprite[] album_sprites = Resources.LoadAll<Sprite>(path);
            if (album_sprites.Length == 0) return;

            foreach (Sprite sprite in album_sprites)
            {
                tiles.Add(sprite);
            }

        }

        SpawnGrid(tileset, tiles);
        ApplyPhysicsFromParentToChildren();
        //DisablePhysicsOnParent();
        SetGridHitPoints();
    }
        
    //spawn new grid of gameobjects, each having a single sprite
    void SpawnGrid(Sprite tileset, List<Sprite> tiles)
    {
        Vector2 sizeSet = GetDimensionInPX(tileset);
        Vector2 size = GetDimensionInPX(tiles[0]);

        GameObject grid = new GameObject("Grid");
        grid.transform.position = this.transform.position;
        grid.transform.parent = this.transform;

        int count = 0;
        for (int y = 0; y < this.grid.GetLength(1); y++)
        {
            for (int x = 0; x < this.grid.GetLength(0); x++)
            {
                //SpawnCell();
                //SetCellStuff();??
                string cellName = transform.name + "Cell_" + count + "_[" + x + "," + y + "]";
                GameObject cell = new GameObject(cellName);
                Vector3 cellPosition = new Vector3(-sizeSet.x/2 + x * size.x + size.x/2, sizeSet.y/2 - (y * size.y + size.y/2), 0);
                cellPosition *= transform.localScale.x;
                cell.transform.position = transform.position + cellPosition;
                cell.transform.localScale = transform.localScale;
                //string str = cellName + ": " + cell.transform.position * 1000 ;
                cell.transform.parent = grid.transform;
                cell.tag = "Cell";
                this.grid[x, y] = cell;
                SpriteRenderer sr = cell.AddComponent<SpriteRenderer>();
                sr.sprite = tiles[count];
                sr.sortingLayerName = this.sr.sortingLayerName;
                sr.sortingOrder += 1;
                //str +=  ", bounds: Center:" + sr.bounds.center * 1000 + ", Extents: " + sr.bounds.extents * 1000 ;
                //print(str);
                count++;
            }
        }
    }

    private Vector2 GetDimensionInPX(Sprite sprite)
    {
        Vector2 dimension;

        dimension.x = sprite.bounds.size.x;
        dimension.y = sprite.bounds.size.y;

        return dimension;
    }

    void ApplyPhysicsFromParentToChildren()
    {
        //Collider2D col = this.GetComponent<BoxCollider2D>();

        foreach (GameObject child in grid)
        {
            child.AddComponent<BoxCollider2D>(); //TODO: generlize
            //Rigidbody2D rb = child.AddComponent<Rigidbody2D>();
            //rb.isKinematic = true;
            //rb.gravityScale = parentRb.gravityScale;
        }
    }

    void DisablePhysicsOnParent()
    {
        List<Collider2D> comps = GetComponents<Collider2D>().ToList();
        foreach (Collider2D comp in comps)
        {
            comp.isTrigger = true;
        }
    }

    void EnablePhysicsOnChildren()
    {

    }



    void SetGridHitPoints()
    {
        int health = this.GetComponent<VehicleControlScript>().health;
        foreach (GameObject go in grid)
        {
            //go.health = health / 24;
        }
    }
}

public static class Utils
{
    /// call with grid.FindInDimensions(col.gameObject)
    public static bool FindInDimensions(this object[,] target, object searchTerm)
    {
        var rowLowerLimit = target.GetLowerBound(0);
        var rowUpperLimit = target.GetUpperBound(0);

        var colLowerLimit = target.GetLowerBound(1);
        var colUpperLimit = target.GetUpperBound(1);

        for (int row = rowLowerLimit; row < rowUpperLimit; row++)
        {
            for (int col = colLowerLimit; col < colUpperLimit; col++)
            {
                // you could do the search here...
                if (target[row,col] == searchTerm) 
                    return true;
            }
        }

        return false;
    }

}

public class StringUtilsExt
{
    /// <summary>
    /// Gets the String that is nested in between two Strings. Only the first match is returned.
    /// A null input String returns null. A null open/close returns null (no match). An empty("") open and close returns an empty string.
    /// </summary>
    public static string SubstringBetween(string str, string open, string close)
    {
        /// <pre>
        /// str - the String containing the substring, may be null
        /// open - the String before the substring, may be null
        /// close - the String after the substring, may be null
        /// </pre>
        int pFrom = (str.IndexOf(open) == -1) ? 0 : str.IndexOf(open) + open.Length;
        int pTo = str.LastIndexOf(close);

        string result = str.Substring(pFrom, pTo - pFrom);
        return result;
    }
}

