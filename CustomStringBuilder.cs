using System;
using System.Linq;
using System.Text;

/// <summary>
///  Usage:
///  CustomStringBuilder sng = new CustomStringBuilder();
///   sng.StringToAppend1 = new[] { "dddd", "ddds","dddx","scdrf","erty" };
///   MessageBox.Show(sng.AppendAll());
///
///
/// </summary>
public class CustomStringBuilder
{
    private string[] StringToAppend;

    private int counter = 0;

    public string[] StringToAppend1
    {
        get
        {
            return StringToAppend;
        }

        set
        {
            StringToAppend = value;
        }
    }

    public String AppendAll(params string[] items)
    {
        StringBuilder stringbuilder = new StringBuilder();

        foreach (var item in StringToAppend)
        {
            counter++;

            if (counter == StringToAppend.Count()) { stringbuilder.Append(item); }

            else if (item == "username" || item == "USERNAME" || item == "password" || item == "PASSWORD"||item == "md5"||item == "url" ||item == "lang")
            {
                stringbuilder.Append(item + "=");
            }

            else { stringbuilder.Append(item + "&"); }
        }

        return stringbuilder.ToString();
    }
}