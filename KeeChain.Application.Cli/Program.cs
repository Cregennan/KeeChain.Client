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

var discover = await warlin.SendRequestAsync<DiscoverRequest, AckResponse>(new());
if (!discover.IsError)
{
    Console.WriteLine($"{discover.Response.IdentifyingToken} получен");
}

var otpResponse = await warlin.SendRequestAsync<TestExplicitCodeRequest, OtpResponse>(new("JBSWY3DPEHPK3PXP"));
if (!otpResponse.IsError)
{
    Console.WriteLine($"Сгенерированный код: {otpResponse.Response.Code}");
}