using KeeChain.Warlin.Requests;
using KeeChain.Warlin.Responses;
using KeeChain.Warlin.Serials;
using KeeChain.Warlin.Services;

var warlin = new WarlinService(new DefaultSerialPort());

var result = warlin.TryConnectToBoard();

if (!result)
{
    Console.WriteLine("Не удалось найти плату");
    return;
}

var response = await warlin.SendRequestAsync<DiscoverRequest, DiscoverResponse>(new DiscoverRequest());

Console.WriteLine("Удалось получить данные");