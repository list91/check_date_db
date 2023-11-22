using System.IO;
using check_date;

var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string basesFilePath = userFolder + $"\\AppData\\Roaming\\1C\\1CEStart\\ibases.v8i";
List<BasesInfo> bases = BasesInfo.getBasesInfoList(basesFilePath);
string logFilePath = Directory.GetCurrentDirectory() + "\\log.txt";
foreach (BasesInfo baseInfo in bases) {
    string result = baseInfo.CheckDate();
    if (result == "real" || result == "past") 
    {
        baseInfo.DelFolder();
    }
}
    //Console.WriteLine("basesFilePath - " + basesFilePath);
    //Console.WriteLine("bases - " + bases.Count);

//string pathDel = userFolder + $"\\TEST_DEL";



//foreach (BasesInfo baseItem in bases)
//{
//    string result = BasesInfo.CheckDate(baseItem.date);
//    if (result != null)
//    {
//        if (result == "дата прошла" || result == "сегодняшняя прошла")
//        {
//            if (File.Exists(logFilePath))
//            {
//                using (StreamWriter sw = File.AppendText(logFilePath))
//                {
//                    DateTime now = DateTime.Now;
//                    Console.WriteLine(now.Day + "." + now.Month + "." + now.Year + "." + now.Hour + "." + now.Minute + "." + now.Second + " удалить - " + baseItem.path);
//                    //sw.WriteLine(now.Day + "." + now.Month + "." + now.Year + "." + now.Hour + "." + now.Minute + "." + now.Second + " удалить - " + baseItem.path);
//                }
//            }
//            else
//            {
//                using (StreamWriter sw = File.CreateText(logFilePath))
//                {
//                    DateTime now = DateTime.Now;
//                    Console.WriteLine(now.Day + "." + now.Month + "." + now.Year + "." + now.Hour + "." + now.Minute + "." + now.Second + " удалить - " + baseItem.path);
//                    //sw.WriteLine(now.Day + "." + now.Month + "." + now.Year + "." + now.Hour + "." + now.Minute + "." + now.Second + " удалить - " + baseItem.path);
//                }
//            }
//        }
//    }
//}
//Console.WriteLine("12323");
Console.ReadKey();