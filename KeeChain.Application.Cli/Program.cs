using KeeChain.Warlin.Requests;
using KeeChain.Warlin.Responses;
using KeeChain.Warlin.Serials;
using KeeChain.Warlin.Services;

var warlin = new WarlinService(new DefaultSerialPort());

var connectResult = warlin.TryConnectToBoard();

if (!connectResult)
{
    Console.WriteLine("Не удалось найти плату");
    return;
}

var discover = await warlin.SendRequestAsync<DiscoverRequest, ACKResponse>(new());
if (!discover.IsError)
{
    Console.WriteLine($"{discover.Response.IdentifyingToken} получен");
}

var syncr = await warlin.SendRequestAsync<SyncRequest, SyncResponse>(new());
if ()
Console.WriteLine($"{syncr.IdentifyingToken} получен");

var response = await warlin.SendRequestAsync<EEPROMHeaderRequest, ServiceResponse>(new());
Console.WriteLine($"{response.IdentifyingToken} получен");