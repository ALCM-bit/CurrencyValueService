using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceCurrencyValue.Dtos
{
    public class CentralBankApiResponseDTO
    {
        public List<CurrencyDTO> Value { get; set; }
    }
}
