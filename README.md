# UptimeRobot2TelegramAzureFunction

Azure Function to publish messages from UptimeRobot.com to Telegram (bot required)

1. Clone this repo
2. Create your Telegram bot ([manual](https://core.telegram.org/bots#6-botfather))
3. Add this bot to group you want send messages to, and [obtain chat_id](http://stackoverflow.com/questions/32423837/)  of required chat;
4. Create Azure Function from Git repo ([manual](https://docs.microsoft.com/en-us/azure/azure-functions/functions-continuous-deployment))
5. Create these App Settings for your Function app:
    * `TelegramBotToken` - token for your bot (from step 2), like `12345:ABCDEF...`
    * `TelegramChatId` - chat_id from step 3
