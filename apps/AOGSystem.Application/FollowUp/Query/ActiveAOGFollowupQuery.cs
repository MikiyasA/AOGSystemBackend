using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Query
{
    public class ActiveAOGFollowupQuery : IActiveAOGFollowupQuery
    {
        private readonly IAOGFollowUpRepository _fupRepository;
        private readonly IPartRepository _partRepository;
        private readonly IFollowUpTabsRepository _followUpTabsRepository;

        public ActiveAOGFollowupQuery(IAOGFollowUpRepository fupRepository, IPartRepository partRepository, IFollowUpTabsRepository followUpTabsRepository)
        {
            _fupRepository = fupRepository;
            _partRepository = partRepository;
            _followUpTabsRepository = followUpTabsRepository;
        }

        public async Task<List<ActiveAOGFollowupDTO>> GetAllActiveFollowUpAsync()
        {
            var returnFollowup = new List<ActiveAOGFollowupDTO>();

            var followUp = await _fupRepository.GetAllActiveFollowUpAsync();

            returnFollowup = followUp.Select(async fup =>
            {
                var part = await _partRepository.GetPartByIDAsync((Guid)fup.PartId);

                return new ActiveAOGFollowupDTO
                {
                    Id = fup.Id,
                    RID = fup.RID,
                    RequestDate = fup.RequestDate,
                    AirCraft = fup.AirCraft,
                    TailNo = fup.TailNo,
                    WorkLocation = fup.WorkLocation,
                    AOGStation = fup.AOGStation,
                    Customer = fup.Customer,
                    PartNumber = part.PartNumber,
                    Description = part.Description,
                    StockNo = part.StockNo,
                    FinancialClass = part.FinancialClass,
                    PONumber = fup.PONumber,
                    OrderType = fup.OrderType,
                    Quantity = fup.Quantity,
                    UOM = fup.UOM,
                    Vendor = fup.Vendor,
                    EDD = fup.EDD,
                    Status = fup.Status,
                    AWBNo = fup.AWBNo,
                    FlightNo = fup.FlightNo,
                    NeedHigherMgntAttn = fup.NeedHigherMgntAttn,
                    Remarks = fup.Remarks,
                };
            }).Select(t => t.Result).ToList();
        
            return returnFollowup;

        }

        public async Task<List<ActiveAOGFollowupDTO>> GetAllActiveFollowUpByTabIdAsync(Guid id)
        {
            var returnFollowup = new List<ActiveAOGFollowupDTO>();

            var followUp = await _fupRepository.GetAllActiveFollowUpByTabIdAsync(id);

            returnFollowup = followUp.Select(async fup =>
            {
                var part = await _partRepository.GetPartByIDAsync((Guid)fup.PartId);

                return new ActiveAOGFollowupDTO
                {
                    Id = fup.Id,
                    RID = fup.RID,
                    RequestDate = fup.RequestDate,
                    AirCraft = fup.AirCraft,
                    TailNo = fup.TailNo,
                    WorkLocation = fup.WorkLocation,
                    AOGStation = fup.AOGStation,
                    Customer = fup.Customer,
                    PartNumber = part.PartNumber,
                    Description = part.Description,
                    StockNo = part.StockNo,
                    FinancialClass = part.FinancialClass,
                    PONumber = fup.PONumber,
                    OrderType = fup.OrderType,
                    Quantity = fup.Quantity,
                    UOM = fup.UOM,
                    Vendor = fup.Vendor,
                    EDD = fup.EDD,
                    Status = fup.Status,
                    AWBNo = fup.AWBNo,
                    FlightNo = fup.FlightNo,
                    NeedHigherMgntAttn = fup.NeedHigherMgntAttn,
                    Remarks = fup.Remarks,
                };
            }).Select(t => t.Result).ToList();

            return returnFollowup;
        }

        public async Task<List<ActiveFollowUpTabsDto>> GetAllActiveFollowUpTabsAsync()
        {
            var returnTabs = new List<ActiveFollowUpTabsDto>();
            
            var tab = await _followUpTabsRepository.GetAllActiveFollowUpTabsAsync();

            returnTabs = tab.Select(async t =>
            {
                var fowups = await GetAllActiveFollowUpByTabIdAsync(t.Id);

                return new ActiveFollowUpTabsDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Color = t.Color,
                    Status = t.Status,
                    FollowUps = fowups,

                };
            }).Select(t => t.Result).ToList();

            return returnTabs;
        }
    }
}
