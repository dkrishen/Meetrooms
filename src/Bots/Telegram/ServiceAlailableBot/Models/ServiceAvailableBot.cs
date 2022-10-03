using ServiceAlailable.BOT.Logger;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ServiceAlailable.BOT.Models
{
    public class ServiceAvailableBot
    {
        private string token;
        private HashSet<long> subscribeChatIds;
        private TelegramBotClient botClient;
        private WebClient webClient;
        private string serviceAddress;
        private int ObserveTimespan;

        private ILogger logger;

        private bool isObserve;

        public ServiceAvailableBot(string token, string serviceAddress, int observeTimespan)
        {
            Configure(token, serviceAddress, observeTimespan);
        }
         
        public void Configure(string token, string serviceAddress, int observeTimespan)
        {
            this.token = token;
            this.serviceAddress = serviceAddress;
            this.subscribeChatIds = new HashSet<long>();
            this.botClient = new TelegramBotClient(token);
            this.webClient = new WebClient();
            this.ObserveTimespan = observeTimespan;

            isObserve = false;
        }

        public async Task StartObserveAsync()
        {
            isObserve = true;

            while (isObserve)
            {
                var isAvalible = IsAddressAvailable(serviceAddress);
                logger.log("Server is avalible: " + isAvalible.ToString());

                if (!isAvalible)
                {
                    await sendTextToSubscribersAsync("Service is not avalible ❗️❗️❗️");
                }

                Thread.Sleep(ObserveTimespan);
            }
        }

        private async Task sendTextToSubscribersAsync(string Message)
        {
            foreach (var chatId in subscribeChatIds)
            {
                await sendTextToChatAsync(chatId, Message);
            }
        }

        private async Task sendTextToChatAsync(long chatId, string message)
        {
            await botClient.SendTextMessageAsync(chatId, message);
        }

        public void Run()
        {
            botClient.StartReceiving(updateHandler, pullingErrorHandler);
        }

        private async Task updateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message != null)
            {
                switch (message.Text.ToLower().Trim())
                {
                    case "subscribe":
                        subscribeChatIds.Add(message.Chat.Id);
                        await sendTextToChatAsync(message.Chat.Id, "You have successfully subscribed 💖");
                        logger.log(message.Chat.Username + " is subscribed");
                        break;
                    case "unsubscribe":
                        subscribeChatIds.Remove(message.Chat.Id);
                        await sendTextToChatAsync(message.Chat.Id, "You have successfully unsubscribed 💔");
                        logger.log(message.Chat.Username + " is unsubscribed");
                        break;
                    case "check":
                        var isAvalible = IsAddressAvailable(serviceAddress);
                        await client.SendTextMessageAsync(message.Chat.Id, "Service is " + (isAvalible ? "avalible ✅" : "not avalible ❌"));
                        logger.log(message.Chat.Username + " is checked");
                        break;
                    case "help":
                        await client.SendTextMessageAsync(message.Chat.Id, "Commands: \n\tSubscribe\n\tUnsubscribe\n\tCheck\n");
                        break;
                    default:
                        await client.SendTextMessageAsync(message.Chat.Id, "I don't know what is \"" + message.Text + "\" 🤓");
                        break;
                }
            }
        }

        private async static Task pullingErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private bool IsAddressAvailable(string address)
        {
            try
            {
                webClient.DownloadData(address);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddLoggger(ILogger logger)
        {
            this.logger = logger;
        }
    }
}
