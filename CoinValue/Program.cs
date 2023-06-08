using CoinValue.Services;

RequestCentralBankAPIService request = new RequestCentralBankAPIService();

var result = await request.GetAllCurenci();

foreach (var curenci in result)
{
    Console.WriteLine($"{curenci.cotacaoVenda}, {curenci.cotacaoCompra}, {curenci.dataHoraCotacao}");
}