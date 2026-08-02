using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using AppFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.AppMotion
{
    public class EtcBusService : IEtcBusService
    {
        private readonly IAppAxisBusFactory _factory;
        private readonly IAppAxisBus _axisBus;

        public EtcBusService(IAppAxisBusFactory factory)
        {
            _factory = factory;
            _axisBus = _factory.CreateAppAxisMaster(1);
        }
        public void ClearBusError()
        {
            _axisBus.ClearBusError();
        }

        public async Task CoolResetAsync()
        {
            await _axisBus.CoolResetAsync();
        }

        public void EStop()
        {
            _axisBus.EStop();
        }

        public MotionAxisBusState GetBusState()
        {
            return _axisBus.MasterState;
        }

        public async Task InitMotionCardAsync()
        {
            await _axisBus.InitMotionCardAsync();
        }

        public async Task SoftResetAsync()
        {
            await _axisBus.SoftResetAsync();
        }
    }
}
