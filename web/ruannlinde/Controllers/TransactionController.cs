﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Ruann.Linde.Database;
using Ruann.Linde.Database.Models;

namespace Ruann.Linde.Controllers {
	public class TransactionController : ApiController {
		private ApplicationDatabaseContext db = new ApplicationDatabaseContext();

		// GET: api/Transactions
		public IQueryable<Transaction> GetTransactions() {
			return db.Transactions;
		}

		// GET: api/Transactions/5
		[ResponseType(typeof(Transaction))]
		public async Task<IHttpActionResult> GetTransaction(int id) {
			var transaction = await db.Transactions.FindAsync(id);
			if (transaction == null) {
				return NotFound();
			}

			return Ok(transaction);
		}

		// PUT: api/Transactions/5
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> PutTransaction(int id, Transaction transaction) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (id != transaction.TransactionId) {
				return BadRequest();
			}

			db.Entry(transaction).State = EntityState.Modified;

			try {
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) {
				if (!TransactionExists(id)) {
					return NotFound();
				}
				else {
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Transactions
		[ResponseType(typeof(Transaction))]
		public async Task<IHttpActionResult> PostTransaction(Transaction transaction) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			db.Transactions.Add(transaction);
			await db.SaveChangesAsync();

			return CreatedAtRoute("DefaultApi", new { id = transaction.TransactionId }, transaction);
		}

		// DELETE: api/Transactions/5
		[ResponseType(typeof(Transaction))]
		public async Task<IHttpActionResult> DeleteTransaction(int id) {
			var transaction = await db.Transactions.FindAsync(id);
			if (transaction == null) {
				return NotFound();
			}

			db.Transactions.Remove(transaction);
			await db.SaveChangesAsync();

			return Ok(transaction);
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TransactionExists(int id) {
			return db.Transactions.Count(e => e.TransactionId == id) > 0;
		}
	}
}