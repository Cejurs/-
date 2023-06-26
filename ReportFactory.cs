using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Тестовое_задание
{
    public class ReportFactory
    {
        // Словарь: ключ - ответсвенное лицо.
        private Dictionary<string, ReportRow> dictionary;
        private object resourseLocker = new object();
        private string errorMessage = string.Empty;
        private bool hasError = false;
        public ReportFactory() 
        { 
            dictionary = new Dictionary<string, ReportRow>();
        }

        private string UnificatePersonName(string personName)
        {
            var array = personName.Split(' ','.');
            var lastName = array[0];
            var firstNameLetter = array[1]?[0].ToString();
            var patronomicNameLatter = array[2]?[0].ToString();
            var result = lastName + " " + firstNameLetter + ". "+ patronomicNameLatter +".";
            return result;
        }

        private void HandleRKKFile(string RKKFilePath)
        {
            StreamReader? fileStream = null;
            try
            {
                fileStream = File.OpenText(RKKFilePath);
                var line = "";
                while ((line = fileStream.ReadLine()) != null)
                {
                    var array = line.Split('\t');
                    if (array.Length == 0) continue;

                    var chief = array[0].Trim();

                    if (array.Length == 1)
                    {
                        IncreaseUnfullfilledDocumentCount(UnificatePersonName(chief));
                        continue;
                    }

                    if(chief == "Климов Сергей Александрович")
                    {
                        var responsiblePerson = UnificatePersonName(array[1].Split(';')[0].Replace("(Отв.)", "").Trim());
                        IncreaseUnfullfilledDocumentCount(responsiblePerson);
                        continue;
                    }

                    IncreaseUnfullfilledDocumentCount(UnificatePersonName(chief));
                }
            }
            catch (Exception ex)
            {
                errorMessage += $"Файл с РКК: {RKKFilePath} \n" + ex.Message +"\n";
                hasError = true;
            }
            finally
            {
                fileStream?.Close();
            }
        }
 
        private void HandleRequestFile(string requestFilePath)
        {
            StreamReader? fileStream = null;
            try
            {
                fileStream = File.OpenText(requestFilePath);
                var line = "";
                while ((line = fileStream.ReadLine()) != null)
                {
                    var array = line.Split('\t');
                    if (array.Length == 0) continue;
                    var chief = array[0].Trim();
                    // Если нет 2 столбца добавляем руководителя в любом случае
                    if (array.Length == 1)
                    {
                        IncreaseUnfullfiledRequestCount(UnificatePersonName(chief));
                        continue;
                    }
                    if(chief == "Климов Сергей Александрович")
                    {
                        var responsiblePerson = UnificatePersonName(array[1].Split(';')[0].Replace("(Отв.)", "").Trim());
                        IncreaseUnfullfiledRequestCount(responsiblePerson);
                        continue;
                    }

                    IncreaseUnfullfiledRequestCount(UnificatePersonName(chief));
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Файл с обращениями: {requestFilePath} \n"+ex.Message + "\n";
                hasError = true;
            }
            finally
            {
                fileStream?.Close();
            }
        }

        /// <summary>
        /// Создание отчета
        /// </summary>
        /// <param name="RKKFilePath">Путь до файла с РКК</param>
        /// <param name="requestFilePath">Путь до файла с запросами</param>
        /// <param name="report">Отчет, если все прошло успешно или null</param>
        /// <param name="errorMessage"></param>
        /// <returns>Результат создание успех/неудача</returns>
        public  bool CreateReport(string RKKFilePath,string requestFilePath,out Report? report,out string? error)
        {
            error = null;
            report = null;
            Task handleRKK = Task.Run(()=>HandleRKKFile(RKKFilePath));
            Task handleRequest = Task.Run(()=>HandleRequestFile(requestFilePath));

            Task.WaitAll(handleRKK,handleRequest);
            if (dictionary.Values.Count == 0)
            {
                hasError = true;
                errorMessage += "В файлах нет данных";
            }
            if (hasError)
            {
                error = errorMessage;
                return false;
            }
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
