using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace check_date
{
    public class BasesInfo
    {
        public string date { get; set; }
        public string path { get; set; }
        public BasesInfo(string date, string path)
        {
            this.date = date;
            this.path = path;
        }
        public void DelFolder() 
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Console.WriteLine("Папка " + path + " успешно удалена.");
            }
            else
            {
                // сделать удаление инфы из файла о БД которой уже нету
                Console.WriteLine("Папка " + path + " не существует.");
            }
        }
        public string CheckDate()
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime dateNow;

            if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateNow))
            {
      // если паст или реал то удаляем
                if (dateNow < currentDate)
                {
                    return "past";
                }
                else if (dateNow > currentDate)
                {
                    return "future";
                }
                else
                {
                    return "real";
                }
            }
            else
            {
                return null;
            }
        }
        public static List<BasesInfo> getBasesInfoList(string path)
        {
            List<BasesInfo> listBase = new List<BasesInfo>();
            if (!File.Exists(path)) // проверка наличия файла списка
            {
                return null;
            }

            StreamReader sr_BasesList = new StreamReader(path); // чтение файла списка
            string namebase, pathbase, datedel = "";

            // парсинг файла списка
            Dictionary<string, List<string>> basesData = new Dictionary<string, List<string>>();
            string baseName = "";
            List<string> strings = new List<string>();

            while (!sr_BasesList.EndOfStream)
            {
                //Dictionary<string, List<string>> t = basesData;
                string str = sr_BasesList.ReadLine();
                // заполнение списка
                if (str.Length != 0)
                {
                    if (str[0] == '[') // если строка начинается с [, то в ней содержится название базы
                    {
                        str = str.Remove(0, 1);
                        str = str.Remove(str.Length - 1, 1);
                        baseName = str;
                        strings = new List<string>();
                    }
                    else if (str != null && str != "" && baseName != "")
                    {
                        strings.Add(str);
                        basesData[baseName] = strings;
                    }
                }
            }
            if (basesData.Count != 0)
            {
                foreach (string base_name in basesData.Keys)
                {
                    string name = base_name;
                    string pathExe = "";
                    string dateDel = "";

                    List<string> lines = basesData[base_name];
                    foreach (string base_line in lines)
                    {
                        if (base_line.Contains("DateDel="))
                        {
                            int index = base_line.IndexOf("DateDel=");
                            string substr = base_line.Substring(index + 8);

                            if (substr.Length >= 10 && Regex.IsMatch(substr, @"\d{2}\.\d{2}\.\d{4}"))
                            {
                                dateDel = substr;
                                //if (!isDel) { isDel = true; }
                            }
                        } else if (base_line.Contains("Connect=File=")) {

                            string input = base_line;//"Connect=File=\"C:\\Bases\\РТ Проф\";";
                            string pattern = @"Connect=File=""([^""]+)"";";

                            Match match = Regex.Match(input, pattern);
                            if (match.Success)
                            {
                                pathExe = match.Groups[1].Value;
                                // Дальнейшие действия с путем до папки
                            }
                        }
                    }
                    if (dateDel!="" && pathExe!="") 
                    {
                        listBase.Add(new BasesInfo(dateDel, pathExe));
                    }
                }
            }
            return listBase;
        }
    }
}
