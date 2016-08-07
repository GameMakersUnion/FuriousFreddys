using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Sectioner : MonoBehaviour
{

    SpriteRenderer sr;
    Sprite sprite;

    void Start()
    {
        //sr = GetSpriteDetails();
        //AssignGrid(4);
        //asdf();
        sprite = GenerateNewBlock();
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void Update()
    {

    }


    public Sprite GenerateNewBlock()
    {
        //Sprite sprite = new Sprite();

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        Texture2D tx = Resources.Load<Texture2D>("Rock");

        //Texture2D tx = GetTextureFrom(s);

        sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), new Vector2(0, 0), 100.0f);
        sprite.name = "BOB";
        Debug.Log("Bounds: " + sprite.bounds + " Rect: " + sprite.rect + " TexRect: " + sprite.textureRect);
        return sprite;

    }

    Texture2D GetTextureFrom(Sprite sprite) {

        SetTextureImporterFormat(sprite.texture, true);

        var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();
        return croppedTexture;

    }

    public static void SetTextureImporterFormat(Texture2D texture, bool isReadable)
    {
        if (null == texture) return;

        string assetPath = AssetDatabase.GetAssetPath(texture);
        var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (tImporter != null)
        {
            tImporter.textureType = TextureImporterType.Advanced;

            tImporter.isReadable = isReadable;

            AssetDatabase.ImportAsset(assetPath);
            AssetDatabase.Refresh();
        }
    }




    SpriteRenderer GetSpriteDetails()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sprite = sr.sprite;
        return sr;
    }

    void AssignGrid(float divideBy)
    {
        Vector2 v = GetDimensionInPX(gameObject);
        v /= divideBy;
        //v = new Vector2(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        //print(v);

        int squareSize = (v.x < v.y) ? (int)v.x : (int)v.y;
        List<GameObject> tiles = new List<GameObject>();

        for (int i = 0; i < divideBy; i++)
        {
            for (int j = 0; j < divideBy; j++)
            {
                print(i + "," + j);
                Instantiate(Resources.Load<Sprite>("Vehicle"), transform.position + squareSize * new Vector3(v.x, v.y, 0), Quaternion.identity);
                //Sprite sprite_ = (tile.AddComponent<SpriteRenderer>().sprite = sprite);
                //tile.transform.localScale = 
                //tiles.Add(tile);
            }
        }

    }

    private Vector2 GetDimensionInPX(GameObject obj)
    {
        Vector2 dimension;

        dimension.x = obj.GetComponent<SpriteRenderer>().sprite.bounds.size.x;  
        dimension.y = obj.GetComponent<SpriteRenderer>().sprite.bounds.size.y;  

        return dimension; 
    }

    void asdf()
    {
        
        Texture2D old = sr.sprite.texture;
        Texture2D left = new Texture2D((int)(old.width), old.height, old.format, false);
        Color[] colors = old.GetPixels(0, 0, (int)(old.width), old.height);
        left.SetPixels(colors);
        left.Apply();
        Sprite sprite = Sprite.Create(left,
               new Rect(0, 0, left.width, left.height),
               new Vector2(0.5f, 0.5f),
               40);
        Debug.Log("Old Bounds: " + sr.sprite.bounds + " Rect: " + sr.sprite.rect + " TexRect: " + sr.sprite.textureRect);
        Debug.Log("Bounds: " + sprite.bounds + " Rect: " + sprite.rect + " TexRect: " + sprite.textureRect);
        sr.sprite = sprite;

    }
}


/**
 * Runs automatically. 
 */
 public class TexturePostProcessor : AssetPostprocessor
{

    void OnPreprocessTexturee()
    {

        {
            TextureImporter importer = assetImporter as TextureImporter;
            importer.textureType = TextureImporterType.Advanced;
            importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
            importer.isReadable = true;
            importer.filterMode = FilterMode.Point;
            importer.npotScale = TextureImporterNPOTScale.None;

            Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
            if (asset)
            {
                EditorUtility.SetDirty(asset);
            }
            else
            {
                importer.textureType = TextureImporterType.Advanced;
            }
        }

    }
}
