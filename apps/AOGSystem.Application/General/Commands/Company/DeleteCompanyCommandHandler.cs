using AOGSystem.Domain.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Commands.Company
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<int> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var data = await _companyRepository.GetCompanyByIDAsync(request.Id);
            if (data != null)
            {
                _companyRepository.Delete(request.Id);
                return await _companyRepository.SaveChangesAsync();
            }
            else
            {
                // TODO
                throw new Exception($"Entity with Id {request.Id} not found.");
            }
        }
    }
    public class DeleteCompanyCommand : IRequest<int>
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }

    }
}
