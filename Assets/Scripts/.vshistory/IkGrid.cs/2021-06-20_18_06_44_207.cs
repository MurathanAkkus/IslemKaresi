using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        string[] solved = SetGridValues().ToArray();
        SetGridSquareData(solved);
        //SetGridNumber(GameSettings.Instance.GetGameMode());
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
        string[] solved = SetGridValues().ToArray();
        SetGridSquareData(solved);
    }

    private void SetGridSquareData(IkData.IkBoardData data)
    {
        foreach (var square in grid_squares_)
        {
            int index = grid_squares_.IndexOf(square);
            square.GetComponent<GridSquare>().SetNumber(data.unsolved_data[index], square);
            square.GetComponent<GridSquare>().SetCorrectNumber(data.solved_data[index]);
            square.GetComponent<GridSquare>().SetHasDefaultValue(data.unsolved_data[index] != "0" && data.unsolved_data[index] == data.solved_data[index]);
        }
    }
    public void SetGridSquareData(string[] solved)
    {
        int i = 0;
        foreach (var square in grid_squares_)
        {
            square.GetComponent<GridSquare>().SetNumber(solved[i], square);
            i++;
        }
    }

    string str, num_opt;
    List<string> solved = new List<string>();

    public List<string> SetGridValues()
    {
        int column_number = 0, row_number = 0; // column_number sutun, y satir kordinati
        int x_lnum = 0;

        string[,] x_array = new string[6, 6];
        string[,] y_array = new string[6, 6];

        int number;
        char div_ = ' ';
        int divisive;

        int max_ = 9;     // listenin maksimum boyutu
        int dividing;   // bolunen rakam

        List<int> numbers = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        string Trans(string[,] array, int index)
        {   // 2 boyutlu stringi alip, str'ye atiyor
            str = null;
            for (int i = 0; i < 5; i++)
                str += array[index, i];
            return str;
        }
        
        int y_lnum;
        int CheckDiv(char div_)
        {   // Bu metodda; bolebilen sayilari bulup gonderiyor
            int max = 0;
            List<int> dvsv = new List<int>();
            for (int i = 0; i < max_; i++)
            {
                if (dividing % numbers[i] == 0)
                {
                    dvsv.Add(numbers[i]);
                    max++;
                }
            }
            if (div_.Equals('x'))
            {
                if (max == 0)
                {   // bolebilen sayi yoksa
                    divisive = Random.Range(0, max_);
                    divisive = numbers[divisive];
                    grid_squares_[x_lnum].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = grid_squares_[x_lnum].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                    solved[x_lnum] = x_array[row_number, column_number - 1];
                }
                else
                {   // bolunebilir sayi varsa
                    divisive = Random.Range(0, max); // bu fonksiyonda olusturulan dvsv isimli listeden...
                    divisive = dvsv[divisive];       // ...bolebilir sayilardan birini seciyor rastgele
                }
            }
            else if (div_.Equals('y'))
            {
                if (max == 0)
                {   // bolebilen sayi yoksa
                    divisive = Random.Range(0, max_);
                    divisive = numbers[divisive];
                    grid_squares_[y_lnum - 6].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = grid_squares_[y_lnum - 6].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                    solved[y_lnum - 6] = y_array[column_number, row_number - 1];
                }
                else
                {   // bolunebilir sayi varsa
                    divisive = Random.Range(0, max);    // bu fonksiyonda olusturulan dvsv isimli listeden...
                    divisive = dvsv[divisive];          // ...bolebilir sayilardan birini seciyor rastgele
                }
            }
            else
            {   // x ve y'den bolme islemi geldiyse, x'dekiler gibi olacak ve y'deki bolme islemi degisecek
                dividing = int.Parse(solved[y_lnum - 12]);
                int max_z = 0;
                List<int> dvsv_z = new List<int>();
                for (int i = 0; i < max; i++)
                {
                    if (dividing % dvsv[i] == 0)
                    {
                        dvsv_z.Add(dvsv[i]);
                        max_z++;
                    }
                }

                if (max_z == 0)
                {   // y'den bolebilen sayi yok
                    grid_squares_[y_lnum - 6].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = grid_squares_[y_lnum - 6].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                    solved[y_lnum - 6] = y_array[column_number, row_number - 1];
                }
                else
                {   // hem x hem y bolunebilir sayi var
                    divisive = Random.Range(0, max_z); // bu fonksiyonda olusturulan dvsv isimli listeden...
                    divisive = dvsv_z[divisive];       // ...bolebilir sayilardan birini seciyor rastgele 
                    return divisive;
                }

                if (max == 0)
                {   // bolebilen sayi yok
                    divisive = Random.Range(0, max_);
                    divisive = numbers[divisive];
                    grid_squares_[x_lnum].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = grid_squares_[x_lnum].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                    solved[x_lnum] = x_array[row_number, column_number - 1];
                }
                else
                {   // bolunebilir sayi var
                    divisive = Random.Range(0, max); // bu fonksiyonda olusturulan dvsv isimli listeden...
                    divisive = dvsv[divisive];       // ...bolebilir sayilardan birini seciyor rastgele 
                }
            }
            return divisive;
        }

        int Division(char div_)
        {
            if (div_ == 'x')
            {
                dividing = int.Parse(solved[x_lnum - 1]);
                //bolme isleminden önceki bolunen sayi
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    grid_squares_[x_lnum].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = grid_squares_[x_lnum].GetComponent<GridSquare>().number_text.GetComponent<Text>().text; // burada -1 in sebebi; bulundugumuz index sayýnýn indexi
                    solved[x_lnum] = x_array[row_number, column_number - 1];                                                                        // 1 gerisinde opt var
                    return numbers[number];                                                                                                                 
                }
            }
            else if (div_ == 'y')
            {
                dividing = int.Parse(solved[y_lnum - 12]);
                //bolme isleminden önceki bolunen sayi
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    grid_squares_[y_lnum - 6].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = grid_squares_[y_lnum - 6].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                    solved[y_lnum - 6] = y_array[column_number, row_number - 1];
                    return numbers[number];
                }
            }
            else
            {   // x ve y'deki bolme islemi kesisti! hem x'de 1 var mi diye hem de y'de 1 var mi diye kontrol ediyor
                dividing = int.Parse(grid_squares_[y_lnum - 12].GetComponent<GridSquare>().number_text.GetComponent<Text>().text);
                //bolme isleminden önceki bolunen sayi: y icin
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    number = numbers[number];
                    grid_squares_[y_lnum - 6].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = grid_squares_[y_lnum - 6].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                    solved[y_lnum - 6] = y_array[column_number, row_number - 1];
                }

                dividing = int.Parse(grid_squares_[x_lnum - 1].GetComponent<GridSquare>().number_text.GetComponent<Text>().text);
                //bolme isleminden önceki bolunen sayi: x icin
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    number = numbers[number];
                    grid_squares_[x_lnum].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = grid_squares_[x_lnum].GetComponent<GridSquare>().number_text.GetComponent<Text>().text; // burada -1 in sebebi; bulundugumuz index sayýnýn indexi
                    solved[x_lnum] = x_array[row_number, column_number - 1];                                                                        // 1 gerisinde opt var
                }
            }
            return CheckDiv(div_);
        }

        
        foreach (var square in grid_squares_)
        {
            if (column_number == 5 && row_number != 5)
            {
                if (row_number == 0 || row_number == 2 || row_number == 4)
                    solved.Add(square.GetComponent<Results>().Result(Trans(x_array, row_number)));
                else
                    solved.Add("-1");

                column_number = 0; // Alt satira geciyor
                row_number++;

                continue;
            }
            else if (row_number == 5)
            {
                if (column_number == 0 || column_number == 2 || column_number == 4)
                    solved.Add(square.GetComponent<Results>().Result(Trans(y_array, column_number)));
                else if (column_number == 4)
                    continue; // Burada column_number == 5 demedim, cunku yukaridaki if 5'teki kareyi deaktif yapti
                else
                    num_opt = "-1";
            }
            else
            {
                if (column_number != 1 && column_number != 3)
                {
                    if (row_number != 1 && row_number != 3)
                    {
                        y_lnum = grid_squares_.IndexOf(square);

                        if (y_lnum > 5 && solved[y_lnum - 6] == "/")
                        {
                            if (div_ == 'x')
                                div_ = 'z'; // burada x ve y'de bolme islemi var
                            else
                                div_ = 'y';
                        }

                        if (div_ == 'x' || div_ == 'y' || div_ == 'z')
                        {
                            number = Division(div_);
                            numbers.Remove(number);  // number'i bulup, direkt element olarak siliyo
                            num_opt = number.ToString();
                        }
                        else
                        {
                            number = Random.Range(0, max_); // Listeden rastgele bir rakam alindi
                            num_opt = numbers[number].ToString();
                            numbers.RemoveAt(number); // number bir index numarasi oldugu icin, number index'indeki sayiyi siliyor
                        }
                        
                        div_ = ' ';
                        max_--;
                    }
                    else
                        num_opt = square.GetComponent<GridSquare>().SetOpt(); // burada, 4 islem operatorleri atiyor
                }
                else
                {
                    if (row_number != 1 && row_number != 3)
                    {
                        num_opt = square.GetComponent<GridSquare>().SetOpt(); // burada, 4 islem operatorleri atiyor
                        if (num_opt.Equals("/"))
                        {
                            div_ = 'x'; // bu kareye bolme opt atiyorsa true
                            x_lnum = grid_squares_.IndexOf(square);
                        }
                    }
                    else
                        num_opt = "-1"; // burada kare olusmasi gerek yok
                }
            }
            if (square.active == true)
            {
                x_array[row_number, column_number] += num_opt;
                y_array[column_number, row_number] += num_opt;
            }
            column_number++;
            solved.Add(num_opt);
            Debug.Log(num_opt);
        }
        return solved;
    }
}