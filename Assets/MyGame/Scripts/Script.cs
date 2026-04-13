using UnityEngine;
using System.IO;

public class Script : MonoBehaviour
{
    private void Start() {
        PlayerPrefs.SetInt("", 2);

        PlayerPrefs.Save();

        PlayerPrefs.GetInt("Du Gielier");

        PlayerPrefs.DeleteKey("Du Gielier");
   }

private void func() {
        string path = @"D:\documents\Schule\4BHITS\ITP\ForWhomTheChairRolls\Assets\MyGame\Scripts\ResetPlayer.cs";
        string csv = "HEY, Brother\n";
        csv +="Hello, Jassi\n";
        File.AppendAllText(path, csv);
    }

    private void fun2() {
        string path = @"D:\documents\Schule\4BHITS\ITP\ForWhomTheChairRolls\Assets\MyGame\Scripts\ResetPlayer.cs";
        string[] funny = File.ReadAllLines(path);
    }
}
