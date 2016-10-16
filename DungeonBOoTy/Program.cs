using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace DungeonBOoTy
{
    class Program
    {
        private static ITelegramBotClient bot;
        private static Mysql mysql = new Mysql();

        static void Main(string[] args)
        {
            string token = "299216895:AAEqE71KPfWPGb0qklr0VrHq8BccSnSrB2c";
            bot = new TelegramBotClient(token);
            bot.OnMessage += BotOnMessageRecieve;
            bot.OnReceiveError += BotOnReceiveError;
            bot.OnMessageEdited += BotOnMessageRecieve;
            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();
        }



        private static void BotOnMessageRecieve(object sender, MessageEventArgs meargs)
        {
            var message = meargs.Message;
            if (message.Text != null)
            {

                Console.WriteLine("Mensaje recibido: " + message.Text);

                if (message.Text.Equals("/help"))
                {
                    string help = "Bienvenido a Dungeon&Booty. Los comandos actuales son los siguientes: \n"
                         + "/createCharacter - crea tu personaje si no lo tenías creado antes  \n"
                         +"/showCharacter - Muestra el los datos de tu personaje  \n"
                         + "/showStats - Muestra las estadísticas de tu personaje  \n"
                         + "/changeName - Modifica el nombre de tu personaje  \n"
                         + "/changeDescription - Modifica la descripción de tu personaje  \n";
                    SendMessage(message.Chat.Id, help);
                    
                }
                if (message.Text.Equals("/createCharacter"))
                {
                    var character = new Character();
                    if (!mysql.ExistsCharacter(message.From.Id))
                    {
                        mysql.InsertCharacter(character, message.From.Id);
                        SendMessage(message.Chat.Id, "personaje creado");

                    }
                    else
                    {
                        SendMessage(message.Chat.Id, "ya tiene creado un personaje");
                    }
                }
                if (message.Text.Contains("/changeName"))
                {
                    var name = message.Text.Split(' ');
                    if (name.Last().Equals("/changeName"))
                    {
                        SendMessage(message.Chat.Id, "No ha introducido el nombre");
                    }
                    else if (!mysql.ExistsCharacter(message.From.Id))
                    {
                        SendMessage(message.Chat.Id, "No existe su personaje");
                    }
                    else
                    {
                        string n = name.Last();
                        mysql.UpdateNameCharacter(n, message.From.Id);
                        SendMessage(message.Chat.Id, "Nombre modificado a: " + n);
                    }
                }
                if (message.Text.Equals("/showCharacter"))
                {
                    if (mysql.ExistsCharacter(message.From.Id))
                    {
                        var character = mysql.ReadCharacter(message.From.Id);
                        SendMessage(message.Chat.Id, character.ToString());
                    }
                    else
                    {
                        SendMessage(message.Chat.Id, "no tiene personaje creado");
                    }
                }

                if (message.Text.Equals("/showStats"))
                {
                    if (mysql.ExistsCharacter(message.From.Id))
                    {
                        var character = mysql.ReadCharacter(message.From.Id);
                        SendMessage(message.Chat.Id, character.Stats());
                    }
                    else
                    {
                        SendMessage(message.Chat.Id, "no tiene personaje creado");
                    }
                }
                if (message.Text.Contains("/changeDescription"))
                {
                    var name = message.Text.Split(' ');
                    if (name.Last().Equals("/changeDescription"))
                    {
                        SendMessage(message.Chat.Id, "No ha introducido el nombre");
                    }
                    else if (!mysql.ExistsCharacter(message.From.Id))
                    {
                        SendMessage(message.Chat.Id, "No existe su personaje");
                    }
                    else
                    {
                        string n = "";
                        foreach(var st in name)
                        {
                            if (!st.Equals("/changeDescription"))
                            {
                                n += " " + st;
                            }
                        }
                        mysql.UpdateDescriptionCharacter(n, message.From.Id);
                        SendMessage(message.Chat.Id, "Descripción modificada: ");
                    }
                }
            }
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Debugger.Break();
        }

        private static void SendMessage(long id, string text){
            bot.SendTextMessageAsync(id, text);
        }
    }
}
