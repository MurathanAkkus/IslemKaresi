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

    private (int, int) GetSquarePosition(int square_index)
    {
        int pos_row = -1, pos_col = -1;
        
        for(int row = 0; row < 6; row++)
        {
            for(int col = 0; col < 6; col++)
            {
                if(line_data[row, col] == square_index)
                {
                    pos_row = row;
                    pos_col = col;
                }
            }
        }
        return (pos_row, pos_col);
    }

    public int [] GetHorizontalLine(int square_index)
    {
        int[] line = new int[6];

        var square_position_row = GetSquarePosition(square_index).Item1;

        for(int index = 0; index < 6; index++)
        {
            line[index] = line_data[square_position_row, index];
        }
        return line;
    }
}
