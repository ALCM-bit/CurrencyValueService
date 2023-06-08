using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceCurrencyValue.Interfaces.Services;
using System.Timers;
using System.IO;

namespace WindowsServiceCurrencyValue
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        private readonly ITxtMaker _maker;
        private readonly IRequestCentralBankAPIService _apiService;

        public Service1(ITxtMaker maker, IRequestCentralBankAPIService apiService)
        {
            _maker = maker;
            _apiService = apiService;
            InitializeComponent();  
        }

        protected override async void OnStart(string[] args)
        {
            var data = await _apiService.GetAllCurenci();
            _maker.WriteData(data);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //número em milisegundos
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            _maker.WriteStopMessage();
        }

        private async void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            var data = await _apiService.GetAllCurenci();
            _maker.WriteData(data);
        }
    }
}
