namespace ConsoleUI
{
    public class UI
    {
        public static void Header(string header, bool clearPreviousScreen = true)
        {
            Console.WriteLine("");
            if (clearPreviousScreen) Console.Clear();
            else Console.WriteLine("------------------------------------------");
            Console.WriteLine(header.ToUpper());
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("");
        }

        public static void Option(string option, string description = "", string current = "")
        {
            string text = $"\t{option}";
            if (current != "") text += $" ({current})";
            if (description != "") text += $"\t\t- {description}";
            Console.WriteLine(text);
        }

        public static void Pause()
        {
            Console.WriteLine("");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        public static void Error(string description)
        {
            Header("///// Error /////");
            Console.WriteLine(description);
            Pause();
        }

        public static void Warning(string description)
        {
            Header("Warning", false);
            Console.WriteLine(description);
        }

        public static void Input(string type, string defaultValue = "")
        {
            Console.WriteLine("");
            if (defaultValue == "") Console.Write($"Input ({type}): ");
            else Console.Write($"Input ({type}): [{defaultValue}] ");
        }
    }

    public class Input
    {
        public static bool GetBoolean(bool defaultValue = false)
        {
            string type = "bool";
            UI.Input("bool", defaultValue.ToString());
            string input = ReadLine();
            bool output = defaultValue;
            if (bool.TryParse(input, out bool val)) output = val;
            else InputInvalid(type, input, defaultValue.ToString());

            return output;
        }
        public static int GetInteger(int defaultValue = 0)
        {
            string type = "int";
            UI.Input("int", defaultValue.ToString());
            string input = ReadLine();
            int output = defaultValue;
            if (int.TryParse(input, out int val)) output = val;
            else InputInvalid(type, input, defaultValue.ToString());

            return output;
        }

        public static float GetFloat(float defaultValue = 0f)
        {
            string type = "float";
            UI.Input(type, defaultValue.ToString());
            string input = ReadLine();
            float output = defaultValue;
            if (float.TryParse(input, out float val)) output = val;
            else InputInvalid(type, input, defaultValue.ToString());

            return output;
        }

        public static double GetDouble(double defaultValue = 0)
        {
            string type = "double";
            UI.Input(type, defaultValue.ToString());
            string input = ReadLine();
            double output = defaultValue;
            if (double.TryParse(input, out double val)) output = val;
            else InputInvalid(type, input, defaultValue.ToString());

            return output;
        }


        public static decimal GetDecimal(decimal defaultValue = 0m)
        {
            string type = "decimal";
            UI.Input(type, defaultValue.ToString());
            string input = ReadLine();
            decimal output = defaultValue;
            if (decimal.TryParse(input, out decimal val)) output = val;
            else InputInvalid(type, input, defaultValue.ToString());

            return output;
        }

        public static string GetString(string defaultValue = "")
        {
            string type = "string";
            UI.Input(type, defaultValue.ToString());
            string input = ReadLine();
            string output = defaultValue;
            if (input != "") output = input;
            else InputInvalid(type, input, defaultValue.ToString());

            return output;
        }

        static string ReadLine()
        {
            string? input = Console.ReadLine();
            if (input == null) input = "";

            return input;
        }

        static void InputInvalid(string type, string input, string valueSetTo)
        {
            UI.Warning($"\"{input}\" was not of type \"{type}\". Default value \"{valueSetTo}\" was used.");
        }
    }
}
