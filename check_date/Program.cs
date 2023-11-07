using System.IO;
// See https://aka.ms/new-console-template for more information
using check_date;

var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string basesFilePath = userFolder + $"\\AppData\\Roaming\\1C\\1CEStart\\ibases.v8i";
List<BasesInfo> bases = BasesInfo.getBasesInfoList(basesFilePath);
string logFilePath = Directory.GetCurrentDirectory() + "\\log.txt";

foreach (BasesInfo baseItem in bases)
{
    string result = BasesInfo.CheckDate(baseItem.date);
    if (result != null)
    {
        if (result == "дата прошла" || result == "сегодняшняя прошла")
        {
            if (File.Exists(logFilePath))
            {
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    DateTime now = DateTime.Now;
                    sw.WriteLine(now.Day + "." + now.Month + "." + now.Year + "." + now.Hour + "." + now.Minute + "." + now.Second + " удалить - " + baseItem.path);
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(logFilePath))
                {
                    DateTime now = DateTime.Now;
                    sw.WriteLine(now.Day + "." + now.Month + "." + now.Year + "." + now.Hour + "." + now.Minute + "." + now.Second + " удалить - " + baseItem.path);
                }
            }
        }
    }
}
Console.ReadKey();