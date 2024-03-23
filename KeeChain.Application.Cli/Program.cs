using KeeChain.Warlin;
using KeeChain.Warlin.Serials;

var warlin = new Warlin(new DefaultSerialPort());

var result = warlin.TryConnectToBoard();

Console.WriteLine(result ? "Успех" : "Неудача");