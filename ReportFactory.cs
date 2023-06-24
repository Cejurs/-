﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Тестовое_задание
{
    public class ReportFactory
    {
        // Словарь: ключ - ответсвенное лицо.
        private Dictionary<string, ReportRow> dictionary;
        private object resourseLocker = new object();
        private string errorMessage = string.Empty;

        public ReportFactory() 
        { 
            dictionary = new Dictionary<string, ReportRow>();
        }

  
        private bool HandleRKKFile(string RKKFilePath)
        {
            var isSuccess = true;
            StreamReader? fileStream = null;
            try
            {
                fileStream = File.OpenText(RKKFilePath);
                var line = "";
                while ((line = fileStream.ReadLine()) != null)
                {
                    var array = line.Split('\t');
                    if (array.Length == 0) continue;
                    if (array.Length == 1)
                    {
                        IncreaseUnfullfilledDocumentCount(array[0]);
                        continue;
                    }

                    var responsiblePersons = array[1].Split(';');
                    foreach (var responiblePerson in responsiblePersons)
                    {
                        var person = responiblePerson.Replace("(Отв.)", "").Trim();
                        IncreaseUnfullfilledDocumentCount(person);
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage += $"Файл с РКК: {RKKFilePath} \n" + ex.Message +"\n";
                isSuccess = false;
            }
            finally
            {
                fileStream?.Close();
            }

            return isSuccess;
        }
 
        private bool HandleRequestFile(string requestFilePath)
        {
            var isSuccess = true;
            StreamReader? fileStream = null;
            try
            {
                fileStream = File.OpenText(requestFilePath);
                var line = "";
                while ((line = fileStream.ReadLine()) != null)
                {
                    var array = line.Split('\t');
                    if (array.Length == 0) continue;
                    if (array.Length == 1)
                    {
                        IncreaseUnfullfiledRequestCount(array[0]);
                        continue;
                    }

                    var responsiblePersons = array[1].Split(';');
                    foreach(var responiblePerson  in responsiblePersons)
                    {
                        var person = responiblePerson.Replace("(Отв.)","").Trim();
                        IncreaseUnfullfiledRequestCount(person);
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Файл с обращениями: {requestFilePath} \n"+ex.Message + "\n";
                isSuccess = false;
            }
            finally
            {
                fileStream?.Close();
            }
            return isSuccess;
        }

        /// <summary>
        /// Создание отчета
        /// </summary>
        /// <param name="RKKFilePath">Путь до файла с РКК</param>
        /// <param name="requestFilePath">Путь до файла с запросами</param>
        /// <param name="report">Отчет, если все прошло успешно или null</param>
        /// <param name="errorMessage"></param>
        /// <returns>Результат создание успех/неудача</returns>
        public  bool CreateReport(string RKKFilePath,string requestFilePath,out Report? report,out string? errorMessage)
        {
            report = null;
            errorMessage = null;
            Task handleRKK = Task.Run(()=>HandleRKKFile(RKKFilePath));
            Task handleRequest = Task.Run(()=>HandleRequestFile(requestFilePath));

            Task.WaitAll(handleRKK,handleRequest);

            report = new Report(dictionary.Values);
            return true;
        }

        private void IncreaseUnfullfilledDocumentCount(string responsiblePerson)
        {
            lock(resourseLocker)
            {
                if(!dictionary.ContainsKey(responsiblePerson))
                {
                    dictionary.Add(responsiblePerson, new ReportRow(responsiblePerson, 1, 0));
                    return;
                }

                dictionary[responsiblePerson].IncreaseUnfunfilledDocumentCount();
            }
        }
        private void IncreaseUnfullfiledRequestCount(string responsiblePerson)
        {
            lock (resourseLocker)
            {
                if (!dictionary.ContainsKey(responsiblePerson))
                {
                    dictionary.Add(responsiblePerson, new ReportRow(responsiblePerson, 0, 1));
                    return;
                }

                dictionary[responsiblePerson].IncreaseUnfunfilledRequestCount();
            }
        }
    }
}