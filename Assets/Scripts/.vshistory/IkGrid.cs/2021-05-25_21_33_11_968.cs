using System.Collections.Generic;
using UnityEngine;

public class IkGrid : MonoBehaviour // Unity Script'leri icin, MonoBehaviour miras alinir. Her Unity script'inin turetildigi temel siniftir....
{                                   //...Bu sayede; Start,FixUpdate,Update gibi metotlar kullanilabilinir.
    public int columns = 0; // sutun
    public int rows = 0;    // satir
    public float square_offset = 0.0f; // karelerin pozisyonunu offset kadar kaydiririz

    public Vector2 start_position = new Vector2(0.0f, 0.0f);  // baslangic pozisyonu
    public float square_scale = 1.0f; // kare olcegi (boyut degildir, boyut = scale(x,y) * W,H)

    private List<GameObject> grid_squares_ = new List<GameObject>(); // Grid'in karelerinin listesi
    public GameObject grid_square; // Grid'in bir karesi icin degisken
    private int selected_grid_data = -1;

    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
            Debug.LogError("Bu GameObject'in GridSquare script'ine eklenmis olmasi gerekir!");

        CreateGrid();
        SetGridNumber(GameSettings.Instance.GetGameMode());
    }

    void Update()
    {

    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {
        //0, 1, 2, 3, 4, 5,
        //6, 7, 8, ..., 35

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {   // satir ve sutun sayilari kadar grid karesi olusturuluyor
                grid_squares_.Add(Instantiate(grid_square) as GameObject);
                grid_squares_[grid_squares_.Count - 1].transform.parent = this.transform;  // Bu(this) GameObject'i, bu script'i tutan nesnenin alt ogesi(child) olarak istantiate et.
                grid_squares_[grid_squares_.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale); // Grid'e eklenen karelerin scale'lerini olusturuyor
            }
        }
    }

    private void SetSquaresPosition()
    {
        var square_rect = grid_squares_[0].GetComponent<RectTransform>(); // ilk pozisyonumuz, sol en ustteki karemiz
        Vector2 offset = new Vector2();
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + square_offset;  // ilk offsetimiz(x, y)ilk karenin [boyutu(rect.(w,h)*localScale(x,y))]
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset; // + kodun basinda belirledigimiz offset kadar kaydi

        int column_number = 0; // karenin bulundugu...
        int row_number = 0;    // ...satir, sutun numarasi

        foreach (GameObject square in grid_squares_)
        {
            if (column_number + 1 > columns)
            {   // burada alt satira geciyor
                row_number++;
                column_number = 0;
            }

            var pos_x_offset = offset.x * column_number;  // bulundugu sutun sayisi kadar saga
            var pos_y_offset = offset.y * row_number; // bulundugu satir sayisi kadar asagi

            if (column_number == 5 && (row_number == 0 || row_number == 2 || row_number == 4))
                pos_x_offset += offset.x / 7; // Bu iki if; sonuc degerlerinin oldugu Grid'ler oldugu icin...
            if (row_number == 5 && (column_number == 0 || column_number == 2 || column_number == 4))
                pos_y_offset += offset.y / 7; // ...ekstra offset ekliyorum, ama cok uzaklasmamasi icin /7 var

            square.GetComponent<RectTransform>().anchoredPosition = // anchor'un pozisyonunu ayarliyor // bir onceki karenin pozisyonunda basliyor // aslinda (Shift+Anchor)  hareketi yapiyor
                new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset); // baslangicin; x'i offset kadar saga, y'si offset kadar asagi

            column_number++; // her kareyi hizaladiktan sonra bir sonraki sutuna geciyor
        }
    }

    private void SetGridNumber(string level)
    {
        var data = IkData.Instance.ik_game[level][0];

        SetGridSquareData(data);
    }

    private void SetGridSquareData(IkData.IkBoardData data)
    {
        for(int index = 0; index < grid_squares_.Count; index++)
        {
            grid_squares_[index].GetComponent<GridSquare>().SetNumber(data.unsolved_data[index]);
        }
    }
}