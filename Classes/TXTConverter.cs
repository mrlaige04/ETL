﻿using ETL.Interfaces;
using ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ETL.Classes;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ETL.Classes
{
    internal class TXTConverter
    {
        private async Task<string> ReadFileAsync(string path)
        {
            using (StreamReader streamReader = new(path))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        private IEnumerable<TransactionDTO> GetListTransDTO(string[] lines)
        {
            foreach (var line in lines)
            {
                yield return GetTransactionDTO(line);
            }
        }
        
        private TransactionDTO GetTransactionDTO(string line)
        {
            string[] fields = line.Split(",");

            string[] validformats = new[] { "MM/dd/yyyy", "yyyy/MM/dd", "MM/dd/yyyy", "MM/dd/yyyy", "yyyy-dd-MM" };
            TransactionDTO transcationDTO = new TransactionDTO()
            {
                first_name = fields[0].Trim(),
                last_name = fields[1].Trim(),
                city = Regex.Replace(Regex.Replace(fields[2].Trim(), @"\“", ""), "\"", ""),
                payment = decimal.Parse(fields[5].Trim()),
                date = DateTime.ParseExact(fields[6].Trim(), validformats, null),
                account_number = long.Parse(fields[7].Trim()),
                service = fields[8].Trim()
            };
            return transcationDTO;
        }
        
        private Output ConvertListDTO_ToOutput(IEnumerable<TransactionDTO> dtoModels)
        {
            Output output = new();

            foreach (var dtoModel in dtoModels)
            {
                var cur_city = output.transcations.FirstOrDefault(x => x.city == dtoModel.city);

                if (cur_city == null)
                {
                    output.transcations.Add(new TranscationModel()
                    {
                        city = dtoModel.city
                    });
                }

                var cur_serv = output.transcations.FirstOrDefault(x => x.city == dtoModel.city).services.FirstOrDefault(x => x.name == dtoModel.service);

                if (cur_serv == null)
                {
                    output.transcations.FirstOrDefault(x => x.city == dtoModel.city).services.Add(new Service()
                    {
                        name = dtoModel.service
                    });
                }

                var cur_payer = output.transcations.FirstOrDefault(x => x.city == dtoModel.city).services
                    .FirstOrDefault(x => x.name == dtoModel.service).payers
                    .FirstOrDefault(x => x.account_number == dtoModel.account_number);

                output.transcations.FirstOrDefault(x => x.city == dtoModel.city).services.FirstOrDefault(x => x.name == dtoModel.service).payers.Add(new Payer()
                {
                    name = dtoModel.first_name + " " + dtoModel.last_name,
                    payment = dtoModel.payment,
                    date = dtoModel.date,
                    account_number = dtoModel.account_number
                });

                output.transcations.ForEach(x =>
                {
                    x.services.ForEach(s =>
                    {
                        s.total = s.payers.Sum(p => p.payment);
                    });
                    x.total = x.services.Sum(s => s.total);
                });
            }
            return output;
        }

        public async Task<Output> GetOutputFromTXTFileAsync(string filePath)
        {
            string fileContent = await ReadFileAsync(filePath);
            string[] lines = fileContent.Split('\n');
            IEnumerable<TransactionDTO> dtoS = GetListTransDTO(lines);
            return ConvertListDTO_ToOutput(dtoS);
        }
    }
}
