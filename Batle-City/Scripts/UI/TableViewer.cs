using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TableViewer : MonoBehaviour
{
    public Text t;
    void Start()
    {
        var Table = Session.ReadFromFile();
        StringBuilder str = new StringBuilder();
        int i = 1;
        str.Append("    First  Second  Time \n");
        foreach (var ses in Table)
        {
            if(ses != null)
            str.Append($"{i,2}."+" "+ses);
            i++;
        }
        t.text = str.ToString();
    }
}
