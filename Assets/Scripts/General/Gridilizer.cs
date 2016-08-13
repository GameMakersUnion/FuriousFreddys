using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

[RequireComponent(typeof(SpriteRenderer))]
public class Gridilizer : MonoBehaviour
{
    [Header("Debuggers:")]

    [Tooltip("For debugging, takes performance hit")]
    public bool useGrid;
    private bool gridBuiltAttempted = false;

    [Tooltip("For debugging, takes performance hit. \n" +
       "e.g. Works with rocks, but Zombies are ignored, \n" + 
        "since performance is too horrendously slow")]
    public bool showCollisionMarkers;

    [Tooltip("For debugging, takes performance hit. \n" +
        "e.g. Works with rocks, but Zombies are ignored, \n" +
        "since performance is too horrendously slow")]
    public bool printCollisionInfo;

    [Tooltip("For debugging, just annoying to see")]
    public bool flashCollidingSprite;
    
    [Space(10)]

    [Tooltip("Attach your sprite sheets here. Ensure is sliced in Unity with Sprite Editor.")]
    public Sprite[] gridLayers;

    SpriteRenderer sr;
    GameObject gridBucket;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gridBucket = BuildGridIn(sr.sprite);

        ApplyPhysicsFromParentToChildren();
        //DisablePhysicsOnParent();
        SetGridHitPoints();
    }

    void Update()
    {
        ToggleGridOnOrOff();
        DetectDamageToGrid();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject == this.gameObject || col.gameObject.tag == "Cell" ) return;

        foreach (ContactPoint2D contact in col.contacts)
        {
            UtilSpawnMarkerAt(contact);
            SpriteRenderer sr = contact.otherCollider.gameObject.GetComponent<SpriteRenderer>();
            StartCoroutine(FlashSprite(sr));

        }
    }

    void UtilSpawnMarkerAt(ContactPoint2D contact)
    {
        if (!showCollisionMarkers || contact.collider.GetComponent<HingeJoint2D>() != null || contact.collider.GetComponent<MasterZombieScript>() != null) return;

        if (printCollisionInfo) print(contact.collider.name + " hit " + contact.otherCollider.name + " at " + contact.point);

        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.name = "Marker_" + contact.point;
        marker.transform.position = contact.point * 1000;
        marker.transform.position /= 1000;
        marker.transform.localScale = new Vector3(0.1f, 1, 1);
        GameObject marker2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker2.transform.position = contact.point;
        marker2.transform.localScale = new Vector3(1, 0.1f, 1);
        marker2.transform.parent = marker.transform;
        StartCoroutine(DelayedBreak(marker));
    }

    IEnumerator DelayedBreak(GameObject go)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(go);
    }

    //warning: does not protect against toggling off mid-collision (when reference to sr is destroyed)
    IEnumerator FlashSprite(SpriteRenderer sr)
    {
        if (!flashCollidingSprite) yield break;
        sr.material.color = Color.blue;
        yield return new WaitForSeconds(0.2f);
        sr.material.color = Color.white;
    }

    void DetectDamageToGrid()
    {

    }

    GameObject BuildGridIn(Sprite tileContainer)
    {
        if (!useGrid || gridLayers.Length <= 0) return null;

        gridBuiltAttempted = true;

        List<Sprite> tiles = new List<Sprite>();

        foreach (Sprite gridLayer in gridLayers)
        {
            string path = AssetDatabase.GetAssetPath(gridLayer);
            path = StringUtilsExt.SubstringBetween(path, "Resources" + "/", ".");
            Sprite[] album_sprites = Resources.LoadAll<Sprite>(path);

            if (album_sprites.Length == 0)
            {
                Debug.LogWarning("Unable to load grid layer, couldn't find file: " + gridLayer.name);
                return null;
            }

            foreach (Sprite sprite in album_sprites)
            {
                tiles.Add(sprite);
            }
        }

        GameObject gridContainer = SpawnGridFrom(tiles, tileContainer);
        return gridContainer;
    }

    //for performance reasons and better debugging, this can be toggled from the inspector. 
    //not it is not supported to toggle off mid-collision
    void ToggleGridOnOrOff()
    {
        //is this the most efficent way or cleanest way to write this??? I'm not sure. 
        if (!useGrid && (gridBucket != null))
        {
            gridBuiltAttempted = false;
            Destroy(gridBucket);
        }
        else if (useGrid && !gridBuiltAttempted && (gridBucket == null))
        {
            gridBuiltAttempted = true;
            gridBucket = BuildGridIn(sr.sprite);
        }
    }

    //spawn new grid of gameobjects, each having a single sprite
    GameObject SpawnGridFrom(List<Sprite> tiles, Sprite tileContainer)
    {
        Vector2 sizeParent = GetDimensionInPX(tileContainer);
        Vector2 size = GetDimensionInPX(tiles[0]);

        GameObject gridContainer = new GameObject("Grid");
        gridContainer.transform.position = this.transform.position;
        gridContainer.transform.parent = this.transform;

        int count = 0;
        GameObject[,] grid = new GameObject[4, 6]; //TODO: still magic, TBD...
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                string cellName = transform.name + "Cell_" + count + "_[" + x + "," + y + "]";
                GameObject cell = SpawnCell(cellName);
                Vector3 cellPosition = new Vector3(-sizeParent.x/2 + x * size.x + size.x/2, sizeParent.y/2 - (y * size.y + size.y/2), 0);
                cell.transform.position = transform.position + (cellPosition * transform.localScale.x);
                cell.transform.parent = gridContainer.transform;
                grid[x, y] = cell;
                SpriteRenderer sr = cell.AddComponent<SpriteRenderer>();
                sr.sprite = tiles[count];
                sr.sortingLayerName = this.sr.sortingLayerName;
                sr.sortingOrder += 1;
                count++;
            }
        }
        return gridContainer;
    }

    GameObject SpawnCell(string cellName)
    {
        GameObject cell = new GameObject(cellName);
        cell.transform.localScale = transform.localScale;
        cell.tag = "Cell";
        return cell;
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
        GameObject parent = this.gameObject;
        GameObject children = this.gridBucket;
        if (children == null) return;

        //Collider2D col = this.GetComponent<BoxCollider2D>();
        foreach (Transform child in children.transform)
        {
            child.gameObject.AddComponent<BoxCollider2D>(); //TODO: generlize
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
        GameObject children = this.gridBucket;
        if (children == null) return;

        int health = this.GetComponent<VehicleControlScript>().health;
        foreach (Transform child in children.transform)
        {
            //child.gameObject.health = health / 24;

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

    //Generalized version
    public static T FindComponentOn<T>(string nameGameObject) where T : Component
    {
        GameObject find = GameObject.Find(nameGameObject);
        T findComponent = null;
        if (find != null)
        {
            findComponent = find.GetComponent<T>();
        }
        return findComponent;
    }

    //TODO: refactor this, desire to generalize 
    public static GameControllerScript FindComponentOn(string nameGameObject)
    {
        GameObject find = GameObject.Find(nameGameObject);
        GameControllerScript findComponent = null;
        if (find != null)
        {
            findComponent = find.GetComponent<GameControllerScript>();
        }
        return findComponent;
    }

    /// <summary>
    /// Gets or add a component. Usage example:
    /// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
    /// </summary>
    static public T GetOrAddComponent<T>(this Component child) where T : Component
    {
        T result = child.GetComponent<T>();
        if (result == null)
        {
            result = child.gameObject.AddComponent<T>();
        }
        return result;
    }


    public static Component FindAndAssignComponentTo<T>(string name) where T : Component
    {
        GameObject g = GameObject.Find(name);
        if (g != null) return g.GetComponent<T>();
        return null;
    }



}

public class StringUtilsExt
{
    /// <summary>
    /// Gets the String that is nested in between two Strings. Only the first match is returned.
    /// A null input String returns null. A null open/close returns null (no match). An empty("") open and close returns an empty string.
    /// (Note copy pasted from Java and adapted in a rush, not thoroughly checked, probably some mistakes)
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