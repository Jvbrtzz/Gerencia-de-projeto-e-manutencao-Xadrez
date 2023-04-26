using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private int squareSize = 4;
    [SerializeField] private GameObject square;
    [SerializeField] private Dictionary<int, Square> squareGrid;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int e = 0; e < 8; e++)
            {
                Vector3 pos = new Vector3(i * squareSize, square.transform.position.y, e * squareSize);
                var sq = Instantiate(square, this.transform);
                sq.transform.localPosition = pos;
                sq.name = GetAlphabetLetter(i) + (e + 1).ToString() ;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    char GetAlphabetLetter(int pos)
    {
        string alphabet = "ABCDEFGHIJKLMNOPQR";
        return alphabet[pos];
    }
}

public class Square
{

}
