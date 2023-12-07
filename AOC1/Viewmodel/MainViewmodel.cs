using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace AOC1.Viewmodel
{
    internal class MainViewmodel : ObservableObject
    {
        private string[] numberTexts;
        public IRelayCommand FindFileExecute { get; }

        private string _finalCount;
        public string FinalCount
        {
            get => _finalCount;
            set => SetProperty(ref _finalCount, value);
        }
        public MainViewmodel() 
        {
            FindFileExecute = new RelayCommand(FindFile);
            numberTexts = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        }

        private void FindFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text documents (.txt)|*.txt";
            string line;
            int counted;
            if (openFileDialog.ShowDialog() == true)
            {
                counted = 0;
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                while ((line = streamReader.ReadLine()) != null)
                {
                    char[] linechars = line.ToCharArray();
                    int i = 0;
                    string workline="";
                    string rewritten;
                    while (i < linechars.Length)
                    {
                        workline += linechars[i].ToString();
                        foreach (string x in numberTexts)
                        {
                            if (workline.Contains(x))
                            {
                                workline = workline.Replace(x, (Array.FindIndex(numberTexts, y => y.Contains(x)) + 1).ToString());


                                char[] numchars = x.ToCharArray();
                                string toTestMatch = numchars[numchars.Length - 1].ToString();
                                if (toTestMatch == "e" ^ toTestMatch == "o" ^ toTestMatch == "t" ^ toTestMatch == "n") 
                                {
                                    if(linechars.Length-1 >= i + 4)
                                    {
                                        int prcat = i;
                                        int prcatMax = i + 4;
                                        string mrdat = "";
                                        while (prcat <= prcatMax)
                                        {
                                            mrdat += linechars[prcat].ToString();
                                            prcat++;
                                        }
                                        foreach (string xx in numberTexts)
                                        {
                                            if (mrdat.Contains(xx))
                                            {
                                                workline += (Array.FindIndex(numberTexts, y => y.Contains(xx)) + 1).ToString();
                                            }
                                        }
                                    }
                                    else if (linechars.Length - 1 >= i + 2)
                                    {
                                        int prcat = i;
                                        int prcatMax = i + 2;
                                        string mrdat = "";
                                        while (prcat <= prcatMax)
                                        {
                                            mrdat += linechars[prcat].ToString();
                                            prcat++;
                                        }
                                        foreach (string xx in numberTexts)
                                        {
                                            if (mrdat.Contains(xx))
                                            {
                                                workline += (Array.FindIndex(numberTexts, y => y.Contains(xx)) + 1).ToString();
                                            }
                                        }
                                    }
                                }
                                
                            }
                        }
                        i++;
                    }                
                    string OnlyDigits = string.Concat(workline.Where(Char.IsDigit));
                    char[] chars = OnlyDigits.ToCharArray();
                    string final = chars.First().ToString() + chars.Last().ToString();
                    counted += int.Parse(final);
                }
                FinalCount = "Tak celkem to bude " + counted.ToString();
            }
        }
    }
}
