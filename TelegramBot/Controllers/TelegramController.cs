using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using TelegramBot.Models;
using static System.Net.WebRequestMethods;

namespace TelegramBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController
    {
        SmartMarket_dbContext my_db = new SmartMarket_dbContext();
        [HttpPost]
        public async Task<IResult> Post([FromBody] Update update)
        {
            TelegramBotClient client = new TelegramBotClient("5807294714:AAGpg99VHhGp9zHGz3FOeS7_A8UrUZTsTak");
            if (update != null)
            {
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    int f = 0;

                    if(update.Message.Text.Contains("gadgets"))

                        {
                        foreach (var item in my_db.Products)
                        {
                           
                             await client.SendTextMessageAsync(update.Message.From.Id, $"{item.Name} {item.Price}");
                               
                        }
                    }

                        else
                    {
                        if (update.Message.Text.Contains("Model"))

                        {

                            if (update.Message.Text.Contains("Price"))
                            {
                                string[] cut = update.Message.Text.Split(',');
                                string model = cut[0].Substring(update.Message.Text.IndexOf(' ') + 1);

                                string price = cut[1].Substring(update.Message.Text.IndexOf(' ') + 1);
                                string[] range = price.Split('-');

                                foreach (var item in my_db.Products)
                                {
                                    if (item.Name.ToLower().Contains(model.ToLower()))
                                    {

                                        if (item.Price >= Convert.ToDouble(range[0]) && item.Price <= Convert.ToDouble(range[1]))
                                        {
                                            await client.SendTextMessageAsync(update.Message.From.Id, $"{item.Name} {item.Price}");
                                            f = 1;
                                        }
                                    }



                                }
                            }
                            else
                            {
                                string model = update.Message.Text.Substring(update.Message.Text.IndexOf(' ') + 1);

                                foreach (var item in my_db.Products)
                                {
                                    if (item.Name.ToLower().Contains(model.ToLower()))
                                    {
                                        await client.SendTextMessageAsync(update.Message.From.Id, $"{item.Name} {item.Price}");
                                        f = 1;
                                    }



                                }
                            }
                        }
                        else if (update.Message.Text.Contains("Price"))
                        {
                            string price = update.Message.Text.Substring(update.Message.Text.IndexOf(' ') + 1);
                            string[] range = price.Split('-');



                            foreach (var item in my_db.Products)
                            {
                                if (item.Price >= Convert.ToDouble(range[0]) && item.Price <= Convert.ToDouble(range[1]))
                                {
                                    await client.SendTextMessageAsync(update.Message.From.Id, $"{item.Name} {item.Price}");
                                    f = 1;
                                }



                            }
                        }





                        if (f == 0)
                        {
                            await client.SendTextMessageAsync(update.Message.From.Id, "Nothing of search");
                        }

                    }

                }
            }
            return Results.Ok();
        }
    }
}
