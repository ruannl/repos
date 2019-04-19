using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Ruann.Linde.Database;
using Ruann.Linde.Database.Models;

namespace Ruann.Linde.Controllers {
	public class PaymentTypesController : ApiController {

		private readonly ApplicationDatabaseContext _applicationDatabaseContext = new ApplicationDatabaseContext();

		// GET: api/PaymentTypes
		public IQueryable<PaymentType> GetPaymentTypes() {
			return _applicationDatabaseContext.PaymentTypes;
		}

		// GET: api/PaymentTypes/5
		[ResponseType(typeof(PaymentType))]
		public async Task<IHttpActionResult> GetPaymentType(int id) {
			var paymentType = await _applicationDatabaseContext.PaymentTypes.FindAsync(id);
			if (paymentType == null) {
				return NotFound();
			}

			return Ok(paymentType);
		}

		// PUT: api/PaymentTypes/5
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> PutPaymentType(int id, PaymentType paymentType) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (id != paymentType.PaymentTypeId) {
				return BadRequest();
			}

			_applicationDatabaseContext.Entry(paymentType).State = EntityState.Modified;

			try {
				await _applicationDatabaseContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) {
				if (!PaymentTypeExists(id)) {
					return NotFound();
				}
				else {
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/PaymentTypes
		[ResponseType(typeof(PaymentType))]
		public async Task<IHttpActionResult> PostPaymentType(PaymentType paymentType) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			_applicationDatabaseContext.PaymentTypes.Add(paymentType);
			await _applicationDatabaseContext.SaveChangesAsync();

			return CreatedAtRoute("DefaultApi", new { id = paymentType.PaymentTypeId }, paymentType);
		}

		// DELETE: api/PaymentTypes/5
		[ResponseType(typeof(PaymentType))]
		public async Task<IHttpActionResult> DeletePaymentType(int id) {
			var paymentType = await _applicationDatabaseContext.PaymentTypes.FindAsync(id);
			if (paymentType == null) {
				return NotFound();
			}

			_applicationDatabaseContext.PaymentTypes.Remove(paymentType);
			await _applicationDatabaseContext.SaveChangesAsync();

			return Ok(paymentType);
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				_applicationDatabaseContext.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool PaymentTypeExists(int id) {
			return _applicationDatabaseContext.PaymentTypes.Count(e => e.PaymentTypeId == id) > 0;
		}
	}
}