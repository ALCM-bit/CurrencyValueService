using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Interfaces.Services;
using System.Timers;
using Serilog;

namespace WindowsServiceCurrencyValue
{
    public partial class MainService : ServiceBase
    {
        Timer timer = new Timer();
        private readonly ITextService _textService;
        private readonly IRequestCentralBankAPIService _apiService;

        public MainService(ITextService textService, IRequestCentralBankAPIService apiService)
        {
            _textService = textService;
            _apiService = apiService;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 7000; // número em milissegundos
            timer.Enabled = true;
        }
        protected override async void OnStop()
        {
            await _textService.WriteStopMessage();
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    var data = await _apiService.GetAllCurrencyData();
                    await _textService.WriteData(data);
                }
                catch (Exception ex)
                {
                    await _textService.WriteError();
                    Log.Error(ex, $"Erro: {ex.Message}");
                }
            });
        }
    }
}
