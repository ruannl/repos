using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using RL.Database.Models;

namespace RL.Areas.Accounting.Providers {
    public class AccountingManager : IDisposable {
        private static AccountingContext _accountingContext;
        private bool disposed;
        private static readonly object o = new object();

        public string WorkingFolder { get; set; }

        public AccountingManager(AccountingContext context) {
            _accountingContext = context;
        }

        public virtual void Attach<T>(T o, EntityState state = EntityState.Modified) where T : class {
            _accountingContext.Set<T>().Attach(o);
            _accountingContext.Entry(o).State = state;
        }

        public virtual void Save() {
            _accountingContext.SaveChanges();
        }

        public virtual void DeleteObject(object o) {
            if (o != null) {
                _accountingContext.Entry(o).State = EntityState.Deleted;
            }
        }

        //public List<BudgetItemCategory> GetBudgetItemCategories() {
        //    return accountingContext.Set<BudgetItemCategory>().ToList();
        //}

        public IList<Bank> BankList() {
            try {
                var items = from b in _accountingContext.Banks orderby b.BankId select b;
                return items.ToList();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        public IList<TransactionType> TransactionTypes() {
            try {
                var items = from transactionType in _accountingContext.TransactionTypes orderby transactionType.TransactionTypeName select transactionType;
                return items.ToList();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        public IList<TransactionType> TransactionTypesAndIdentifiers() {
            try {
                var items = from transactionType in _accountingContext.TransactionTypes.Include(i => i.TransactionTypeIdentifiers)
                    orderby transactionType.TransactionTypeName
                    select transactionType;

                return items.ToList();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        //public IList<TransactionType> ReturnTransactionTypes() {
        //    return accountingContext.Set<TransactionType>()
        //        .Include(x => x.TransactionTypeIdentifiers)
        //        .ToList();
        //}

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposed)
                if (disposing)
                    _accountingContext.Dispose();
            disposed = true;
        }

        public bool ExtractFileAndData(HttpPostedFileBase file, int bankId) {
            if (file.ContentType != "application/x-zip-compressed") {
                throw new Exception("invalid file type. expected application/x-zip-compressed. received " + file.ContentType);
            }

            if (file.ContentLength <= 0) {
                throw new Exception("invalid file. file may not be empty");
            }

            try {
                var filePath = ExtractFile(file, bankId);
                if (filePath.IsNullOrWhiteSpace()) return false;

                var extracted = ExtractData(filePath, bankId);
                Debug.Write(extracted);

                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        private string ExtractFile(HttpPostedFileBase file, int bankId) {
            if (string.IsNullOrWhiteSpace(WorkingFolder)) {
                throw new FileNotFoundException("Working Folder not set. Extract unable to continue.");
            }

            var filePath = WorkingFolder + file.FileName;
            if (File.Exists(filePath)) File.Delete(filePath);
            file.SaveAs(filePath);

            using (var archive = ZipFile.OpenRead(filePath)) {
                foreach (var entry in archive.Entries) {
                    if (entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)) {
                        var extractFileName = $"{WorkingFolder}{entry.FullName.Replace(".csv", "")}{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                        entry.ExtractToFile(extractFileName);
                        return extractFileName;
                    }
                }
            }

            return null;
        }

        private bool ExtractData(string filePath, int bankId) {
            try {
                var line = string.Empty;
                var name = string.Empty;
                var accountName = string.Empty;
                var accountNumber = string.Empty;
                var currentBalance = string.Empty;
                var availableBalance = string.Empty;
                var items = new List<string>();
                var loadTransactions = false;

                using (var reader = new StreamReader(filePath)) {
                    while ((line = reader.ReadLine()) != null) {
                        if (line.IsNullOrWhiteSpace()) continue;

                        switch (line.Substring(0, 8).ToLower()) {
                            case "account:":
                                accountNumber = line.Split(',')[1];
                                accountName = line.Split(',')[2];
                                continue;
                            case "balance:":
                                currentBalance = line.Split(',')[1];
                                availableBalance = line.Split(',')[2];
                                continue;
                            default:
                                if (line.Substring(0, 5).ToLower() == "name:") {
                                    name = line.Split(',')[1] + ' ' + line.Split(',')[1];
                                    continue;
                                }

                                break;
                        }

                        if (string.Equals(line.ToLower(), "date, amount, balance, description")) {
                            loadTransactions = true;
                        }

                        if (loadTransactions && !accountNumber.IsNullOrWhiteSpace() &&
                            !currentBalance.IsNullOrWhiteSpace() &&
                            !accountName.IsNullOrWhiteSpace()) {
                            items.Add(line);
                        }
                    }
                }

                if (items.Count <= 0) return false;

                //var bank = _accountingContext.Banks.FirstOrDefault(b => b.BankId == bankId);
                var bankAccount = _accountingContext.BankAccounts.FirstOrDefault(ba => ba.BankId == bankId && ba.AccountNumber == accountNumber);
                //accountNumber
                var statement = new Statement {
                    AccountBalance = Convert.ToDouble(currentBalance, new NumberFormatInfo {CurrencyDecimalDigits = 2, CurrencyDecimalSeparator = ","}), AvailableBalance = Convert.ToDouble(availableBalance, new NumberFormatInfo {CurrencyDecimalDigits = 2, CurrencyDecimalSeparator = ","}), BankAccount = bankAccount
                };

                _accountingContext.Statements.Add(statement);
                var statementSave = _accountingContext.SaveChanges();

                foreach (var item in items) {
                    var data = item.Split(',');
                    if (data[0].ToLower() == "date") {
                        continue;
                    }

                    var date = Convert.ToDateTime(data[0].Trim());
                    var amount = Convert.ToDouble(data[1].Trim(), new NumberFormatInfo {CurrencyDecimalDigits = 2, CurrencyDecimalSeparator = ","});
                    var itemBalance = Convert.ToDouble(data[2].Trim(), new NumberFormatInfo {CurrencyDecimalDigits = 2, CurrencyDecimalSeparator = ","});
                    var description = data[3].Trim();

                    _accountingContext.Transactions.Add(new TransactionEntry {
                        TransactionDate = date, TransactionAmount = amount, TransactionBalance = itemBalance, TransactionDescription = description
                    });
                }

                var x = _accountingContext.SaveChanges();
                Debug.WriteLine(x);

                File.Delete(filePath);

                return true;

            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}