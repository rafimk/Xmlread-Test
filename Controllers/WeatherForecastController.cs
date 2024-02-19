using Microsoft.AspNetCore.Mvc;
using Xmlread_Test.Models;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Xmlread_Test.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("GetDeserialize")]
    public ActionResult GetDeserialize()
    {
         string xmlData = @"<PutAwayConfirmationMessageRequest>
                              <messageHeader>
                                  <msgId>9913</msgId>
                                  <msgType>TT</msgType>
                              </messageHeader>
                              <siteDetails>
                                  <stroreCode>ABC</stroreCode>
                              </siteDetails>
                              <tuId>ADSFS</tuId>
                              <asnLine>
                                  <lineNumber>1</lineNumber>
                                  <tuid />
                                  <quantityReceived>1</quantityReceived>
                                  <quantityExprected>1</quantityExprected>
                                  <attributesValues>
                                      <name>AS_BIN</name>
                                      <value>2323</value>
                                  </attributesValues>
                                  <attributesValues>
                                      <name>TYPE</name>
                                      <value>GG</value>
                                  </attributesValues>
                              </asnLine>
                          </PutAwayConfirmationMessageRequest>";

        XmlSerializer serializer = new XmlSerializer(typeof(PutAwayConfirmationMessageRequest));

        using (StringReader reader = new StringReader(xmlData))
        {
            PutAwayConfirmationMessageRequest request = (PutAwayConfirmationMessageRequest)serializer.Deserialize(reader);

            // Access deserialized data
            Console.WriteLine($"Message ID: {request.messageHeader.msgId}");
            Console.WriteLine($"Message Type: {request.messageHeader.msgType}");
            Console.WriteLine($"Store Code: {request.siteDetails.stroreCode}");
            Console.WriteLine($"TU ID: {request.tuId}");

            foreach (AsnLine asnLine in request.asnLine)
            {
                Console.WriteLine($"Line Number: {asnLine.lineNumber}");
                Console.WriteLine($"TUID: {asnLine.tuid}");
                Console.WriteLine($"Quantity Received: {asnLine.quantityReceived}");
                Console.WriteLine($"Quantity Expected: {asnLine.quantityExprected}");

                foreach (AttributesValues attribute in asnLine.attributesValues)
                {
                    Console.WriteLine($"Attribute Name: {attribute.name}");
                    Console.WriteLine($"Attribute Value: {attribute.value}");
                }
            }
        }
    }
}
