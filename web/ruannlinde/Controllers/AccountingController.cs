using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;

namespace RL.Controllers
{
    public class AccountingController : ApiController
    {
        //private readonly AccountingManager _accountingManager;
        private readonly ILog _log;

        public AccountingController(ILog log) {
            _log = log;
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            _log.Info("test");
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}