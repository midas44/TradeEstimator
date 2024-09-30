using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System;

namespace TradeEstimator.Conf
{
    public class IniFile
    {
        public static int capacity = 512;

        const string INI_EXT = ".ini";
        string _path;
        string _exeName = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string section, string key, string value, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string section, string key, string defaultValue, [In, Out] char[] value, int size, string filePath);

        // Путь к файлу конфигурации (может использоваться за пределами методов)
        // IniFile ini = new IniFile(Environment.ExpandEnvironmentVariables("%ProgramFiles%\\AppFolder\\config.ini"));
        public IniFile(string iniPath = null)
        {
            _path = new FileInfo(iniPath ?? _exeName + INI_EXT).FullName.ToString();
        }
        // Чтение значения указного ключа в указанной секции файла конфигурации
        // ini.Read("keyName", "sectionName");
        public string Read(string key, string section = null)
        {
            var retVal = new StringBuilder(255);
            GetPrivateProfileString(section ?? _exeName, key, "", retVal, 255, _path);
            return retVal.ToString();
        }
        // Запись значения указного ключа и указанной секции в файл конфигурации
        // ini.Write("keyName", "valueName", "sectionName");
        public void Write(string key, string value, string section = null)
        {
            WritePrivateProfileString(section, key, value, _path);
        }
        // Удалние указанного ключа в файле конфигурации
        // ini.DeleteKey("keyName", "sectionName");
        public void DeleteKey(string key, string section = null)
        {
            Write(key, null, section ?? _exeName);
        }
        // Удалние указанной секции в файле конфигурации
        // ini.DeleteSection("sectionName");
        public void DeleteSection(string section = null)
        {
            Write(null, null, section ?? _exeName);
        }
        // Проверка наличия указанного ключа в указанной секции файла конфигурации
        // if(!ini.KeyExists("keyName", "sectionName"))
        public bool KeyExists(string key, string section = null)
        {
            return Read(key, section).Length > 0;
        }
        // Чтение всех ключей в указанной секции файла конфигурации
        // string[] values = ini.ReadKeys("GUID");
        public string[] ReadKeys(string section)
        {
            // Первая строка не распознается, если ini-файл сохранен в UTF-8 с BOM
            while (true)
            {
                char[] chars = new char[capacity];
                int size = GetPrivateProfileString(section, null, "", chars, capacity, _path);

                if (size == 0)
                {
                    return null;
                }

                if (size < capacity - 2)
                {
                    string result = new String(chars, 0, size);
                    string[] keys = result.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                    return keys;
                }
                capacity = capacity * 2;
            }
        }
    }
}