using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using BlazorControlDeGastos.BOT.Servicios.Implementaciones;
using BlazorControlDeGastos.Model;

var botClient = new TelegramBotClient("6116855642:AAFx53APp0zLZuHr0DfXFm2y57T1WjJ-3NY");

var nuevoGasto = new Gastos();

var baseAddress = new Uri("https://localhost:7111/"); 

var gastosServicio = new GastosServicio(new HttpClient { BaseAddress = baseAddress });

var categoriasServicio = new CategoriaServicio(new HttpClient { BaseAddress = baseAddress });

using CancellationTokenSource cts = new();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    var idCategoriaElegeida = 0;
    // Only process CallbackQuery updates
    if (update.CallbackQuery is { } callbackQuery)
    {
        var callbackChatId = callbackQuery.Message.Chat.Id;
        var opcionElegida = callbackQuery.Data;

        var categoriasPorFila = 3;

        var categorias = await categoriasServicio.GetAllCategorias();
        switch (opcionElegida)
        {
            case "1. Registrar Gasto":
                nuevoGasto.TipoGasto = TipoGasto.Gasto;
                var gruposCategorias = categorias.Select((categoria, index) => new { Categoria = categoria, Grupo = index / categoriasPorFila })
                                                 .GroupBy(item => item.Grupo)
                                                 .Select(group => group.Select(item => item.Categoria));

                var inlineKeyboardRows = gruposCategorias.Select(categoriasGrupo =>
                {
                    return categoriasGrupo.Select(categoria => InlineKeyboardButton.WithCallbackData(categoria.DescCategoria))
                                         .ToArray();
                }).ToArray();

                var inlineKeyboard = new InlineKeyboardMarkup(inlineKeyboardRows);

                // Se muestran las categorias                
                await botClient.SendTextMessageAsync(callbackChatId, "Seleccione una categoría", replyMarkup: inlineKeyboard);
                break;
            case "2. Registrar Ingreso":
                nuevoGasto.TipoGasto = TipoGasto.Ingreso;
                var gruposCategorias2 = categorias.Select((categoria, index) => new { Categoria = categoria, Grupo = index / categoriasPorFila })
                                                 .GroupBy(item => item.Grupo)
                                                 .Select(group => group.Select(item => item.Categoria));

                var inlineKeyboardRows2 = gruposCategorias2.Select(categoriasGrupo =>
                {
                    return categoriasGrupo.Select(categoria => InlineKeyboardButton.WithCallbackData(categoria.DescCategoria))
                                         .ToArray();
                }).ToArray();

                var inlineKeyboard2 = new InlineKeyboardMarkup(inlineKeyboardRows2);

                // Se muestran las categorias                
                await botClient.SendTextMessageAsync(callbackChatId, "Seleccione una categoría", replyMarkup: inlineKeyboard2);
                break;
            case "3. Consultar Balance":
                var balance = await gastosServicio.GetBalance();
                await botClient.SendTextMessageAsync(callbackChatId, $"Tu balance es de: {balance} 🤑");
                break;
            case "4. Consultar Gastos":
                var gastos = await gastosServicio.GetAllGastos();
                var gastosString = ""; 
                foreach (var gasto in gastos)
                {
                    gastosString += $"🗓 {gasto.Fecha.ToShortDateString()} - 💵 {gasto.Monto} - 📑 {gasto.Categoria.DescCategoria} \n\n";
                }
                await botClient.SendTextMessageAsync(callbackChatId, $"Tus gastos son:\n\n{gastosString}");
                break;
            case "5. Consultar Ingresos":
                var ingresos = await gastosServicio.GetAllIngresos();
                var ingresosString = "";
                foreach (var ingreso in ingresos)
                {
                    ingresosString += $"🗓 {ingreso.Fecha.ToShortDateString()} - 💵 {ingreso.Monto} - 📑 {ingreso.Categoria.DescCategoria} \n\n";
                }
                await botClient.SendTextMessageAsync(callbackChatId, $"Tus ingresos son:\n\n{ingresosString}");
                break;
            case "6. Salir":
                // Lógica para salir o finalizar el bot
                break;
            // Case para las categorias, usando la lista categorias
            case var categoria when categorias.Any(c => c.DescCategoria == categoria):
                // Informar que se eligió la categoria
                await botClient.SendTextMessageAsync(callbackChatId, $"Seleccionaste la categoria {categoria}");
                idCategoriaElegeida = categorias.First(c => c.DescCategoria == categoria).Id;

                // Se envia un mensaje para que ingrese el gasto, el proximo mensaje debe ser un gasto
                await botClient.SendTextMessageAsync(callbackChatId, $"Envie el monto del {nuevoGasto.TipoGasto} con el simbolo $");

                nuevoGasto.Fecha = DateTime.Now;

                nuevoGasto.CategoriaId = idCategoriaElegeida;
                



                break;
           
        }

        return;
    }

    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;

    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    // Start command
    if (messageText.StartsWith("/start") || messageText.StartsWith("Menu"))
    {
        var keyboard = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("1. Registrar Gasto"),
                InlineKeyboardButton.WithCallbackData("2. Registrar Ingreso"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("3. Consultar Balance"),
                InlineKeyboardButton.WithCallbackData("4. Consultar Gastos"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("5. Consultar Ingresos"),
                InlineKeyboardButton.WithCallbackData("6. Salir"),
            },
        });

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Bienvenido al bot de control de gastos e ingresos. Selecciona una opción",
            replyMarkup: keyboard
        );
    } else
    {
        // Mensaje comienza con $ y es un gasto
        if (messageText.StartsWith("$"))
        {
            var monto = Convert.ToDecimal(messageText.Substring(1));
            nuevoGasto.Monto = monto;
            ProcesarGasto(chatId, nuevoGasto, botClient, update, cancellationToken);
        }
    }

    


}

async void ProcesarGasto(long callbackChatId, Gastos gasto, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{



    
    var chatId = callbackChatId;

    await gastosServicio.SaveGasto(gasto);
    botClient.SendTextMessageAsync(callbackChatId, $"Se registro el {gasto.TipoGasto} de {gasto.Monto}");

}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
