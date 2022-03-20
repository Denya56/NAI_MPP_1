﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NAI_MPP_1.SourceFiles
{
    class KNN_AI
    {
        public Dictionary<double[], string> learnData { get; set; }
        public Dictionary<double[], string> testData { get; set; }
        public List<string> testAnswers { get; set; }
        public List<string> results { get; set; }
        public int k { get; set; }

        public KNN_AI(int kIndex, string learnFilePath, string testFilePath) 
        {
            k = kIndex;
            learnData = ReadData(learnFilePath);
            testData = ReadData(testFilePath);

            testAnswers = testData.Values.ToList();
            results = new List<string>();
        }
        private Dictionary<double[], string> ReadData(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var data = new Dictionary<double[], string>();

            using (StreamReader sr = new StreamReader(fs))
            {
                double d;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(",");

                    if (s.Take(s.Length - 1).All(n => Double.TryParse(n, out d)))
                    {
                        data.Add(Array.ConvertAll<string, double>(s.Take(s.Length - 1).ToArray(), Convert.ToDouble), s[s.Length - 1]);
                    }
                }
            }
            return data;
        }
        public void Run(int option)
        {
            Dictionary<double[], string> vectorDistanceAndAnswer = new Dictionary<double[], string>();
            List<string> sortedAnswers;
            string most;
            switch (option)
            {
                case 1:
                    foreach (var vectorTest in testData)
                    {
                        foreach (var vectorLearn in learnData)
                        {
                            vectorDistanceAndAnswer.Add(CalculateVectorsDistance(vectorTest.Key, vectorLearn.Key), vectorLearn.Value);
                        }

                        /*foreach (KeyValuePair<double[], string> kvp in vectorDistance)
                        {
                            //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                            Console.WriteLine("Key = {0}, Value = {1}", kvp.Key.ToArray()[0], kvp.Value);
                        }*/



                        /*foreach (var s in vectorDistance.OrderBy(a => a.Key[0]))
                        {
                            Console.WriteLine(ii++ + ". Key = {0}, Value = {1}", s.Key[0], s.Value);
                        }*/

                        //Console.WriteLine("\nTest\n");

                        /*for (int i = 0; i < sortedAnswers.Count; i++)
                        {
                            string ss = sortedAnswers[i];
                            Console.WriteLine(i + ". " + ss);
                        }*/


                        //Console.WriteLine(most);
                        sortedAnswers = vectorDistanceAndAnswer.OrderBy(a => a.Key[0]).Select(x => x.Value).Take(k).ToList();
                        most = sortedAnswers.GroupBy(i => i).Select(grp => grp.Key).First();
                        results.Add(most);
                    }
                    break;
                case 2:
                    int dataSize = learnData.Keys.ToArray()[0].Length - 1;
                    Console.WriteLine("Input data in format: {0}{1}", 
                                      String.Concat(Enumerable.Repeat("double,", dataSize)), "double");
                    double d;
                    string[] s = Console.ReadLine().Split(",");
                    if(s.Length != dataSize && !s.All(n => Double.TryParse(n, out d)))
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }

                    foreach (var vectorLearn in learnData)
                    {
                        vectorDistanceAndAnswer.Add(CalculateVectorsDistance((Array.ConvertAll<string, double>(s, Convert.ToDouble)), vectorLearn.Key), vectorLearn.Value);
                    }
                    sortedAnswers = vectorDistanceAndAnswer.OrderBy(a => a.Key[0]).Select(x => x.Value).Take(k).ToList();
                    most = sortedAnswers.GroupBy(i => i).Select(grp => grp.Key).First();
                    results.Add(most);
                    break;
            }
            
        }

        private double[] CalculateVectorsDistance(double[] vec_1, double[] vec_2)
        {
            int vectorLength = vec_1.Length;
            double[] result = new double[1];

            for (int i = 0; i < vectorLength; i++)
            {
                double pow = Math.Pow((vec_1[i] - vec_2[i]), 2);
                result[0] += pow;
            }

            return result;
        }
    }
}