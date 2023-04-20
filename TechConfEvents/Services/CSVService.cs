using CsvHelper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using TechConfEvents.Dto;
using TechConfEvents.Helper;
using TechConfEvents.Interface;
using TechConfEvents.Models;

namespace TechConfEvents.Services
{
    public class CSVService : ICSVService
    {
        public void WriteCSV<T>(List<T> records)
        {
            using (var writer = new StreamWriter("C:\\Selva\\export.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
