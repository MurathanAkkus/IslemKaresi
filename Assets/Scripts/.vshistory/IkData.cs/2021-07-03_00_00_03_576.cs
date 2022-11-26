using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class IkEasyData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(List<string> solved)
    {
        Debug.Log("Degisti Bak yine!!");
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        string[] unsolved = IkData.SetData(solved, "Easy");

        data.Add(new IkData.IkBoardData(solved.ToArray(), unsolved));

        return data;
    }

    public static List<IkData.IkBoardData> getData(string[] solved, string[] unsolved)
    {
        Debug.Log("Yeni olusmadan!");
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(solved, unsolved));

        return data;
    }
}

public class IkMediumData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(List<string> solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        string[] unsolved = IkData.SetData(solved, "Medium");

        data.Add(new IkData.IkBoardData(solved.ToArray(), unsolved));

        return data;
    }

    public static List<IkData.IkBoardData> getData(string[] solved, string[] unsolved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(solved, unsolved));

        return data;
    }

    internal static List<IkData.IkBoardData> getData()
    {
        throw new NotImplementedException();
    }
}

public class IkHardData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(List<string> solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        string[] unsolved = IkData.SetData(solved, "Hard");

        data.Add(new IkData.IkBoardData(solved.ToArray(), unsolved));

        return data;
    }

    public static List<IkData.IkBoardData> getData(string[] solved, string[] unsolved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(solved, unsolved));

        return data;
    }

    internal static List<IkData.IkBoardData> getData()
    {
        throw new NotImplementedException();
    }
}

public class IkVeryHardData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(List<string> solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        string[] unsolved = IkData.SetData(solved, "VeryHard");

        data.Add(new IkData.IkBoardData(solved.ToArray(), unsolved));

        return data;
    }

    public static List<IkData.IkBoardData> getData(string[] solved, string[] unsolved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(solved, unsolved));

        return data;
    }

    internal static List<IkData.IkBoardData> getData()
    {
        throw new NotImplementedException();
    }
}

public class IkDefaultData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(string[] solved, string[] unsolved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(solved, unsolved));

        return data;
    }
}

public class IkData : MonoBehaviour
{
    public static IkData Instance;

    public struct IkBoardData
    {
        public string[] solved_data;
        public string[] unsolved_data;

        public IkBoardData(string[] solved, string[] unsolved)
        {
            solved_data = solved;
            unsolved_data = unsolved;
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
        if (!GameSettings.Instance.GetExitAfterWon())
        {
            ik_game.Add("Easy", IkEasyData.getData(SetGridValues()));
            ik_game.Add("Medium", IkMediumData.getData(SetGridValues()));
            ik_game.Add("Hard", IkHardData.getData(SetGridValues()));
            ik_game.Add("VeryHard", IkVeryHardData.getData(SetGridValues()));
        }
    }

    private static int idx, rand;

    private string str, num_opt;

    private List<string> SetGridValues()
    {
        List<string> solved = new List<string>();
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
        int CheckDiv()
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
                    solved[x_lnum] = SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = solved[x_lnum];
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
                    solved[y_lnum - 6] = SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = solved[y_lnum - 6];
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
                    solved[y_lnum - 6] = SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = solved[y_lnum - 6];
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
                    solved[x_lnum] = SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = solved[x_lnum];
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
                    solved[x_lnum] = SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = solved[x_lnum]; // burada -1 in sebebi; bulundugumuz index sayýnýn indexi
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
                    solved[y_lnum - 6] = SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = solved[y_lnum - 6];
                    solved[y_lnum - 6] = y_array[column_number, row_number - 1];
                    return numbers[number];
                }
            }
            else
            {   // x ve y'deki bolme islemi kesisti! hem x'de 1 var mi diye hem de y'de 1 var mi diye kontrol ediyor
                dividing = int.Parse(solved[y_lnum - 12]);
                //bolme isleminden önceki bolunen sayi: y icin
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    number = numbers[number];
                    solved[y_lnum - 6] = SetOpt_withoutDiv();
                    y_array[column_number, row_number - 1] = solved[y_lnum - 6];
                    solved[y_lnum - 6] = y_array[column_number, row_number - 1];
                }

                dividing = int.Parse(solved[x_lnum - 1]);
                //bolme isleminden önceki bolunen sayi: x icin
                if (dividing == 1)
                {
                    number = Random.Range(0, max_);
                    number = numbers[number];
                    solved[x_lnum] = SetOpt_withoutDiv();
                    x_array[row_number, column_number - 1] = solved[x_lnum]; // burada -1 in sebebi; bulundugumuz index sayýnýn indexi
                    solved[x_lnum] = x_array[row_number, column_number - 1];                                                                        // 1 gerisinde opt var
                }
            }
            return CheckDiv();
        }


        for (int i = 0; i < 36; i++)
        {
            if (column_number == 5 && row_number != 5)
            {
                if (row_number == 0 || row_number == 2 || row_number == 4)
                    solved.Add(Evaluate(Trans(x_array, row_number)));   // x'deki islemlerin sonucu
                else
                    solved.Add("_"); // deaktif olacak

                column_number = 0;  // Alt satira geciyor
                row_number++;
                continue;
            }
            else if (row_number == 5)
            {
                if (column_number == 0 || column_number == 2 || column_number == 4)
                    solved.Add(Evaluate(Trans(y_array, column_number)));
                else
                    solved.Add("_");    // deaktif olacak
            }
            else
            {
                if (column_number != 1 && column_number != 3)
                {
                    if (row_number != 1 && row_number != 3)
                    {
                        y_lnum = i;

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
                        num_opt = SetOpt(); // burada, 4 islem operatorleri atiyor
                }
                else
                {
                    if (row_number != 1 && row_number != 3)
                    {
                        num_opt = SetOpt(); // burada, 4 islem operatorleri atiyor
                        if (num_opt.Equals("/"))
                        {
                            div_ = 'x'; // bu kareye bolme opt atiyorsa true
                            x_lnum = i;
                        }
                    }
                    else
                        num_opt = "_"; // burada kare olusmasi gerek yok
                }
                solved.Add(num_opt);
            }
            if (solved[i] != "_")
            {   // kare aktif ise
                x_array[row_number, column_number] += num_opt;
                y_array[column_number, row_number] += num_opt;
            }
            column_number++;
        }
        return solved;
    }

    private string SetOpt()
    {
        string[] opt = { "+", "-", "*", "/" };
        int number = Random.Range(0, 4);
        return opt[number];
    }

    private string SetOpt_withoutDiv()
    {
        string[] opt = { "+", "-", "*" };
        int number = Random.Range(0, 3);
        return opt[number];
    }

    private static string Evaluate(string expression)
    {
        var loDataTable = new DataTable();
        var loDataColumn = new DataColumn("Eval", typeof(double), expression);
        loDataTable.Columns.Add(loDataColumn);
        loDataTable.Rows.Add(0);
        return Convert.ToString(loDataTable.Rows[0]["Eval"]);
    }

    public static string[] SetData(List<string> solved, string mode)
    {
        string[] unsolved = solved.ToArray();
        int out_, i = 0;
        for (; i < 29; i++)
        {
            if (i == 5 || i == 17)
                continue;
            if (Int32.TryParse(solved[i], out out_))
                if (out_ >= 1 && out_ <= 9)
                    unsolved[i] = " ";
        }

        List<string> numbers = new List<string>(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
        i = 0;

        if (mode.Equals("Easy"))
            i = 3;  // 3 ipucu verecek

        else if (mode.Equals("Medium"))
            i = 2;  // 2 ipucu verecek

        else if (mode.Equals("Hard"))
            i = 1;  // 1 ipucu verecek
        // i = 0 geliyorsa, VeryHard modda oynuyor demektir, ipucu verilmeyecek
        if (i != 0)
        {
            string str;
            do
            {
                rand = Random.Range(0, numbers.Count - 1);
                str = numbers[rand];
                for (int j = 0; j < 28; j++)
                    if (solved[j].Equals(str) && unsolved[j].Equals(" "))
                        idx = j;    // rastgele üretilen index'teki sayinin tablodaki yerini bulup, onun index'ini aliyor.

                numbers.Remove(str);
                unsolved[idx] = solved[idx]; // Oradaki index'teki sayiyi aciyor kullaniciya
                i--;
            } while (i > 0);
        }
        return unsolved;
    }
}