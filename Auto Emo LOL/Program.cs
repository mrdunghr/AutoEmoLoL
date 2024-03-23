using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Auto_Emo_LOL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // tìm kiếm thư mục game bằng cách xem regedit - để chắc chắn rằng game đã cài trên máy hay chưa, cách này tối ưu rất ngon
            Object reg = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\Riot Game league_of_legends.live", "InstallLocation", null);
            string path = reg + "/Config/PersistedSettings.json";

            // test đọc all file json
            string jsonContent = File.ReadAllText(path);
            //Console.WriteLine(jsonContent);

            // chuyển json thành object
            JObject jsonObject = JObject.Parse(jsonContent);
            //Console.WriteLine(jsonObject);

            AutoChampMastery(jsonObject, path, false);

            AutoEmoteLaugh(jsonObject, path, true);

        }

        public static void AutoChampMastery(JObject jsonObject, string path, bool status)
        {
            // tìm đến đối tượng có tên "evtChampMasteryDisplay" - thông thạo tướng
            JToken targetSetting = jsonObject.SelectToken("$.files[?(@.name == 'Input.ini')].sections[?(@.name == 'GameEvents')].settings[?(@.name == 'evtChampMasteryDisplay')]");
            Console.WriteLine("OLD");
            Console.WriteLine(targetSetting);

            // kiểm tra xem có tìm thấy đối tượng không
            if (targetSetting != null)
            {
                // thay đổi giá trị của trường "value"
                string value = status ? "[q], [w], [e], [r], [a], [s], [d], [f]" : "[Ctrl][6]";
                targetSetting["value"] = value;
            }
            Console.WriteLine("NEW");
            Console.WriteLine(targetSetting);

            // Lưu lại các thay đổi vào file JSON
            File.WriteAllText(path, jsonObject.ToString());
        }
        public static void AutoEmoteLaugh(JObject jsonObject, string path, bool status)
        {
            // tìm đến đối tượng có tên "evtChampMasteryDisplay" - thông thạo tướng
            JToken targetSetting = jsonObject.SelectToken("$.files[?(@.name == 'Input.ini')].sections[?(@.name == 'GameEvents')].settings[?(@.name == 'evtEmoteLaugh')]");
            Console.WriteLine("OLD");
            Console.WriteLine(targetSetting);

            // kiểm tra xem có tìm thấy đối tượng không
            if (targetSetting != null)
            {
                // thay đổi giá trị của trường "value"
                string value = status ? "[q], [w], [e], [r], [s], [d], [f]" : "[Ctrl][4]";
                targetSetting["value"] = value;
            }
            Console.WriteLine("NEW");
            Console.WriteLine(targetSetting);

            // Lưu lại các thay đổi vào file JSON
            File.WriteAllText(path, jsonObject.ToString());
        }
    }
}
