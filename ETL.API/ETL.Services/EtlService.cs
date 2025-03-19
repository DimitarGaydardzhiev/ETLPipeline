using CsvHelper;
using ETL.Data.Models;
using ETL.Data.Repositories;
using Newtonsoft.Json;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;

namespace ETL.Services
{
    public class EtlService: IEtlService
    {
        private readonly UnitOfWork unitOfWork;

        public EtlService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Start()
        {
            var csvData = this.ExtractFromCSV("transactions.csv");
            var apiData = this.ExtractFromAPI("https://mockapi.com/transactions");

            var transformedData = this.FilterData(csvData, t => t.Amount >= 100).ToList();
            transformedData.AddRange(this.FilterData(apiData, t => t.Amount >= 100));

            SaveData(transformedData);
        }

        private IEnumerable<Transaction> ExtractFromCSV(string filePath)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            string fullPath = Path.Combine(projectRoot, "DataFiles", filePath);

            List<Transaction> transactions = new List<Transaction>();
            using (var reader = new StreamReader(fullPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                transactions = new List<Transaction>(csv.GetRecords<Transaction>());
            }
            return transactions;
        }

        private IEnumerable<Transaction> ExtractFromAPI(string url)
        {
            Task.Delay(500);

            string mockJsonResponse = @"
            [
                {""Id"": ""550e8400-e29b-41d4-a716-446655440001"", ""CustomerID"": 1, ""Amount"": 150.75, ""TransactionDate"": ""2024-03-19T10:30:00""},
                {""Id"": ""3fa85f64-5717-4562-b3fc-2c963f66afa2"", ""CustomerID"": 2, ""Amount"": 89.50, ""TransactionDate"": ""2024-03-18T15:45:00""},
                {""Id"": ""6f9619ff-8b86-d011-b42d-00cf4fc964ff"", ""CustomerID"": 3, ""Amount"": 200.00, ""TransactionDate"": ""2024-03-17T09:15:00""},
                {""Id"": ""d9428888-122b-4dbb-8c43-df4bf0fdba14"", ""CustomerID"": 4, ""Amount"": 120.00, ""TransactionDate"": ""2024-03-16T12:00:00""}
            ]";

            return JsonConvert.DeserializeObject<List<Transaction>>(mockJsonResponse);
        }

        private IEnumerable<Transaction> FilterData(IEnumerable<Transaction> transactions, Func<Transaction, bool> filterFunction)
        {
            var uniqueTransactions = new HashSet<Transaction>(transactions);
            uniqueTransactions.RemoveWhere(t => !filterFunction(t));
            return uniqueTransactions;
        }

        private void SaveData(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                var curretnTransaction = new Transaction()
                {
                    Amount = transaction.Amount,
                    CustomerID = transaction.CustomerID,
                    Id = transaction.Id,
                    TransactionDate = transaction.TransactionDate,
                };

                this.unitOfWork.TransactionRepository.Add(curretnTransaction);
            }

            this.unitOfWork.SaveChanges();
        }
    }
}
