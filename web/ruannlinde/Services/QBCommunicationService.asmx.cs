namespace RL.Services {
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Web.Script.Services;
    using System.Web.Services;
    using System.Xml;

    using log4net;
    using log4net.Config;

    [WebService(Namespace = "http://developer.intuit.com/" , Name = "QBCommunicationService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class QBCommunicationService : WebService {

        private readonly ILog logger;
        private readonly ArrayList requests = new ArrayList();
        private int count;

        public QBCommunicationService() {
            XmlConfigurator.Configure();
            this.logger = LogManager.GetLogger(typeof(QBCommunicationService));
        }

        /// <summary>
        ///     WebMethod - authenticate()
        ///     To verify username and password for the web connector that is trying to connect
        ///     Signature: public string[] authenticate(string strUserName, string strPassword)
        ///     IN:
        ///     string strUserName
        ///     string strPassword
        ///     OUT:
        ///     string[] authReturn
        ///     Possible values:
        ///     string[0] = ticket
        ///     string[1]
        ///     - empty string = use current company file
        ///     - "none" = no further request/no further action required
        ///     - "nvu" = not valid user
        ///     - any other string value = use this company file
        /// </summary>
        [WebMethod]
        public string[] authenticate(string strUserName, string strPassword) {
            var id = Guid.NewGuid().ToString();
            this.logEvent($"WebMethod: authenticate({strUserName},{strPassword}) returns: {id}");

            return new[] {
                             id, string.Empty
                         };
        }

        private ArrayList buildRequest() {
            var strRequestXML = string.Empty;
            XmlDocument inputXMLDoc = null;

            // CustomerQuery
            inputXMLDoc = new XmlDocument();
            inputXMLDoc.AppendChild(
                inputXMLDoc.CreateXmlDeclaration(
                    "1.0"
                    , null
                    , null));
            inputXMLDoc.AppendChild(
                inputXMLDoc.CreateProcessingInstruction(
                    "qbxml"
                    , "version=\"4.0\""));

            var qbXML = inputXMLDoc.CreateElement("QBXML");
            inputXMLDoc.AppendChild(qbXML);
            var qbXMLMsgsRq = inputXMLDoc.CreateElement("QBXMLMsgsRq");
            qbXML.AppendChild(qbXMLMsgsRq);
            qbXMLMsgsRq.SetAttribute(
                "onError"
                , "stopOnError");
            var customerQueryRq = inputXMLDoc.CreateElement("CustomerQueryRq");
            qbXMLMsgsRq.AppendChild(customerQueryRq);
            customerQueryRq.SetAttribute(
                "requestID"
                , "1");
            var maxReturned = inputXMLDoc.CreateElement("MaxReturned");
            customerQueryRq.AppendChild(maxReturned).InnerText = "1";

            strRequestXML = inputXMLDoc.OuterXml;
            this.requests.Add(strRequestXML);

            this.logEvent($"custom query {strRequestXML}");

            // Clean up
            strRequestXML = string.Empty;
            inputXMLDoc = null;
            qbXML = null;
            qbXMLMsgsRq = null;
            maxReturned = null;

            // InvoiceQuery
            inputXMLDoc = new XmlDocument();
            inputXMLDoc.AppendChild(
                inputXMLDoc.CreateXmlDeclaration(
                    "1.0"
                    , null
                    , null));
            inputXMLDoc.AppendChild(
                inputXMLDoc.CreateProcessingInstruction(
                    "qbxml"
                    , "version=\"4.0\""));

            qbXML = inputXMLDoc.CreateElement("QBXML");
            inputXMLDoc.AppendChild(qbXML);
            qbXMLMsgsRq = inputXMLDoc.CreateElement("QBXMLMsgsRq");
            qbXML.AppendChild(qbXMLMsgsRq);
            qbXMLMsgsRq.SetAttribute(
                "onError"
                , "stopOnError");
            var invoiceQueryRq = inputXMLDoc.CreateElement("InvoiceQueryRq");
            qbXMLMsgsRq.AppendChild(invoiceQueryRq);
            invoiceQueryRq.SetAttribute(
                "requestID"
                , "2");
            maxReturned = inputXMLDoc.CreateElement("MaxReturned");
            invoiceQueryRq.AppendChild(maxReturned).InnerText = "1";

            strRequestXML = inputXMLDoc.OuterXml;
            //this.requests.Add(strRequestXML);
            //this.logEvent($"invoice query {strRequestXML}");

            // Clean up
            strRequestXML = string.Empty;
            inputXMLDoc = null;
            qbXML = null;
            qbXMLMsgsRq = null;
            maxReturned = null;

            // BillQuery
            inputXMLDoc = new XmlDocument();
            inputXMLDoc.AppendChild(
                inputXMLDoc.CreateXmlDeclaration(
                    "1.0"
                    , null
                    , null));
            inputXMLDoc.AppendChild(
                inputXMLDoc.CreateProcessingInstruction(
                    "qbxml"
                    , "version=\"4.0\""));

            qbXML = inputXMLDoc.CreateElement("QBXML");
            inputXMLDoc.AppendChild(qbXML);
            qbXMLMsgsRq = inputXMLDoc.CreateElement("QBXMLMsgsRq");
            qbXML.AppendChild(qbXMLMsgsRq);
            qbXMLMsgsRq.SetAttribute(
                "onError"
                , "stopOnError");
            var billQueryRq = inputXMLDoc.CreateElement("BillQueryRq");
            qbXMLMsgsRq.AppendChild(billQueryRq);
            billQueryRq.SetAttribute(
                "requestID"
                , "3");
            maxReturned = inputXMLDoc.CreateElement("MaxReturned");
            billQueryRq.AppendChild(maxReturned).InnerText = "1";

            strRequestXML = inputXMLDoc.OuterXml;
            //this.requests.Add(strRequestXML);
            //this.logEvent($"billing query {strRequestXML}");

            return this.requests;
        }

        /// <summary>
        ///     WebMethod - clientVersion()
        ///     To enable web service with QBWC version control
        ///     Signature: public string clientVersion(string strVersion)
        ///     IN:
        ///     string strVersion
        ///     OUT:
        ///     string errorOrWarning
        ///     Possible values:
        ///     string retVal
        ///     - NULL or emptyString = QBWC will let the web service update
        ///     - "E:any text" = popup ERROR dialog with any text
        ///     - abort update and force download of new QBWC.
        ///     - "W:any text" = popup WARNING dialog with any text
        ///     - choice to user, continue update or not.
        /// </summary>
        [WebMethod]
        public string clientVersion(string strVersion) {
            if(string.IsNullOrWhiteSpace(strVersion)) {
                this.logEvent($"WebMethod: clientVersion() has been called by QBCommunicationService({strVersion}) result: E: Invalid Version");

                return "E: Invalid Version version may nor be empty\r\n";
            }

            var version = this.ParseForVersion(strVersion);

            this.logEvent($"WebMethod: clientVersion({strVersion}) version:{version} returns: empty string");

            return string.Empty;
        }

        /// <summary>
        ///     WebMethod - closeConnection()
        ///     At the end of a successful update session, QBCommunicationService will call this web method.
        ///     Signature: public string closeConnection(string ticket)
        ///     IN:
        ///     string ticket
        ///     OUT:
        ///     string closeConnection result
        /// </summary>
        [WebMethod]
        public string closeConnection(string ticket) {
            this.logEvent($"WebMethod: closeConnection({ticket}) returns: OK");
            return "OK";
        }

        /// <summary>
        ///     WebMethod - connectionError()
        ///     To facilitate capturing of QuickBooks error and notifying it to web services
        ///     Signature: public string connectionError (string ticket, string hresult, string message)
        ///     IN:
        ///     string ticket = A GUID based ticket string to maintain identity of QBCommunicationService
        ///     string hresult = An HRESULT value thrown by QuickBooks when trying to make connection
        ///     string message = An error message corresponding to the HRESULT
        ///     OUT:
        ///     string retVal
        ///     Possible values:
        ///     - “done” = no further action required from QBCommunicationService
        ///     - any other string value = use this name for company file
        /// </summary>
        [WebMethod(
            Description = "This web method facilitates web service to handle connection error between QuickBooks and QBCommunicationService"
            , EnableSession = true)]
        public string connectionError(string ticket, string hresult, string message) {
            this.logEvent($"WebMethod: connectionError({ticket},{hresult},{message})");
            var evLogTxt = string.Empty;

            string retVal = null;

            // 0x80040400 - QuickBooks found an error when parsing the provided XML text stream. 
            const string QB_ERROR_WHEN_PARSING = "0x80040400";

            // 0x80040401 - Could not access QuickBooks.  
            const string QB_COULDNT_ACCESS_QB = "0x80040401";

            // 0x80040402 - Unexpected error. Check the qbsdklog.txt file for possible, additional information. 
            const string QB_UNEXPECTED_ERROR = "0x80040402";

            if(hresult.Trim().Equals(QB_ERROR_WHEN_PARSING)) {
                evLogTxt = evLogTxt + "HRESULT = " + hresult + "\r\n";
                evLogTxt = evLogTxt + "Message = " + message + "\r\n";
                retVal = "DONE";
            }
            else if(hresult.Trim().Equals(QB_COULDNT_ACCESS_QB)) {
                evLogTxt = evLogTxt + "HRESULT = " + hresult + "\r\n";
                evLogTxt = evLogTxt + "Message = " + message + "\r\n";
                retVal = "DONE";
            }
            else if(hresult.Trim().Equals(QB_UNEXPECTED_ERROR)) {
                evLogTxt = evLogTxt + "HRESULT = " + hresult + "\r\n";
                evLogTxt = evLogTxt + "Message = " + message + "\r\n";
                retVal = "DONE";
            }
            else {
                evLogTxt = evLogTxt + "HRESULT = " + hresult + "\r\n";
                evLogTxt = evLogTxt + "Message = " + message + "\r\n";
                evLogTxt = evLogTxt + "Sending DONE to stop.";
                retVal = "DONE";
            }

            evLogTxt = evLogTxt + "\r\n";
            evLogTxt = evLogTxt + "Return values: " + "\r\n";
            evLogTxt = evLogTxt + "string retVal = " + retVal + "\r\n";

            this.logEvent(evLogTxt);
            this.logEvent("return: DONE");

            return "DONE";
        }

        /// <summary>
        ///     WebMethod - getInteractiveURL()
        ///     Signature: public string getInteractiveURL(string wcTicket, string sessionID)
        ///     IN:
        ///     string wcTicket
        ///     string sessionID
        ///     OUT:
        ///     URL string
        ///     Possible values:
        ///     URL to a website
        /// </summary>
        [WebMethod]
        public string getInteractiveURL(string wcTicket, string sessionID) {
            this.logEvent($"WebMethod: getInteractiveURL({wcTicket},{sessionID}) returns: http://www.ruannlinde.co.za/intuit/");
            return @"http://www.ruannlinde.co.za/intuit/"; 
        }

        /// <summary>
        ///     WebMethod - getLastError()
        ///     Signature: public string getLastError(string ticket)
        ///     IN:
        ///     string ticket
        ///     OUT:
        ///     string retVal
        ///     Possible Values:
        ///     Error message describing last web service error
        /// </summary>
        [WebMethod]
        public string getLastError(string ticket) {
            this.logEvent($"WebMethod: getLastError({ticket}) returns: Interactive mode");

            return "Interactive mode";
        }

        /// <summary>
        ///     WebMethod - interactiveDone()
        ///     Signature: public string interactiveDone(string wcTicket)
        ///     IN:
        ///     string wcTicket
        ///     OUT:
        ///     string
        /// </summary>
        [WebMethod]
        public string interactiveDone(string wcTicket) {
            this.logEvent($"WebMethod: interactiveDone({wcTicket}) returns: empty string");
            return string.Empty;
        }

        /// <summary>
        ///     WebMethod - interactiveRejected()
        ///     Signature: public string interactiveRejected(string wcTicket, string reason)
        ///     IN:
        ///     string wcTicket
        ///     string reason
        ///     OUT:
        ///     string
        /// </summary>
        [WebMethod]
        public string interactiveRejected(string wcTicket, string reason) {
            this.logEvent($"WebMethod: interactiveRejected({wcTicket},{reason}) returns: empty string");

            return string.Empty;
        }

        private void logEvent(string logText) {
            try {
                this.logger.Info(logText);
            }
            catch(Exception exception) {
                this.logger.Error(exception.Message);
            }
        }

        private string ParseForVersion(string input) {
            string retVal;
            var version = new Regex(
                @"^(?<major>\d+)\.(?<minor>\d+)(\.\w+){0,2}$"
                , RegexOptions.Compiled);
            var versionMatch = version.Match(input);
            if(versionMatch.Success) {
                var major = versionMatch.Result("${major}");
                var minor = versionMatch.Result("${minor}");
                retVal = major + "." + minor;
            }
            else {
                retVal = input;
            }

            return retVal;
        }

        /// <summary>
        ///     WebMethod - receiveResponseXML()
        ///     Signature: public int receiveResponseXML(string ticket, string response, string hresult, string message)
        ///     IN:
        ///     string ticket
        ///     string response
        ///     string hresult
        ///     string message
        ///     OUT:
        ///     int retVal
        ///     Greater than zero  = There are more request to send
        ///     100 = Done. no more request to send
        ///     Less than zero  = Custom Error codes
        /// </summary>
        [WebMethod(
            Description = "This web method facilitates web service to receive response XML from QuickBooks via QBCommunicationService"
            , EnableSession = true)]
        public int receiveResponseXML(string ticket, string response, string hresult, string message) {
            this.logEvent($"WebMethod: receiveResponseXML({ticket},{response},{message}) returns: 0");

            return 0;
        }

        /// <summary>
        ///     WebMethod - sendRequestXML()
        ///     Signature: public string sendRequestXML(string ticket, string strHCPResponse, string strCompanyFileName,
        ///     string Country, int qbXMLMajorVers, int qbXMLMinorVers)
        ///     IN:
        ///     int qbXMLMajorVers
        ///     int qbXMLMinorVers
        ///     string ticket
        ///     string strHCPResponse
        ///     string strCompanyFileName
        ///     string Country
        ///     int qbXMLMajorVers
        ///     int qbXMLMinorVers
        ///     OUT:
        ///     string request
        ///     Possible values:
        ///     - “any_string” = Request XML for QBCommunicationService to process
        ///     - "" = No more request XML
        /// </summary>
        [WebMethod(Description = "This web method facilitates web service to send request XML to QuickBooks via QBCommunicationService", EnableSession = true)]
        public string sendRequestXML(string ticket, string strHCPResponse, string strCompanyFileName, string qbXMLCountry, int qbXMLMajorVers, int qbXMLMinorVers) {


            this.logEvent($"WebMethod: sendRequestXML({ticket},{strCompanyFileName},{qbXMLCountry},{qbXMLMajorVers},{qbXMLMinorVers}); returns: null");

            //if(this.Session["counter"] == null)
            //    this.Session["counter"] = 0;

            //var response = this.buildRequest();
            //var request = string.Empty;
            //var total = this.requests.Count;
            //this.count = Convert.ToInt32(this.Session["counter"]);
            //var evLogTxt = "";

            //if (this.count < total) {
            //    request = requests[this.count].ToString();
            //    evLogTxt = evLogTxt + "sending request no = " + (this.count + 1) + "\r\n";
            //    this.Session["counter"] = (int)this.Session["counter"] + 1;
            //}
            //else {
            //    this.count = 0;
            //    this.Session["counter"] = 0;
            //    request = string.Empty;
            //}

            //evLogTxt = evLogTxt + "\r\n";
            //evLogTxt = evLogTxt + "Return values: " + "\r\n";
            //evLogTxt = evLogTxt + "string request = " + request + "\r\n";

            //logEvent(evLogTxt);

            //return request;
            return "";

        }

        /// <summary>
        ///     WebMethod - serverVersion()
        ///     To enable web service with its version number returned back to QBWC
        ///     Signature: public string serverVersion()
        ///     OUT:
        ///     string
        ///     Possible values:
        ///     Version string representing server version
        /// </summary>
        [WebMethod(
            Description = "This web method facilitates web service to send request XML to QuickBooks via QBCommunicationService"
            , EnableSession = true)]
        public string serverVersion() {
            this.logEvent("WebMethod: serverVersion() returns: empty string");

            return "1.0.0";
        }
    }
}