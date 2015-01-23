using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner
{
    class scanner
    {
        public static bool scannerError;
        string variable = "";
        string special = "";
        static void checkVar(string variable, Dictionary<string, string> reservedWords, Form1 obj)
        {

            int tmp;
            if (variable.Length > 0)
            {
                bool Isexp = true;
                bool found = false;
                for (int i = 0; i < variable.Length; i++)
                {
                    if (variable[i] == 'E')
                    {
                        if (found == true || i == variable.Length - 1)
                            Isexp = false;
                        found = true;

                    }
                    else if (!Char.IsDigit(variable[i]))
                        Isexp = false;
                }

                if (Char.IsLetter(variable[0]))
                {
                    if (reservedWords.ContainsKey(variable))
                    {

                        obj.PrintOnGrid(variable, reservedWords[variable]);

                    }
                    else
                        obj.PrintOnGrid(variable, "ID");

                }
                else if (Isexp && found)
                    obj.PrintOnGrid(variable, "Exponent number");
                else if (int.TryParse(variable, out tmp))
                {
                    obj.PrintOnGrid(variable, "Number");
                }
                else
                {
                    obj.PrintOnGrid(variable, "Error1");
                    scannerError = true;
                    }
            }
        }


        static void checkSpec(string special, Dictionary<string, string> specialSymbols, Form1 obj)
        {
            //if (special == "\t" || special == "\n"||special=="\r")
            //    return;
            if (special.Length > 0)
            {
                if (specialSymbols.ContainsKey(special))
                {
                    obj.PrintOnGrid(special, specialSymbols[special]);
                }
                else
                {
                    for (int i = 0; i < special.Length; i++)
                    {
                        if (i < special.Length - 1)
                        {
                            string tmp = special[i].ToString() + special[i + 1];
                            if (specialSymbols.ContainsKey(tmp))
                            {
                                obj.PrintOnGrid(tmp, specialSymbols[tmp]);
                                i++;
                                continue;
                            }

                        }
                        if (specialSymbols.ContainsKey(special[i].ToString()))
                            obj.PrintOnGrid(special[i].ToString(), specialSymbols[special[i].ToString()]);
                        else
                        {
                            obj.PrintOnGrid(special[i].ToString(), "Error2");
                            scannerError = true;
                        }
                    }

                }
            }
        }

        static int commentMode(int i, string input, Form1 obj)
        {
            string comment = "";
            for (int j = i; ; j++)
            {
                comment += input[j];
                if (input[j] == '*')
                    if (input[j + 1] == '/')
                    {
                        i = j + 1;
                        break;
                    }
            }
            obj.PrintOnGrid(comment, "Comment");
            Console.WriteLine(comment + "Comment");
            return i;
        }


        public void scan(string input, Dictionary<string, string> reservedWords, Dictionary<string, string> specialSymbols, Form1 obj)
        {
            scannerError = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]) || Char.IsLetter(input[i]))
                {
                    variable += input[i];
                    if (special != "/*")
                        checkSpec(special, specialSymbols, obj);
                    else
                    {
                        i = commentMode(i - 2, input, obj);
                        variable = "";
                        special = "";
                        continue;
                    }
                    special = "";
                }
                else if (input[i] != ' ' && input[i] != '\n' && input[i] != '\r' && input[i] != '\t')
                {
                    special += input[i];
                    checkVar(variable, reservedWords, obj);
                    variable = "";
                }
                if (i == input.Length - 1 || input[i] == ' ' || input[i] == '\n' || input[i] == '\r' || input[i] == '\t')
                {
                    if (special != "/*")
                        checkSpec(special, specialSymbols, obj);
                    else
                    {
                        i = commentMode(i - 2, input, obj);
                        variable = "";
                        special = "";
                        continue;
                    }
                    checkVar(variable, reservedWords, obj);
                    variable = "";
                    special = "";
                }
            }
        }
    }
}
