using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public Fruit fruitPrefab;

    public List<Fruit> fruitList;

    public List<FruitTypeColor> fruitTypeColors;

    public Board board;


    public void ClearFruitList()
    {
        for (int i = 0; i < fruitList.Count; i++)
        {
            fruitList[i].cell.body = null;
            Destroy(fruitList[i].gameObject);
        }
        fruitList.Clear();
    }

    public void SpawnFruit(FruitType type)
    {
        Fruit fruit = Instantiate(fruitPrefab, transform);

        fruit.SetType(type, fruitTypeColors[(int)type].fruitColor);

        BoardCell cell = board.GetRamdomEmptyCell();
        fruit.SetCell(cell);

        fruitList.Add(fruit);
    }

    public void RemoveFruit(int index)
    {
        Fruit fruit = fruitList[index];
        fruitList.Remove(fruit);
        Destroy(fruit.gameObject);
    }
}
