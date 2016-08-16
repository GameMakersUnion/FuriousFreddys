using UnityEngine;
using System.Collections;

public enum IngredientUnit { Spoon, Cup, Bowl, Piece }
      
[System.Serializable]
public class Ingredient : PropertyAttribute
{
    public string name;
    public int amount = 1;
    public IngredientUnit unit;
}
