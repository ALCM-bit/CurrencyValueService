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

        protected override void OnStart(string[] args)
        {
            Task.Run(async () =>
            {
                var data = await _apiService.GetAllCurenci();
                await _maker.WriteData(data);
                timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                timer.Interval = 7000; // número em milissegundos
                timer.Enabled = true;
            });
        }

        protected override async void  OnStop()
        {
            
             await _maker.WriteStopMessage();
            
            
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Task.Run(async () => {
                var data = await _apiService.GetAllCurenci();
                await _maker.WriteData(data);
            });
            
        }
    }
}
