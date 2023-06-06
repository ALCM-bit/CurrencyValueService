using CoinValue.Services;

RequestCentralBankAPIService request = new RequestCentralBankAPIService();

var result = await request.GetCurenci("EUR");

Console.WriteLine(result.cotacaoCompra);