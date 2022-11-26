using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineIndicator : MonoBehaviour
{
    public static LineIndicator instance;

    private int[,] line_data = new int[6, 6]
    {
        { 0, 1, 2, 3, 4, 5 },
        { 6, 7, 8, 9, 10, 11 },
        { 12, 13, 14, 15, 16, 17},
        { 18, 19, 20, 21, 22, 23 },
        { 24, 25, 26, 27, 28, 29 },
        { 30, 31, 32, 33, 34, 35 }
    };

    private int[] line_data_falt = new int[36]
    {
        0, 1, 2, 3, 4, 5,
        6, 7, 8, 9, 10, 11,
        12, 13, 14, 15, 16, 17,
        18, 19, 20, 21, 22, 23,
        24, 25, 26, 27, 28, 29,
        30, 31, 32, 33, 34, 35
    };

    private int[,] square_data = new int[6, 6]
    {
        { 0, 1, 2, 3, 4, 5 },
        { 6, 7, 8, 9, 10, 11 },
        { 12, 13, 14, 15, 16, 17},
        { 18, 19, 20, 21, 22, 23 },
        { 24, 25, 26, 27, 28, 29 },
        { 30, 31, 32, 33, 34, 35 }
    };

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private (int, int) GetSquarePosition(int square_iindex)
    {
        int pos_row = -1, pos_col = -1;
        
    }
}
