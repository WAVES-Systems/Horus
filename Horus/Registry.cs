using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waves.Horus
{
    public static class Registry
    {
        static bool initialized = false;
        static Microsoft.Win32.RegistryKey key;
        public static bool Initialize(string path)
        {
            try
            {
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(path);
                initialized = true;
            }
            catch
            {
                System.Windows.MessageBox.Show("Error initalizing data gate!", "WAVES Systems", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return initialized;
        }

        private static void ThrowError()
        {
            System.Windows.MessageBox.Show("Data gate not initialized!", "WAVES Systems", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        public static string Get(string name, string defaultValue)
        {
            if (initialized)
            {
                return (string)key.GetValue(name, defaultValue);
            }
            ThrowError();
            return "FATAL FAIL";
        }

        public static void Set(string name, object value)
        {
            if (initialized)
            {
                key.SetValue(name, value);
                return;
            }
            ThrowError();
        }
    }
}
