using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkEasyData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(string[] solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        data.Add(new IkData.IkBoardData(solved, "Easy"));

        return data;
    }
}

public class IkMediumData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(string[] solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(
            new int[36]
            ));
        return data;
    }
}

public class IkHardData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(string[] solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(
            new int[36], new int[36]
            ));
        return data;
    }
}

public class IkVeryHardData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(string[] solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(
            new int[36], new int[36]
            ));
        return data;
    }
}

public class IkData : MonoBehaviour
{
    public static IkData Instance;
    private List<GameObject> grid_squares_ = new List<GameObject>(); // Grid'in karelerinin listesi

    string str, num_opt;

    public struct IkBoardData
    {
        public string[] unsolved_data;
        public string[] solved_data;

        public IkBoardData(string[] solved, string mode) : this()
        {
            List<string>
            int i = 0, rand;
            solved_data = solved;
            if(mode.Equals("Easy"))
            {
                do
                {
                    rand = Random.Range(0,9);
                    unsolved_data[rand] = solved[rand];
                    i++;
                } while (i != 3);
            }
            else if (mode.Equals("Medium"))
            {
                do
                {
                    rand = Random.Range(0, 9);
                    unsolved_data[rand] = solved[rand];
                    i++;
                } while (i != 2);
            }
            else if (mode.Equals("Hard"))
            {
                do
                {
                    rand = Random.Range(0, 9);
                    unsolved_data[rand] = solved[rand];
                    i++;
                } while (i != 2);
            }
            else
            {

            }

        }
    }

    public Dictionary<string, List<IkBoardData>> ik_game = new Dictionary<string, List<IkBoardData>>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        ik_game.Add("Easy", IkEasyData.getData(Solved()));
        ik_game.Add("Medium", IkMediumData.getData(Solved()));
        ik_game.Add("Hard", IkHardData.getData(Solved()));
        ik_game.Add("VeryHard", IkVeryHardData.getData(Solved()));
    }

    public string[] Solved()
    {
        int column_number = 0, row_number = 0; // column_number sutun, y satir kordinati
        int x_lnum = 0;
        bool divisible = false;

        string[,] x_array = new string[6, 6];
        string[,] y_array = new string[6, 6];
        
        int number;
        char div_ = ' ';
        int divisive;

        int max_ = 9;     // listenin maksimum boyutu
        int dividing;   // bolunen rakam

        List<int> numbers = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        string[] solved = new string[36];

        string Trans(string[,] array, int index)
        {   // 2 boyutlu stringi alip, str'ye atiyor
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
                    grid_squares_[x_lnum].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = grid_squares_[x_lnum].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                }
                else
                {   // bolunebilir sayi varsa
                    divisive = Random.Range(0, max); // bu fonksiyonda olusturulan dvsv isimli listeden...
                    divisive = dvsv[divisive];       // ...bolebilir sayilardan birini seciyor rastgele
                    divisible = true;
                }
            }
            else if (div_.Equals('y'))
            {
                if (max == 0)
                {   // bolebilen sayi yoksa
                    divisive = Random.Range(0, max_);
                    grid_squares_[y_lnum - 6].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = grid_squares_[y_lnum - 6].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                }
                else
                {   // bolunebilir sayi varsa
                    divisive = Random.Range(0, max);    // bu fonksiyonda olusturulan dvsv isimli listeden...
                    divisive = dvsv[divisive];          // ...bolebilir sayilardan birini seciyor rastgele
                    divisible = true;
                }
            }
            else
            {   // x ve y'den bolme islemi geldiyse, x'dekiler gibi olacak ve y'deki bolme islemi degisecek
                dividing = int.Parse(grid_squares_[y_lnum - 12].GetComponent<GridSquare>().number_text.GetComponent<Text>().text);
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
                }
                else
                {   // hem x hem y bolunebilir sayi var
                    divisive = Random.Range(0, max_z); // bu fonksiyonda olusturulan dvsv isimli listeden...
                    divisive = dvsv_z[divisive];       // ...bolebilir sayilardan birini seciyor rastgele 
                    divisible = true;
                    return divisive;
                }

                if (max == 0)
                {   // bolebilen sayi yok
                    divisive = Random.Range(0, max_);
                    grid_squares_[x_lnum].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = grid_squares_[x_lnum].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                }
                else
                {   // bolunebilir sayi var
                    divisive = Random.Range(0, max); // bu fonksiyonda olusturulan dvsv isimli listeden...
                    divisive = dvsv[divisive];       // ...bolebilir sayilardan birini seciyor rastgele 
                    divisible = true;
                }
            }
            return divisive;
        }

        int Division(char div_)
        {

            if (div_ == 'x')
            {
                dividing = int.Parse(grid_squares_[x_lnum - 1].GetComponent<GridSquare>().number_text.GetComponent<Text>().text);
                //bolme isleminden önceki bolunen sayi
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    grid_squares_[x_lnum].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = grid_squares_[x_lnum].GetComponent<GridSquare>().number_text.GetComponent<Text>().text; // burada -1 in sebebi; bulundugumuz index sayýnýn indexi
                    return number;                                                                                                                 // 1 gerisinde opt var
                }
            }
            else if (div_ == 'y')
            {
                dividing = int.Parse(grid_squares_[y_lnum - 12].GetComponent<GridSquare>().number_text.GetComponent<Text>().text);
                //bolme isleminden önceki bolunen sayi
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    grid_squares_[y_lnum - 6].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = grid_squares_[y_lnum - 6].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                    return number;
                }
            }
            else
            {   // x ve y'deki bolme islemi kesisti! hem x'de 1 var mi diye hem de y'de 1 var mi diye kontrol ediyor
                dividing = int.Parse(grid_squares_[y_lnum - 12].GetComponent<GridSquare>().number_text.GetComponent<Text>().text);
                //bolme isleminden önceki bolunen sayi: y icin
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    grid_squares_[y_lnum - 6].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = grid_squares_[y_lnum - 6].GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                }

                dividing = int.Parse(grid_squares_[x_lnum - 1].GetComponent<GridSquare>().number_text.GetComponent<Text>().text);
                //bolme isleminden önceki bolunen sayi: x icin
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    grid_squares_[x_lnum].GetComponent<GridSquare>().SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = grid_squares_[x_lnum].GetComponent<GridSquare>().number_text.GetComponent<Text>().text; // burada -1 in sebebi; bulundugumuz index sayýnýn indexi                                                                                                            // 1 gerisinde opt var
                }
            }
            return CheckDiv(div_);
        }

        foreach (var square in grid_squares_)
        {
            if (column_number == 5 && row_number != 5)
            {
                if (row_number == 0 || row_number == 2 || row_number == 4)
                {
                    square.GetComponent<Results>().Result(Trans(x_array, row_number));
                    str = "";
                }
                else
                    square.SetActive(false);

                column_number = 0; // Alt satira geciyor
                row_number++;
                continue;
            }
            else if (row_number == 5)
            {
                if (column_number == 0 || column_number == 2 || column_number == 4)
                {
                    square.GetComponent<Results>().Result(Trans(y_array, column_number));
                    str = "";
                }
                else if (column_number == 4)
                    break; // Burada column_number == 5 demedim, cunku yukaridaki if 5'teki kareyi deaktif yapti
                else
                    square.SetActive(false);
            }
            else
            {
                if (column_number != 1 && column_number != 3)
                {
                    if (row_number != 1 && row_number != 3)
                    {
                        y_lnum = grid_squares_.IndexOf(square);

                        if (y_lnum > 5 && grid_squares_[y_lnum - 6].GetComponent<GridSquare>().number_text.GetComponent<Text>().text == "/")
                        {
                            if (div_ == 'x')
                                div_ = 'z'; // burada x ve y'de bolme islemi var
                            else
                                div_ = 'y';
                        }

                        if (div_ == 'x' || div_ == 'y' || div_ == 'z')
                            number = Division(div_);
                        else
                            number = Random.Range(0, max_);

                        if (divisible)
                        {
                            solved[y_lnum] = number.ToString(); // Listeden rastgele bir rakam alindi
                            numbers.Remove(number); // number'i bulup, direkt element olarak siliyor
                        }
                        else
                        {   // Listeden rastgele bir rakam alindi
                            solved[y_lnum] = numbers[number].ToString();
                            numbers.RemoveAt(number); // number bir index numarasi oldugu icin, number index'indeki sayiyi siliyor
                        }
                        divisible = false;
                        div_ = ' ';
                        max_--;
                    }
                    else
                        square.GetComponent<GridSquare>().SetOpt(); // burada, 4 islem operatorleri atiyor
                }
                else
                {
                    if (row_number != 1 && row_number != 3)
                    {
                        square.GetComponent<GridSquare>().SetOpt(); // burada, 4 islem operatorleri atiyor
                        if (square.GetComponent<GridSquare>().number_text.GetComponent<Text>().text.Equals("/"))
                        {
                            div_ = 'x'; // bu kareye bolme opt atiyorsa true
                            x_lnum = grid_squares_.IndexOf(square);
                        }

                    }
                    else
                        square.SetActive(false); // burada kare olusmasi gerek yok
                }
            }
            if (square.active == true)
            {
                x_array[row_number, column_number] += square.GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                y_array[column_number, row_number] += square.GetComponent<GridSquare>().number_text.GetComponent<Text>().text;
                solved[y_lnum] = num_opt;
            }
            column_number++;
        }
        return solved;
    }
}