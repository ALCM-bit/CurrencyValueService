using CoinValue.Services;

RequestCentralBankAPIService request = new RequestCentralBankAPIService();

var result = await request.GetAllCurenci();

TxtMaker maker = new TxtMaker();

maker.WriteData(result);