using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public static class UnityUtils {

    /// call with grid.FindInDimensions(col.gameObject)
    /*
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
    }*/

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

    public static T FindComponentTagged<T>(string tagOfGameObject) where T : Component
    {
        GameObject find = GameObject.FindGameObjectWithTag(tagOfGameObject);
        T findComponent = null;
        if (find != null)
        {
            findComponent = find.GetComponent<T>();
        }
        return findComponent;
    }

    //TODO: refactor this, desire to generalize 
    public static ZombieSpawnOnceController FindComponentOn(string nameGameObject)
    {
        GameObject find = GameObject.Find(nameGameObject);
        ZombieSpawnOnceController findComponent = null;
        if (find != null)
        {
            findComponent = find.GetComponent<ZombieSpawnOnceController>();
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

    public static Vector2 GetDimensionInPX(Sprite sprite)
    {
        Vector2 dimension;

        dimension.x = sprite.bounds.size.x;
        dimension.y = sprite.bounds.size.y;

        return dimension;
    }

    public struct Vector2Int
    {
        int x;
        int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }


    public static GameObject createGameObject(string name)
    {
        GameObject go = new GameObject(name);
        go.name = name;
        return go;
    }

    public static GameObject createGameObject(Sprite sprite)
    {
        GameObject go = createGameObject(sprite.name);
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        return go;
    }

    public static List<GameObject> createGameObjects(Sprite[] sprites)
    {
        List<GameObject> gos = new List<GameObject>();
        foreach (Sprite sprite in sprites)
        {
            gos.Add(createGameObject(sprite));
        }
        return gos;
    }

    public static GameObject createGameObjectOfSprite(Sprite sprite)
    {
        GameObject go = new GameObject(sprite.name);
        go.name = sprite.name;
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        return go;

    }

    //temp hack, TODO: replace body with much better generic version later.
    public static GameObject FindCanvas()
    {
        return GameObject.Find("GameCanvas");
    }

    public static GameObject AddTextToCanvas(string textString)
    {
        //GameObject canvas = FindCanvas();

        GameObject textGameObject = new GameObject(textString);
        //textGameObject.transform.parent = canvas.transform;

        TextMesh text = textGameObject.AddComponent<TextMesh>();
        text.text = textString;

        //Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        //text.font = ArialFont;
        //text.material = ArialFont.material;

        text.offsetZ -= 1;

        return textGameObject;
    }



    //public static bool IsCanvasPresent()
    //{

    //}

    //public static bool IsEventSystemPresent()
    //{

    //}

    //public static bool AddOrFindUISystem()
    //{

    //}

    //private static List GetObjectsInLayer(int layer, GameObject root = null)
    //{
    //    var result = new List();
    //    foreach (Transform t in root.transform.GetComponentsInChildren(typeof(GameObject), true))
    //    {
    //        if (t.gameObject.layer == layer)
    //        {
    //            result.Add(t.gameObject);
    //        }
    //    }
    //    return result;
    //}


}
