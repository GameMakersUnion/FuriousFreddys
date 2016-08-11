using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public class Gridilizer : MonoBehaviour
{

    public Sprite[] sections;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        AssignGridTo(sr.sprite);
        //ShowGrid();
        //sr = GetSpriteDetails();

    }

    void Update()
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
        Texture2D tx = BuildTileset(tileset, tiles);
        sr.sprite = GetSpriteFrom(tx);
    }


    Texture2D BuildTileset(Sprite tileset, List<Sprite> tiles)
    {
        Texture2D textureSet = GetTextureFrom(tileset);

        GetComponent<SpriteRenderer>().material.mainTexture = textureSet;

        Vector2 size = GetDimensionInPX(tileset);
        int count = 0;
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                Texture2D texture = GetTextureFrom(tiles[count]);
                print(count + ", " + tiles[count].name);
                count++;

                for (int y = 0; y < texture.height; y++)
                {
                    for (int x = 0; x < texture.width; x++)
                    {
                        Color color = texture.GetPixel(x, y);
                        int xx = (x + i * texture.width);
                        int yy = textureSet.height - texture.height + y - j * texture.height;
                        textureSet.SetPixel(xx, yy, color);
                    }
                }
            }
        }

        textureSet.Apply();
        return textureSet;

    }

    Sprite GetSpriteFrom(Texture2D texture)
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100.0f);
        return sprite;
    }

    Texture2D GetTextureFrom(Sprite sprite)
    {
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

    private Vector2 GetDimensionInPX(GameObject obj)
    {
        Vector2 dimension;

        dimension.x = obj.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        dimension.y = obj.GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        return dimension;
    }

    private Vector2 GetDimensionInPX(Sprite sprite)
    {
        Vector2 dimension;

        dimension.x = sprite.bounds.size.x;
        dimension.y = sprite.bounds.size.y;

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

