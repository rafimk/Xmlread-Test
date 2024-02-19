using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Xmlread_Test.Models;

[XmlRoot("PutAwayConfirmationMessageRequest")]
public class PutAwayConfirmationMessageRequest
{
    public MessageHeader messageHeader { get; set; }
    public SiteDetails siteDetails { get; set; }
    public string tuId { get; set; }
    public List<AsnLine> asnLine { get; set; }
}