using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkGrid : MonoBehaviour // Unity Script'leri icin, MonoBehaviour miras alinir. Her Unity script'inin turetildigi temel siniftir....
{                                   //...Bu sayede; Start,FixUpdate,Update gibi metotlar kullanilabilinir.
    public int columns = 0; // sutun
    public int rows = 0;    // satir
    public float square_offset = 0.0f; // karelerin pozisyonunu offset kadar kaydiririz

    public Vector2 start_position = new Vector2(0.0f, 0.0f);  // baslangic pozisyonu
    public float square_scale = 1.0f; // kare olcegi (boyut = scale(x,y) * W,H)
    public Color line_highlight_color = Color.red;

    public List<GameObject> grid_squares_ = new List<GameObject>(); // Grid'in karelerinin listesi
    public Button pause_button;
    public GameObject pb;
    public GameObject grid_square; // Grid'in bir karesi icin degisken
    private int selected_grid_data = -1;
    public Color[] grid_squares_color = new Color[36];
    public Color opt_col = new Color32(200, 227, 208, 255);
    public Color res_col = new Color32(200, 245, 215, 200);

    
    //Button button_ = pau
    public static IkGrid Instance;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
            Debug.LogError("Bu GameObject'in GridSquare script'ine eklenmis olmasi gerekir!");

        CreateGrid();

        if (GameSettings.Instance.GetContinuePreviousGame())
        {
            Debug.Log("Save'den devam ediyor!");
            SetGridFormFile();
        }
        else
        {
            Debug.Log("Yeni oyun!");
            SetGridNumber(GameSettings.Instance.GetGameMode());
        }

        SetAllSqaureColor();
        AdManager.Instance.ShowBanner();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pause_button.
    }

    private void SetAllSqaureColor()
    {
        int i;

        foreach (var square in grid_squares_)
        {
            var comp = square.GetComponent<GridSquare>();
            i = grid_squares_.IndexOf(square);
            if (comp.IsOpt_Square())
                comp.SetSquareColor(opt_col);
            else if (comp.IsRes_Square())
            {
                comp.SetSquareSizeUp(0.1f, square);
                comp.SetSquareColor(res_col);
            }
          
            grid_squares_color[i] = comp.GetSquareColor();
        }
    }

    void SetGridFormFile()
    {
        selected_grid_data = Config.ReadGameBoardLevel();
        var data = Config.ReadGridData();

        SetGridSquareData(data);
        SetGridNotes(Config.GetGridNotes());
    }

    private void SetGridNotes(Dictionary<int, List<int>> notes)
    {
        foreach (var note in notes)
            grid_squares_[note.Key].GetComponent<GridSquare>().SetGridNotes(note.Value);
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
        int square_index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {   // satir ve sutun sayilari kadar grid karesi olusturuluyor
                grid_squares_.Add(Instantiate(grid_square) as GameObject);
                grid_squares_[grid_squares_.Count - 1].GetComponent<GridSquare>().SetSquareIndex(square_index);
                grid_squares_[grid_squares_.Count - 1].transform.parent = this.transform;  // Bu(this) GameObject'i, bu script'i tutan nesnenin alt ogesi(child) olarak istantiate et.
                grid_squares_[grid_squares_.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale); // Grid'e eklenen karelerin scale'lerini olusturuyor

                square_index++;
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
            square.GetComponent<GridSquare>().SetSquareIndex(grid_squares_.IndexOf(square));
            if (column_number + 1 > columns)
            {   // burada alt satira geciyor
                row_number++;
                column_number = 0;
            }

            var pos_x_offset = offset.x * column_number;  // bulundugu sutun sayisi kadar saga
            var pos_y_offset = offset.y * row_number; // bulundugu satir sayisi kadar asagi

            if (column_number == 5 && (row_number == 0 || row_number == 2 || row_number == 4))
                pos_x_offset += offset.x / 10; // Bu iki if; sonuc degerlerinin oldugu Grid'ler oldugu icin...
            if (row_number == 5 && (column_number == 0 || column_number == 2 || column_number == 4))
                pos_y_offset += offset.y / 10; // ...ekstra offset ekliyorum, ama cok uzaklasmamasi icin /7 var

            square.GetComponent<RectTransform>().anchoredPosition = // anchor'un pozisyonunu ayarliyor // bir onceki karenin pozisyonunda basliyor // aslinda (Shift+Anchor)  hareketi yapiyor
                new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset); // baslangicin; x'i offset kadar saga, y'si offset kadar asagi

            column_number++; // her kareyi hizaladiktan sonra bir sonraki sutuna geciyor
        }
    }

    private void SetGridNumber(string level)
    {
        selected_grid_data = Random.Range(0, IkData.Instance.ik_game[level].Count);
        var data = IkData.Instance.ik_game[level][selected_grid_data];
        SetGridSquareData(data);
    }

    private void SetGridSquareData(IkData.IkBoardData data)
    {
        int index;
        foreach (var square in grid_squares_)
        {
            index = grid_squares_.IndexOf(square);
            square.GetComponent<GridSquare>().SetHasDefaultValue(data.unsolved_data[index] != " " && data.unsolved_data[index] == data.solved_data[index]);
            square.GetComponent<GridSquare>().SetNumber(data.unsolved_data[index], square);
            square.GetComponent<GridSquare>().SetCorrectNumber(data.solved_data[index]);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnSquareSelected += OnSquareSelected;
        GameEvents.OnCheckBoardCompleted += CheckBoardCompleted;
        GameEvents.OnGiveAHint += GiveAHint;
    }

    private void OnDisable()
    {
        GameEvents.OnSquareSelected -= OnSquareSelected;
        GameEvents.OnCheckBoardCompleted -= CheckBoardCompleted;
        GameEvents.OnGiveAHint -= GiveAHint;

        //***************************************
        string[] solved_data = new string[36];
        string[] unsolved_data = new string[36];
        Dictionary<string, List<string>> grid_notes = new Dictionary<string, List<string>>();

        for (int i = 0; i < grid_squares_.Count; i++)
        {
            var comp = grid_squares_[i].GetComponent<GridSquare>();
            unsolved_data[i] = comp.GetSquareNumber();
            solved_data[i] = comp.GetSquareCorrectNumber();
            string key = "square_note:" + i.ToString();
            grid_notes.Add(key, comp.GetSquareNotes());
        }
        IkData.IkBoardData current_game_data = new IkData.IkBoardData(solved_data, unsolved_data);

        if (!GameSettings.Instance.GetExitAfterWon()) // board tamamlandiktan sonra cikarken verileri kaydetme
            Config.SaveBoardData(current_game_data,
                GameSettings.Instance.GetGameMode(),
                selected_grid_data,
                Lives.instance.GetErrorNumbers(),
                grid_notes);
        else
            Config.DeleteDataFile();

        AdManager.Instance.HideBanner();
    }

    private void SetOptColor()
    {
        foreach (var square in grid_squares_)
        {
            var comp = square.GetComponent<GridSquare>();
            if (comp.IsOpt_Square())
                comp.SetSquareColor(opt_col);
            if (comp.IsRes_Square())
                comp.SetSquareColor(res_col);
        }
    }

    private void GiveAHint()
    {
        var squareIndexes = new List<int>();
        GridSquare comp;
        for (int index = 0; index < grid_squares_.Count; index++)
        {
            comp = grid_squares_[index].GetComponent<GridSquare>();
            if (comp.GetSquareNumber() == " " && !comp.GetHasDefaultValue())
                squareIndexes.Add(index);
        }

        //Hic bos kare kalmamissa
        if (squareIndexes.Count == 0)
            return;

        var random_index = Random.Range(0, squareIndexes.Count);
        var square_index = squareIndexes[random_index];
        comp = grid_squares_[square_index].GetComponent<GridSquare>();
        comp.SetCorrectValueOnHint();
        comp.SetHasDefaultValue(true);
        comp.number_text.GetComponent<Text>().fontStyle = FontStyle.Bold;
    }

    private void SetSquaresColor(int[] data, Color col)
    {
        foreach (var index in data)
        {
            var comp = grid_squares_[index].GetComponent<GridSquare>();
            if (!comp.HasWrongValue()) //&& !comp.IsSelected() && !comp.IsOpt_Square()
                comp.SetSquareColor(col);
        }
    }

    public void OnSquareSelected(int square_index)
    {
        var horizontal_line = LineIndicator.instance.GetHorizontalLine(square_index);
        var vertical_line = LineIndicator.instance.GetVerticalLine(square_index);
        GridSquare comp = grid_squares_[square_index].GetComponent<GridSquare>();

        if (!grid_squares_[square_index].GetComponent<GridSquare>().GetHasDefaultValue())
        {
            SetSquaresColor(LineIndicator.instance.GetAllSquaresIndexes(), Color.white);
            SetOptColor();
            SetSquaresColor(horizontal_line, line_highlight_color);
            SetSquaresColor(vertical_line, line_highlight_color);
            
            if (!comp.IsOpt_Square() && !comp.HasWrongValue() 
                && !comp.GetHasDefaultValue() && !comp.IsRes_Square())
                comp.SetSquareColor(Color.white);
        }
        else
        {
            foreach (var gridSquare in grid_squares_)
            {
                comp = gridSquare.GetComponent<GridSquare>();
                if (!comp.HasWrongValue() && !comp.IsOpt_Square() && !comp.IsRes_Square()) //&& !comp.IsSelected()
                    comp.SetSquareColor(Color.white);
                else if (comp.IsOpt_Square())
                    comp.SetSquareColor(opt_col);
                else if (comp.IsRes_Square())
                    comp.SetSquareColor(res_col);
            }
        }
    }

    private void CheckBoardCompleted()
    {
        foreach (var square in grid_squares_)
        {
            var comp = square.GetComponent<GridSquare>();
            if (!comp.IsCorrectNumberSet())
                return;
        }

        GameEvents.OnBoardCompletedMethod();
    }
}