using System;
using System.Linq;
using System.Net;
using System.Text;
using NetTelegramBotApi;
using NetTelegramBotApi.Requests;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    // Sample "DOWN" request:
    //   http://posttestserver.com/data/2017/03/01/05.08.341025666869
    // Sample "UP" request:
    //   http://posttestserver.com/data/2017/03/01/05.09.34274176411

    var prms = req.GetQueryNameValuePairs().ToList();

    var monitorID = prms.FirstOrDefault(x => x.Key == "monitorID").Value; 
    var monitorURL = prms.FirstOrDefault(x => x.Key == "monitorURL").Value; 
    var monitorFriendlyName = prms.FirstOrDefault(x => x.Key == "monitorFriendlyName").Value;
    var alertType = prms.FirstOrDefault(x => x.Key == "alertType").Value; 
    var alertTypeFriendlyName = prms.FirstOrDefault(x => x.Key == "alertTypeFriendlyName").Value; 
    var alertDetails = prms.FirstOrDefault(x => x.Key == "alertDetails").Value; 
    var monitorAlertContacts = prms.FirstOrDefault(x => x.Key == "monitorAlertContacts").Value; 
    var alertDateTime = prms.FirstOrDefault(x => x.Key == "alertDateTime").Value; 

    var telegramBotToken = Environment.GetEnvironmentVariable("TelegramBotToken");
    var telegramChatId = Environment.GetEnvironmentVariable("TelegramChatId");
    
    var msgText = $"Unknown alertType: '{alertType}'";

    switch (alertType)
    {
        case "1": // down 
            msgText = $"\uD83D\uDC94 *{monitorFriendlyName}* is DOWN \r\n {monitorURL} \r\n {alertDetails}";
            break;
        case "2": // up
            msgText = $"\uD83D\uDC9A *{monitorFriendlyName}* is UP \r\n {monitorURL} \r\n {alertDetails}";
            break;
    }

    var bot = new TelegramBot(telegramBotToken);
    var msg = new SendMessage(long.Parse(telegramChatId), msgText) {
        ParseMode = SendMessage.ParseModeEnum.Markdown
    };
    await bot.MakeRequestAsync(msg);

    return req.CreateResponse(HttpStatusCode.OK);
}