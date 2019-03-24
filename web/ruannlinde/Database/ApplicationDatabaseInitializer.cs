﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using log4net;
using RL.Database.Models;

namespace RL.Database {
	public class ApplicationDatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationDatabaseContext> {
		private static readonly ILog Log = LogManager.GetLogger(typeof(ApplicationDatabaseInitializer).Name);

		public override void InitializeDatabase(ApplicationDatabaseContext context) {
			//context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, $"ALTER DATABASE {context.Database.Connection.Database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
			base.InitializeDatabase(context);
		}

		protected override void Seed(ApplicationDatabaseContext context) {

			var lookupTypes = new List<LookupType> {new LookupType {Name = "Contains"}, new LookupType {Name = "Starts With"}, new LookupType {Name = "Ends With"}};

			var accountTypes = new List<AccountType> {new AccountType {AccountTypeName = "Transaction", AccountTypeCode = "TRNS"}, new AccountType {AccountTypeName = "Split", AccountTypeCode = "SPL"}};

			var accounts = new List<Account> {new Account {AccountName = "ABSA", AccountType = accountTypes.FirstOrDefault(x => x.AccountTypeCode == "TRNS")}, new Account {AccountName = "Accounts Receivable", AccountType = accountTypes.FirstOrDefault(x => x.AccountTypeCode == "SPL")}};

			var expressions = new List<CleanupExpression> {new CleanupExpression {Expression = "ACB CREDIT"}, new CleanupExpression {Expression = "CASH DEP BRANCH"}, new CleanupExpression {Expression = "IBANK"}, new CleanupExpression {Expression = "ABSA BANK"}};

			var paymentTypes = new List<PaymentType> {
				                                         new PaymentType {PaymentTypeName = "Cash", PaymentTypeCode = "Cash"}
				                                       , new PaymentType {PaymentTypeName = "Credit Card", PaymentTypeCode = "CC"}
				                                       , new PaymentType {PaymentTypeName = "Direct Debit", PaymentTypeCode = "DD"}
				                                       , new PaymentType {PaymentTypeName = "Electronic Transfer", PaymentTypeCode = "EFT"}
				                                       , new PaymentType {PaymentTypeName = "Medical Aid", PaymentTypeCode = "MA"}
				                                       , new PaymentType {PaymentTypeName = "Less Payment", PaymentTypeCode = "LP"}
			                                         };

			var contains = lookupTypes.FirstOrDefault(t => t.Name == "Contains");
			var startsWith = lookupTypes.FirstOrDefault(t => t.Name == "Starts With");
			var endsWith = lookupTypes.FirstOrDefault(t => t.Name == "Ends With");

			var companies = new List<Company> {
				                                  new Company {Name = "AECI", Code = "AECI"}
				                                , new Company {Name = "Affinity", Code = "Affinity"}
				                                , new Company {Name = "AMS", Code = "AMS "}
				                                , new Company {Name = "Bankmed", Code = "Bankmed"}
				                                , new Company {Name = "Barloworld", Code = "Barloworld"}
				                                , new Company {Name = "Bestmed", Code = "Bestmed"}
				                                , new Company {Name = "Bonitas", Code = "Bonitas"}
				                                , new Company {Name = "CAMAF", Code = "CAMAF"}
				                                , new Company {Name = "CMP", Code = "CMP"}
				                                , new Company {Name = "Compcare", Code = "CMP"}
				                                , new Company {Name = "Denis", Code = "Denis"}
				                                , new Company {Name = "Discovery", Code = "DISC"}
				                                , new Company {Name = "DRC Medihelp", Code = "DRC"}
				                                , new Company {Name = "Fedhealth", Code = "FH"}
				                                , new Company {Name = "Gems", Code = "GEMS"}
				                                , new Company {Name = "Get Savvi", Code = "GS"}
				                                , new Company {Name = "Hosmed", Code = "HM"}
				                                , new Company {Name = "Imperial Med", Code = "IM"}
				                                , new Company {Name = "Lib Care", Code = "LC"}
				                                , new Company {Name = "Keyhealth", Code = "KH"}
				                                , new Company {Name = "Keymed", Code = "KM"}
				                                , new Company {Name = "Malcor", Code = "MC"}
				                                , new Company {Name = "Massmart", Code = "MM"}
				                                , new Company {Name = "Medihelp", Code = "MH"}
				                                , new Company {Name = "Medshield", Code = "MS"}
				                                , new Company {Name = "Medimed", Code = "MM"}
				                                , new Company {Name = "Momentum", Code = "MOM"}
				                                , new Company {Name = "Momentum Healthsaver", Code = "MOMHS"}
				                                , new Company {Name = "Moto Health", Code = "MH"}
				                                , new Company {Name = "MSD", Code = "MSD"}
				                                , new Company {Name = "PG Group", Code = "PG Group"}
				                                , new Company {Name = "Polmed", Code = "POL"}
				                                , new Company {Name = "Profmed", Code = "PROF"}
				                                , new Company {Name = "Reso Med", Code = "RM"}
				                                , new Company {Name = "Remedi", Code = "REM"}
				                                , new Company {Name = "SASOL Med", Code = "SM"}
				                                , new Company {Name = "Selfmed", Code = "Selfmed"}
				                                , new Company {Name = "Sizwe", Code = "Sizwe"}
				                                , new Company {Name = "Sisonke", Code = "SIS"}
				                                , new Company {Name = "Spectramed", Code = "Spectramed"}
				                                , new Company {Name = "Stancom", Code = "SC"}
				                                , new Company {Name = "SEDMED", Code = "SEDM"}
				                                , new Company {Name = "Transmed", Code = "TM"}
				                                , new Company {Name = "Tiger Brands", Code = "TB"}
				                                , new Company {Name = "TopMed", Code = "TOPM"}
			                                  };

			var companyIdentifiers = new List<CompanyIdentifier> {
				                                                     new CompanyIdentifier {Expression = "AECI", Company = companies.FirstOrDefault(c => c.Name == "AECI"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "AFFINITY", Company = companies.FirstOrDefault(c => c.Name == "Affinity"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "8MN33", Company = companies.FirstOrDefault(c => c.Name == "Affinity"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "AMS", Company = companies.FirstOrDefault(c => c.Name == "AMS"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "BANKMED", Company = companies.FirstOrDefault(c => c.Name == "Bankmed"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "BAR", Company = companies.FirstOrDefault(c => c.Name == "Barloworld"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "BESTMED", Company = companies.FirstOrDefault(c => c.Name == "Bestmed"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "BON", Company = companies.FirstOrDefault(c => c.Name == "Bonitas"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "CAMAF", Company = companies.FirstOrDefault(c => c.Name == "CAMAF"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "CMP", Company = companies.FirstOrDefault(c => c.Name == "CMP"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "DENIS", Company = companies.FirstOrDefault(c => c.Name == "Denis"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "DENDENTAL", Company = companies.FirstOrDefault(c => c.Name == "Denis"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "DISCHEALT", Company = companies.FirstOrDefault(c => c.Name == "Discovery"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "DISC", Company = companies.FirstOrDefault(c => c.Name == "Discovery"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "DISC SUPP", Company = companies.FirstOrDefault(c => c.Name == "Discovery"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "HPA ", Company = companies.FirstOrDefault(c => c.Name == "Discovery"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "DRC-", Company = companies.FirstOrDefault(c => c.Name == "DRC Medihelp"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "FDH", Company = companies.FirstOrDefault(c => c.Name == "Fedhealth"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "GEMSGOVMED", Company = companies.FirstOrDefault(c => c.Name == "Gems"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "Getsavvi", Company = companies.FirstOrDefault(c => c.Name == "Get Savvi"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "HOS", Company = companies.FirstOrDefault(c => c.Name == "Hosmed"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "IMPERIAL", Company = companies.FirstOrDefault(c => c.Name == "Imperial Med"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "Keyhealth", Company = companies.FirstOrDefault(c => c.Name == "Keyhealth"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "Malcor", Company = companies.FirstOrDefault(c => c.Name == "Malcor"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "MASSMARTMS", Company = companies.FirstOrDefault(c => c.Name == "Massmart"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "Massmart", Company = companies.FirstOrDefault(c => c.Name == "Massmart"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "MEDIHELP", Company = companies.FirstOrDefault(c => c.Name == "Medihelp"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "MSD", Company = companies.FirstOrDefault(c => c.Name == "Medshield"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "MOM HEALTH", Company = companies.FirstOrDefault(c => c.Name == "Momentum"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "Moto Health", Company = companies.FirstOrDefault(c => c.Name == "Moto Health"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "PG Group", Company = companies.FirstOrDefault(c => c.Name == "PG Group"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "POLMED", Company = companies.FirstOrDefault(c => c.Name == "Polmed"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "PROFMED", Company = companies.FirstOrDefault(c => c.Name == "Profmed"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "SELFMED", Company = companies.FirstOrDefault(c => c.Name == "Selfmed"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "SIZWEMED", Company = companies.FirstOrDefault(c => c.Name == "Sizwe"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "SPECTRAMED", Company = companies.FirstOrDefault(c => c.Name == "Spectramed"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "TRANSMED", Company = companies.FirstOrDefault(c => c.Name == "Transmed"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "TIGERBMS", Company = companies.FirstOrDefault(c => c.Name == "Tiger Brands"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "COMPCAREMS", Company = companies.FirstOrDefault(c => c.Name == "Compcare"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "KEYMED", Company = companies.FirstOrDefault(c => c.Name == "Keymed"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "MEDIMED", Company = companies.FirstOrDefault(c => c.Name == "Medimed"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "MOTOSUP", Company = companies.FirstOrDefault(c => c.Name == "Moto Health"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "MSD", Company = companies.FirstOrDefault(c => c.Name == "MSD"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "MALCOR", Company = companies.FirstOrDefault(c => c.Name == "Malcore"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "SASOL", Company = companies.FirstOrDefault(c => c.Name == "SASOL Med"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "SISONKE HESHMS", Company = companies.FirstOrDefault(c => c.Name == "Sisonke"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "STANCOM", Company = companies.FirstOrDefault(c => c.Name == "Stancom"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "REMEDISUPP", Company = companies.FirstOrDefault(c => c.Name == "Remedi"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "LIBCARE", Company = companies.FirstOrDefault(c => c.Name == "Lib Care"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "RESOMED", Company = companies.FirstOrDefault(c => c.Name == "Reso Med"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "SEDMED", Company = companies.FirstOrDefault(c => c.Name == "SED Med"), LookupType = contains}
				                                                   , new CompanyIdentifier {Expression = "HEALTH SAV", Company = companies.FirstOrDefault(c => c.Name == "Momentum Healthsaver"), LookupType = startsWith}
				                                                   , new CompanyIdentifier {Expression = "TMCLCR", Company = companies.FirstOrDefault(c => c.Name == "TopMed"), LookupType = startsWith}

				                                                     //new CompanyIdentifier { Expression = "DENIS", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Denis"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "LIBERTY", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Liberty"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "MEDICOVER", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Medicover"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "MOM HEALTH", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Momentum") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "MSD", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "MSD"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "NASPERS", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Naspers") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "AID-OM", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "AID-OM"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "POLMED", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Polmed") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "PROSANO", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Prosano") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "PUREHEALTH", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Pure Health") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "SAB MED", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "SAB MED"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "SPM", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "SPM"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "TIGERBRAND", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Tiger Brands"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "TELEMED", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Telemed") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "UMED", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "UMED") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "CMP", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "CMP") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "GEMSGOVMED", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "GEMSGOVMED") , LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "GENH", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "GENH"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "HOSMEDC", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "HOSMEDC"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "KEYHEALTH", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "KEYHEALTH"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "LAHEALTH", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "LAHEALTH"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "MAK", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "MAK"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "MEDCOR", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "MEDCOR"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "NIMAS", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "NIMAS"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "XXOC", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "XXOC"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "SAPPIMED", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "SAPPIMED") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "SMS", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "AFFINITY"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "DRC", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "SMS"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "AFFINITY", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "AFFINITY") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "BANKMED", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "BANKMED"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "SPECTRAMED", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "SPECTRAMED"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "DISC", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Discovery Health"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "DISCHEALT", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "Discovery Health"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "COMPCARE", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "COMPCARE") , LookupType = contains},
				                                                     //new CompanyIdentifier { Expression = "MEDIPOS", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "MEDIPOS"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "MASSMARTMS", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "MASSMART"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "BESTMED", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "BESTMED"), LookupType = contains },
				                                                     //new CompanyIdentifier { Expression = "MEDIHELP", Company = companies.FirstOrDefault(medicalCompany => medicalCompany.Name == "MEDIHELP"), LookupType = contains }
			                                                     };

			var paymentTypeIdentifiers = new List<PaymentTypeIdentifier> {
				                                                             new PaymentTypeIdentifier {Expression = "Cash", PaymentType = paymentTypes.FirstOrDefault(x => x.PaymentTypeCode == "Cash"), LookupType = contains}
				                                                           , new PaymentTypeIdentifier {Expression = " CC", PaymentType = paymentTypes.FirstOrDefault(x => x.PaymentTypeCode == "CC"), LookupType = endsWith}
				                                                           , new PaymentTypeIdentifier {Expression = " DD", PaymentType = paymentTypes.FirstOrDefault(x => x.PaymentTypeCode == "DD"), LookupType = endsWith}
				                                                           , new PaymentTypeIdentifier {Expression = "EFT", PaymentType = paymentTypes.FirstOrDefault(x => x.PaymentTypeCode == "EFT"), LookupType = contains}
				                                                           , new PaymentTypeIdentifier {Expression = "PAYMENT FROM", PaymentType = paymentTypes.FirstOrDefault(x => x.PaymentTypeCode == "EFT"), LookupType = contains}
				                                                           , new PaymentTypeIdentifier {Expression = "ACB CREDIT", PaymentType = paymentTypes.FirstOrDefault(x => x.PaymentTypeCode == "EFT"), LookupType = contains}
			                                                             };

			var banks = new List<Bank> {
				                           new Bank {Name = "ABSA", ImageSource = "Content//images//standardbank.png"}
				                         , new Bank {Name = "Capitec", ImageSource = "Content//images//capitec.png"}
				                         , new Bank {Name = "FNB", ImageSource = "Content//images//fnb.png"}
				                         , new Bank {Name = "Nedbank", ImageSource = "Content//images//nedbank.png"}
				                         , new Bank {Name = "Standard Bank", ImageSource = "Content//images//standardbank.png"}
			                           };

			var retailers = new List<Retailer> {
				                                   new Retailer {RetailerName = "Caltex"}
				                                 , new Retailer {RetailerName = "Checkers"}
				                                 , new Retailer {RetailerName = "Builders Warehouse"}
				                                 , new Retailer {RetailerName = "Dis-Chem"}
				                                 , new Retailer {RetailerName = "Engen"}
				                                 , new Retailer {RetailerName = "MBT"}
				                                 , new Retailer {RetailerName = "Pick & Pay"}
				                                 , new Retailer {RetailerName = "Spar"}
				                                 , new Retailer {RetailerName = "KFC"}
				                                 , new Retailer {RetailerName = "Liquor Store"}
				                                 , new Retailer {RetailerName = "Toll Gate"}
				                                 , new Retailer {RetailerName = "Pharmacy"}
				                                 , new Retailer {RetailerName = "Steers"}
				                                 , new Retailer {RetailerName = "Clicks"}
				                                 , new Retailer {RetailerName = "Wimpy"}
				                                 , new Retailer {RetailerName = "Twisp"}
				                                 , new Retailer {RetailerName = "Animal Kingdom"}
				                                 , new Retailer {RetailerName = "Barcelos"}
				                                 , new Retailer {RetailerName = "Gautrain"}
			                                   };

			var transactionTypes = new List<TransactionType> {
				                                                 new TransactionType {TransactionTypeName = "Bank Card Purchase"}
				                                               , new TransactionType {TransactionTypeName = "Bank Charges"}
				                                               , new TransactionType {TransactionTypeName = "Bank Transfer"}
				                                               , new TransactionType {TransactionTypeName = "Budget Item"}
				                                               , new TransactionType {TransactionTypeName = "Cash Withdrawal"}
				                                               , new TransactionType {TransactionTypeName = "Credits"}
				                                               , new TransactionType {TransactionTypeName = "Salaries"}
				                                               , new TransactionType {TransactionTypeName = "Debt Repayment"}
				                                               , new TransactionType {TransactionTypeName = "Fuel"}
				                                               , new TransactionType {TransactionTypeName = "Failed TransactionEntry"}
				                                               , new TransactionType {TransactionTypeName = "Failed TransactionEntry Reversal"}
				                                               , new TransactionType {TransactionTypeName = "Medical Payments"}
			                                                 };

			var budgetItemCategories = new List<BudgetItemCategory> {
				                                                        new BudgetItemCategory {BudgetItemCategoryName = "Banking"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Charities"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Education"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Entertainment"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Financial Services"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Family And Friends"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "General"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Health And Beauty"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Holiday Expenses"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Household Maintenance"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Insurance"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Internet Service Providers"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Investments"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Legal Services"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Medical"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Motoring"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Municipalities"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "OffShore"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Personal Services"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Property"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Rental Agencies"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Rent / Levies"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Retailers"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Salaries"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Scheduled Pre-paid"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Security Services"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Staff Costs"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Tax"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Telecommunications"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Traffic Departments"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Transport"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Travel"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Unemployment Insurance"}
				                                                      , new BudgetItemCategory {BudgetItemCategoryName = "Utilities"}
			                                                        };

			context.Banks.AddRange(banks);
			context.Retailers.AddRange(retailers);
			context.TransactionTypes.AddRange(transactionTypes);
			context.BudgetItemCategories.AddRange(budgetItemCategories);
			
			context.Customers.Add(new Customer {CustomerName = "1.Solumed"});
			context.AccountTypes.AddRange(accountTypes);
			context.Accounts.AddRange(accounts);
			context.CleanupExpressions.AddRange(expressions);
			context.LookupTypes.AddRange(lookupTypes);

			context.PaymentTypes.AddRange(paymentTypes);
			context.Companies.AddRange(companies);
			context.CompanyIdentifiers.AddRange(companyIdentifiers);
			context.PaymentTypeIdentifierIdentifiers.AddRange(paymentTypeIdentifiers);

			Log.Info("Seed Completed");
		}
	}
}